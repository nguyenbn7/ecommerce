import { createHttpClient } from '$lib/core/httpClient';
import { delayFetch } from '$lib/core/httpClient/plugin';
import { getAccessToken } from './service';

const httpClient = createHttpClient('account');

httpClient.interceptors.response.use(
	async (response) => {
		await delayFetch(1500);
		return response;
	},
	async (error) => {
		await delayFetch(1500);
		throw error;
	}
);

/**
 * @param {CustomerLogin} loginDTO
 */
export async function login(loginDTO) {
	return await httpClient.post('login', loginDTO);
}

/**
 * @param {CustomerRegister} registerDTO
 */
export async function register(registerDTO) {
	return await httpClient.post('register', registerDTO);
}

export async function getDisplayName() {
	return await httpClient.get('display-name', {
		headers: {
			Authorization: `Bearer ${getAccessToken()}`
		}
	});
}

/**
 *
 * @returns {Promise<UserProfile>}
 */
export async function getUserProfile() {
	const response = await httpClient.get('profile', {
		headers: {
			Authorization: `Bearer ${getAccessToken()}`
		}
	});

	return response.data;
}
