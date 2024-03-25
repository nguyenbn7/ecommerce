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
	 * routes: product/[productId]
	 * call: addAnAlias("[productId]", "productName")
	 * Breadcrumb: Home > Product > ProductName
	 */
	export function addAlias(pathVariable, alias) {
		aliasesStore.update((curr) => ({ ...curr, [pathVariable]: alias }));
	}

	/**
	 * @param {import("@sveltejs/kit").Page<Record<string, string>, string | null>} page
	 * @param {{ [x: string]: string | undefined; }} aliases
	 */
	export function createBreadcrumbs(page, aliases) {
		const paths = page.route.id?.split('/').filter((path) => path && path[0] !== '(') ?? [];
		const pathParams = page.params;

		const breadcrumbs = [{ name: _.startCase('home'), href: '/' }];

		for (let idx = 0; idx < paths.length; idx++) {
			let name = _.startCase(paths[idx]);

			if (paths[idx].startsWith('[')) {
				// if alias defined use alias else use value of params
				name = aliases[paths[idx]]
					? _.startCase(aliases[paths[idx]])
					: pathParams[paths[idx].slice(1, -1)];
			}

			const prefPath = breadcrumbs.slice(-1)[0].href;
			let href =
				prefPath === '/'
					? `${breadcrumbs.slice(-1)[0].href}${paths[idx]}`
					: `${breadcrumbs.slice(-1)[0].href}/${paths[idx]}`;

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
