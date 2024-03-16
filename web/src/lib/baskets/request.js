import { createHttpClient } from "$lib/shared/client/http";

const httpClient = createHttpClient("basket");

/**
 * @param {string} id
 */
export async function getBasket(id) {
    return await httpClient.get(`?id=${id}`);
}

/**
 * @param {Basket} basket
 */
export async function setBasket(basket) {
    return await httpClient.post('', basket);
}

/**
 * @param {Basket} basket
 */
export async function deleteBasket(basket) {
    return await httpClient.delete(`?id=${basket.id}`);
}