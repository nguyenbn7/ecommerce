import { currentUser } from '$lib/auth/service';
import { redirect } from '@sveltejs/kit';
import { get } from 'svelte/store';

/** @type {import('./$types').LayoutLoad} */
export async function load() {
	if (get(currentUser)) {
		throw redirect(302, '/');
	}
	return;
}
