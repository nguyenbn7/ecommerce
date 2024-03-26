import { showDanger } from '$lib/component/toastr.svelte';

/**
 * Delay plugin if you want to slow down your request or response
 * @param {number} ms
 * @returns {Promise<void>}
 */
export async function delayFetch(ms = 1500) {
	return new Promise((resolve) => setTimeout(resolve, ms));
}

/**
 * Handle error from axios client and show notification through toast
 * @param {any} err
 */
export function showClientError(err) {
	const error = /** @type {import("axios").AxiosError} */ (err);

	if (!error.response) {
		showDanger(error.message);
		return;
	}
	// TODO: Handle validation errors
	showDanger(error.response.data.message);
}
