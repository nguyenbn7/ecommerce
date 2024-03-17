import { getAccessToken } from "$lib/auth/service";
import { createHttpClient, delayFetch } from "$lib/shared/client/http";

const httpClient = createHttpClient("orders");

httpClient.interceptors.request.use(config => {
    config.headers.Authorization = `Bearer ${getAccessToken()}`;
    return config;
})

httpClient.interceptors.response.use(async (response) => {
    await delayFetch(1000);
    return response;
}, async (error) => {
    await delayFetch(1000);
    throw error;
});

/**
 * @param {Order} data
 */
export async function createOrder(data) {
    return await httpClient.post('', data);
}

export async function getPaymentTypes() {
    return await httpClient.get('payments');
}

export async function getDeliveryMethods() {
    return await httpClient.get('/delivery-methods');
}
