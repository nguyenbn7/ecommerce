import { createHttpClient } from '$lib/core/httpClient';
import { delayFetch } from '$lib/core/httpClient/plugin';

const httpClient = createHttpClient('products');

const delay = httpClient.interceptors.response.use(
	async (response) => {
		await delayFetch(1000);
		return response;
	},
	async (error) => {
		await delayFetch(1000);
		throw error;
	}
);

/**
 * @param {number} id
 */
export async function getProduct(id) {
	return httpClient.get(`${id}`);
}

export async function getProductBrands() {
	return httpClient.get('brands');
}

export async function getProductTypes() {
	return httpClient.get('types');
}

/**
 * @param {ShopParams} queries
 */
export async function getProducts(queries) {
	return httpClient.get('', { params: queries });
}

/**
 * TODO:
 * @returns {Promise<Product>}
 */
async function getDealProduct() {
	return httpClient.get('deal');
}
/**
 * TODO:
 * @returns {Promise<Array<Product>>}
 */
async function getNewArrivalProducts() {
	return httpClient.get('new-arrivals');
}
