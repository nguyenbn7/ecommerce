import { userInfo } from '$lib/auth/service';
import { redirect } from '@sveltejs/kit';
import { get } from 'svelte/store';

/** @type {import('./$types').PageLoad} */
export async function load({ parent, url }) {
    await parent();
    // const user = get(userInfo);
    // if (!user) throw redirect(302, `/account/login?returnUrl=${url.href}`);
}
