import { userInfo } from '$lib/core/auth/service';
import { redirect } from '@sveltejs/kit';
import { get } from 'svelte/store';

/** @type {import('./$types').LayoutLoad} */
export async function load() {
	if (get(userInfo)) {
		throw redirect(302, '/');
	}
	return;
}
