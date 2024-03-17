import { currentUser } from '$lib/auth/service';
import { redirect } from '@sveltejs/kit';
import { get } from 'svelte/store';

/** @type {import('./$types').PageLoad} */
export async function load({ parent, url }) {
    await parent();
    const user = get(currentUser)
    if (!user) throw redirect(302, `/login?redirect=${url.pathname}`);
}
