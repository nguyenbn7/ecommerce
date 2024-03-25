import { createHttpClient } from '$lib/core/httpClient';
import { delayFetch } from '$lib/core/httpClient/plugin';

const httpClient = createHttpClient('products');

httpClient.interceptors.response.use(
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
 * @returns {Promise<Product>}
 */
export async function getProduct(id) {
	const response = await httpClient.get(`${id}`);
	return response.data;
}

/**
 * @returns {Promise<ProductBrand[]>}
 */
export async function getProductBrands() {
	const response = await httpClient.get('brands');
	return response.data;
}

/**
 * @returns {Promise<ProductType[]>}
 */
export async function getProductTypes() {
	const response = await httpClient.get('types');
	return response.data;
}

/**
 * @param {ShopParams} shopParams
 * @returns {Promise<Page<Product>>}
 */
export async function getPageProduct(shopParams) {
	const params = {};
	if (shopParams.brandId > 0) params['brandId'] = shopParams.brandId;
	if (shopParams.typeId > 0) params['typeId'] = shopParams.typeId;
	params['sort'] = shopParams.sort;
	params['pageNumber'] = shopParams.pageNumber;
	params['pageSize'] = shopParams.pageSize;
	if (shopParams.search) params['search'] = shopParams.search;

	const response = await httpClient.get('', {
		params
	});
	return response.data;
}

/**
 * TODO:
 * @returns {Promise<Product>}
 */
export async function getDealProduct() {
	const response = await httpClient.get('deal');
	return response.data;
}
/**
 * TODO:
 * @returns {Promise<Array<Product>>}
 */
export async function getNewArrivalProducts() {
	const response = await httpClient.get('new-arrivals');
	return response.data;
}
