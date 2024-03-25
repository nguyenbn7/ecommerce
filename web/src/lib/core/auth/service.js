import { readonly, writable } from 'svelte/store';
import { getDisplayName, login, register } from './request';
import { showClientError } from '$lib/core/httpClient/plugin';

const ACCESS_TOKEN_KEY_NAME = 'access_token';

/**
 * @type {import("svelte/store").Writable<CustomerInfo | null>}
 */
const userInfoStore = writable(null);
export const userInfo = readonly(userInfoStore);

export function getAccessToken() {
	return localStorage.getItem(ACCESS_TOKEN_KEY_NAME) ?? '';
}

export async function loadUser() {
	const accessToken = getAccessToken();
	if (!accessToken) return;

	try {
		const response = await getDisplayName();
		/**
		 * @type {string}
		 */
		const displayName = response.data;

		userInfoStore.update(() => {
			return {
				displayName: displayName
			};
		});
	} catch (error) {
		console.log(error);
	}
}

export function logout() {
	localStorage.removeItem(ACCESS_TOKEN_KEY_NAME);
	userInfoStore.update(() => null);
}

/**
 * @param {CustomerLoginSuccess} data
 */
function updateUserInfo(data) {
	localStorage.setItem(ACCESS_TOKEN_KEY_NAME, data.accessToken);

	userInfoStore.update((_) => {
		return {
			displayName: data.displayName
		};
	});
}

/**
 * @param {CustomerLogin} customerLogin
 * @returns {Promise<string | null>} customer display name
 */
export async function loginAsCustomer(customerLogin) {
	try {
		const response = await login(customerLogin);

		/**
		 * @type {CustomerLoginSuccess}
		 */
		const data = response.data;

		updateUserInfo(data);

		return data.displayName;
	} catch (err) {
		showClientError(err);
		return null;
	}
}

/**
 * @param {CustomerRegister} customerRegister
 * @returns {Promise<string|null>} customer display name
 */
export async function registerAsCustomer(customerRegister) {
	try {
		const response = await register(customerRegister);
		/**
		 * @type {CustomerLoginSuccess}
		 */
		const data = response.data;

		updateUserInfo(data);

		return data.displayName;
	} catch (err) {
		showClientError(err);
		return null;
	}
}
