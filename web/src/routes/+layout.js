import { loadUser } from '$lib/core/auth/service';
import { loadBasket } from '$lib/core/basket/service';

export const ssr = false;

/** @type {import('./$types').LayoutLoad} */
export async function load() {
	await Promise.all([loadUser(), loadBasket()]);
}
