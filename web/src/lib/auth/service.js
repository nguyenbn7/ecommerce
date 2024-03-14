import { get, readonly, writable } from 'svelte/store';
import { createHttpClient, delayFetch, notifyFetchError } from "$lib/shared/client/http";

const httpClientBackground = createHttpClient();

const httpClient = createHttpClient();
httpClient.interceptors.response.use(
    async (response) => {
        await delayFetch(1500);
        return response;
    },
    async (error) => {
        await delayFetch(1500);
        notifyFetchError(error);
    }
);

const ACCESS_TOKEN = 'token';

/**
 * @type {import("svelte/store").Writable<Account.UserInfo | null>}
 */
const currentUserStore = writable(null);

export const currentUser = readonly(currentUserStore);

export function getAccessToken() {
    return localStorage.getItem(ACCESS_TOKEN) ?? '';
}

export async function loadUser() {
    const accessToken = getAccessToken();
    if (!accessToken) return;

    const response = await httpClientBackground.get('account/display', {
        headers: {
            Authorization: `Bearer ${getAccessToken()}`
        }
    });
    
    /**
     * @type {Account.UserDisplayInfoResponse}
     */
    const data = response.data;
    currentUserStore.update(() => ({
        email: data.email,
        displayName: `${data.firstName} ${data.lastName}`
    }));
}

export function logout() {
    localStorage.removeItem(ACCESS_TOKEN);
    currentUserStore.update(() => null);
}

/**
 * @param {Account.UserLoginForm} loginForm
 */
export async function loginAsUser(loginForm) {
    /**
     * @type {Account.LoginSuccessResponse}
     */
    const data = (await httpClient.post('account/login', loginForm)).data;

    localStorage.setItem(ACCESS_TOKEN, data.accessToken);

    currentUserStore.update(() => ({
        email: data.email,
        displayName: `${data.firstName} ${data.lastName}`
    }));

    return get(currentUserStore);
}

/**
 * @param {Account.UserRegisterForm} registerForm
 */
export async function registerAsUser(registerForm) {
    /**
     * @type {Account.LoginSuccessResponse}
     */
    const data = (await httpClient.post('account/register', registerForm)).data;

    localStorage.setItem(ACCESS_TOKEN, data.accessToken);

    currentUserStore.update(() => ({
        email: data.email,
        displayName: `${data.firstName} ${data.lastName}`
    }));

    return get(currentUserStore);
}
