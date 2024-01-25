import { AccountService, ToastrService } from "$lib";

export async function delayFetch(ms = 1500) {
    return new Promise((resolve) => setTimeout(resolve, ms));
}

/**
 * @param {import("axios").AxiosError} error
 */
export function notifyFetchError(error) {
    let errorMessage = error.message;

    if (error.response) {
        errorMessage = error.response.data.message;
    }

    ToastrService.notifyDanger(errorMessage);
    throw error;
}

/**
 * @param {import("axios").InternalAxiosRequestConfig<any>} config
 */
export function addAuthenticationToHeader(config) {
    config.headers.Authorization = `Bearer ${AccountService.getAccessToken()}`;
}