import { CURRENCY_CODE } from "./constant";

/**
 * @param {string} code
 * @param {number} amount
 * @returns {string}
 */
export function currency(amount, code = CURRENCY_CODE.US_Dollar) {
    return new Intl.NumberFormat(navigator.language, {
        style: 'currency',
        currency: code
    }).format(amount);
}