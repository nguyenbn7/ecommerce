import { get, readonly, writable } from 'svelte/store';
import { getDisplayName, login, register } from './request';
import { notifyDanger } from '$lib/shared/toastr/service';
import { showClientError } from '$lib/core/httpClient/plugin';

const ACCESS_TOKEN_KEY_NAME = 'access_token';

/**
 * @type {import("svelte/store").Writable<UserInfo | null>}
 */
const userInfoStore = writable(null);
export const userInfo = readonly(userInfoStore);

export function getAccessToken() {
	return localStorage.getItem(ACCESS_TOKEN_KEY_NAME) ?? '';
}

export async function loadUser() {
	const accessToken = getAccessToken();
	if (!accessToken) return;

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
}

export function logout() {
	localStorage.removeItem(ACCESS_TOKEN_KEY_NAME);
	userInfoStore.update(() => null);
}

/**
 * @param {LoginSuccess} data
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
 * @param {LoginDTO} loginDTO
 */
export async function loginAsCustomerDemo(loginDTO) {
	try {
		const response = await login(loginDTO);

		/**
		 * @type {LoginSuccess}
		 */
		const data = response.data;

		updateUserInfo(data);

		return data.displayName;
	} catch (err) {
		/**
		 * @type {import('axios').AxiosError}
		 */
		// @ts-ignore
		let error = err;
		notifyDanger(error.message);
		return undefined;
	}
}

/**
 * @param {import('./form').LoginForm} loginForm
 * @returns {Promise<string | null>} customer display name
 */
export async function loginAsCustomer(loginForm) {
	/**
	 * @type {LoginDTO}
	 */
	const loginDTO = {
		email: loginForm.emailField.value,
		password: loginForm.passwordField.value
	};

	try {
		const response = await login(loginDTO);

		/**
		 * @type {LoginSuccess}
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
 * @param {import('./form').RegisterForm} registerForm
 * @returns {Promise<string|null>} customer display name
 */
export async function registerAsCustomer(registerForm) {
	/**
	 * @type {RegisterDTO}
	 */
	const registerDTO = {
		fullName: registerForm.fullNameField.value,
		displayName: registerForm.displayNameField.value,
		email: registerForm.emailField.value,
		password: registerForm.passwordField.value,
		confirmPassword: registerForm.confirmPasswordField.value
	};

	try {
		const response = await register(registerDTO);
		/**
		 * @type {LoginSuccess}
		 */
		const data = response.data;

		updateUserInfo(data);

		return data.displayName;
	} catch (err) {
		showClientError(err);
		return null;
	}
}
