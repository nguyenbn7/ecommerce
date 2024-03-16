import { readonly, writable } from 'svelte/store';

/**
 * @type {import('svelte/store').Writable<{message:string, type: ToastType}>}
 */
const toastStore = writable();
export const toastService = readonly(toastStore);

/**
 * @type {import('svelte/store').Writable<{message:string, type: ToastType}[]>}
 */
const toastsStore = writable([]);
export const toastsService = readonly(toastsStore);

let showSingleToast = true;

/**
 * @param {boolean} flag
 */
export function saveShowSingleToastOption(flag) {
    showSingleToast = flag;
}

/**
 * @param {string} message
 * @param {ToastType} type
 */
export function notify(message, type) {
    if (showSingleToast)
        return toastStore.update((_) => ({
            message,
            type
        }));
        
    return toastsStore.update(toasts => [...toasts, { message, type }]);
}

/**
 * @param {string} message
 */
export function notifyDanger(message) {
    notify(message, 'DANGER');
}

/**
 * @param {string} message
 */
export function notifySuccess(message) {
    notify(message, 'SUCCESS');
}

/**
 * @param {string} message
 */
export function notifyWarning(message) {
    notify(message, 'WARNING');
}

/**
 * @param {string} message
 */
export function notifyInfo(message) {
    notify(message, 'INFO');
}