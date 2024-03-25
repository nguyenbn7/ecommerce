<script context="module">
	import _ from 'lodash';
	import { readonly, writable } from 'svelte/store';

	/** @type {import("svelte/store").Writable<{[x: string]: string}>} */
	const aliasesStore = writable({});
	export const aliases = readonly(aliasesStore);

	/**
	 * @param {string} pathVariable
	 * @param {string} alias
	 * @example
	 * routes: products/123
	 * call: addAnAlias(123, "Sock")
	 * Breadcrumb: Home > Product > Sock
	 */
	export function addAlias(pathVariable, alias) {
		aliasesStore.update((curr) => ({ ...curr, [pathVariable]: alias }));
	}

	/**
	 * @param {import("@sveltejs/kit").Page<Record<string, string>, string | null>} page
	 * @param {{ [x: string]: string | undefined; }} aliases
	 */
	export function createBreadcrumbs(page, aliases) {
		const paths = page.url.pathname.split('/').filter((path) => path);

		const breadcrumbs = [{ name: _.startCase('home'), href: '/' }];

		for (const path of paths) {
			let name = _.startCase(path);

			if (aliases[path]) {
				name = _.startCase(aliases[path]);
			}

			const prefPath = breadcrumbs.slice(-1)[0].href;
			let href =
				prefPath === '/'
					? `${breadcrumbs.slice(-1)[0].href}${path}`
					: `${breadcrumbs.slice(-1)[0].href}/${path}`;

			breadcrumbs.push({ name: name, href });
		}

		return breadcrumbs;
	}

	/**
	 * @param {{ name: string; href: string; }[]} breadcrumbs
	 */
	export function getLastBreadcrumbItem(breadcrumbs) {
		return breadcrumbs.at(-1);
	}
</script>

<script>
	export { classNames as class };
	/**
	 * @type {string}
	 */
	let classNames;

	/**
	 * @type {{name: string, href: string}[]}
	 */
	export let breadcrumbs;
</script>

<ol class={classNames} {...$$restProps}>
	{#each breadcrumbs as breadcrumbItem, idx}
		{#if idx < breadcrumbs.length - 1}
			<li class="breadcrumb-item">
				<a class="link-primary fw-semibold text-decoration-none" href={breadcrumbItem.href}>
					{breadcrumbItem.name}
				</a>
			</li>
		{:else}
			<li class="breadcrumb-item active" aria-current="page">
				{breadcrumbItem.name}
			</li>
		{/if}
	{/each}
</ol>
