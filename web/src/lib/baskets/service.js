import { get, readonly, writable } from 'svelte/store';
import { deleteBasket, getBasket, setBasket } from './request';

const BASKET_ID = 'basketId';

/**
 * @type {import("svelte/store").Writable<Basket | null>}
 */
const basketSource = writable(null);
export const basket = readonly(basketSource);

/**
 * @type {import("svelte/store").Writable<BasketTotals | null>}
 */
const basketTotalsSource = writable(null);
export const basketTotals = readonly(basketTotalsSource);

function calculateTotals() {
	const basket = get(basketSource);
	if (!basket) return;

	const shipping = 0;
	const subtotal = basket.items.reduce((total, item) => total + item.price * item.quantity, 0);
	const total = subtotal + shipping;

	basketTotalsSource.update(() => ({ shipping, subtotal, total }));
}

/**
 * if newBasket is null, reset the stores
 * @param {Basket | null} newBasket
 */
function updateStores(newBasket = null) {
	if (newBasket === null) {
		basketSource.update(() => null);
		basketTotalsSource.update(() => null);
		return;
	}

	basketSource.update(() => newBasket);
	calculateTotals();
}

export async function loadBasket() {
	const basketId = localStorage.getItem(BASKET_ID);
	if (!basketId) return;

	const response = await getBasket(basketId);

	/**
	 * @type {Basket}
	 */
	const basket = response.data;

	return updateStores(basket);
}

/**
 * @param {BasketItem | Product} item
 * @returns {item is Product}
 */
function isProduct(item) {
	return Object.hasOwn(item, 'productBrand');
}

/**
 * @param {Product} item
 * @returns {BasketItem}
 */
function mapProductItemToBasketItem(item) {
	return {
		id: item.id,
		productName: item.name,
		price: item.price,
		quantity: 0,
		pictureUrl: item.pictureUrl,
		brand: item.productBrand,
		type: item.productType
	};
}

function createBasket() {
	/**
	 * @type {Basket}
	 */
	// TODO: set key here
	const basket = {
		id: 'basket1',
		items: []
	};

	localStorage.setItem(BASKET_ID, basket.id);
	return basket;
}

/**
 * @param {BasketItem[]} items
 * @param {BasketItem} itemToAdd
 * @param {number} quantity
 */
function addOrUpdateItem(items, itemToAdd, quantity) {
	const item = items.find((i) => i.id === itemToAdd.id);
	if (item) item.quantity += quantity;
	else {
		itemToAdd.quantity = quantity;
		items.push(itemToAdd);
	}
	return items;
}

/**
 * @param {BasketItem | Product} item
 * @param {number} quantity
 */
export async function addItemToBasket(item, quantity = 1) {
	if (isProduct(item)) item = mapProductItemToBasketItem(item);

	const basket = get(basketSource) ?? createBasket();
	basket.items = addOrUpdateItem(basket.items, item, quantity);

	const response = await setBasket(basket);
	const newBasket = response.data;

	return updateStores(newBasket);
}

/**
 * @param {number} id
 */
export async function removeItemFromBasket(id, quantity = 1) {
	const basket = get(basketSource);
	if (!basket) return;

	const item = basket.items.find((i) => i.id === id);
	if (!item) return;

	item.quantity -= quantity;
	if (item.quantity <= 0) {
		basket.items = basket.items.filter((i) => i.id !== id);
	}

	if (basket.items.length > 0) {
		const response = await setBasket(basket);
		const newBasket = response.data;

		return updateStores(newBasket);
	}

	const response = await deleteBasket(basket);

	if (response.status === 200) return updateStores();
}
