import { notifyDanger } from '$lib/shared/toastr/service';
import { AxiosError } from 'axios';

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
	const error = /** @type {AxiosError} */ (err);

	if (!error.response) {
		notifyDanger(error.message);
		return;
	}
	// TODO: Handle validation errors
	notifyDanger(error.response.data.message);
}
