import { createHttpClient, delayFetch } from "$lib/shared/client/http";
import { getAccessToken } from "./service";

const httpClient = createHttpClient('account');

httpClient.interceptors.response.use(
    async (response) => {
        await delayFetch(1500);
        return response;
    },
    async (error) => {
        await delayFetch(1500);
        throw error
    }
);

/**
 * @param {LoginDTO} loginDTO
 */
export async function login(loginDTO) {
    return await httpClient.post('login', loginDTO);
}

/**
 * @param {RegisterDTO} registerDTO
 */
export async function register(registerDTO) {
    return await httpClient.post('register', registerDTO);
}

export async function getDisplayName() {
    return await httpClient.get('display', {
        headers: {
            Authorization: `Bearer ${getAccessToken()}`
        }
    });
}