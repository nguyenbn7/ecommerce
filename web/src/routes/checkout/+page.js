import { userInfo } from '$lib/core/auth/service';
import { redirect } from '@sveltejs/kit';
import { get } from 'svelte/store';

/** @type {import('./$types').PageLoad} */
export async function load({ parent, url }) {
	await parent();
	const user = get(userInfo);
	if (!user) redirect(302, `/account/login?next=${url.href}`);
}
