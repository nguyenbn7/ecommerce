import { loadUser } from '$lib/auth/service';
import { loadBasket } from '$lib/baskets/service';

export const ssr = false;

/** @type {import('./$types').LayoutLoad} */
export async function load() {
	loadBasket();
	loadUser();
}
