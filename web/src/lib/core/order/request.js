import { getAccessToken } from '../auth/service';
import { createHttpClient } from '../httpClient';
import { delayFetch } from '../httpClient/plugin';

const httpClient = createHttpClient('orders');

httpClient.interceptors.request.use((config) => {
	config.headers.Authorization = `Bearer ${getAccessToken()}`;
	return config;
});

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
 * @param {CreateOrder} data
 */
export async function createCustomerOrder(data) {
	return await httpClient.post('', data);
}

export async function getOrderPreview() {
	return await httpClient.get('preview');
}

/**
 *
 * @returns {Promise<string[]>}
 */
export async function getPaymentTypes() {
	const response = await httpClient.get('payment/types');
	return response.data;
}

/**
 *
 * @returns {Promise<DeliveryMethod[]>}
 */
export async function getDeliveryMethods() {
	const response = await httpClient.get('shipping/methods');
	return response.data;
}
