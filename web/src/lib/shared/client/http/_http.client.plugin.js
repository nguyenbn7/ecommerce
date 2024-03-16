import { getAccessToken } from '$lib/auth/service';
import { notifyDanger } from '$lib/shared/toastr/service';

export async function delayFetch(ms = 1500) {
	return new Promise((resolve) => setTimeout(resolve, ms));
}

/**
 * @param {import("axios").AxiosError} error
 */
export async function notifyFetchError(error) {
	let errorMessage = error.message;

	if (error.response) {
		errorMessage = error.response.data.message;
	}

	notifyDanger(errorMessage);
	throw error;
}

/**
 * @param {import("axios").InternalAxiosRequestConfig<any>} config
 */
export function addAuthenticationToHeader(config) {
	config.headers.Authorization = `Bearer ${getAccessToken()}`;
}
