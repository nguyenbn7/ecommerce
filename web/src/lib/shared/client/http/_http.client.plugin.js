import { browser } from '$app/environment';
import { getAccessToken } from '$lib/auth/service';

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

	if (browser) {
		// Weird thing is dat you have to do this instead of import modudle like normal
		const toastModules = await import("$lib/shared/toasts/toastr.svelte");
		toastModules.notifyDanger(errorMessage);
	}
	throw error;
}

/**
 * @param {import("axios").InternalAxiosRequestConfig<any>} config
 */
export function addAuthenticationToHeader(config) {
	config.headers.Authorization = `Bearer ${getAccessToken()}`;
}
