<script context="module">
	import _ from 'lodash';
	import { readonly, writable } from 'svelte/store';

	/** @type {import("svelte/store").Writable<{[x: string]: string}>} */
	const breadcrumbStore = writable({});
	export const mapper = readonly(breadcrumbStore);

	/**
	 * @param {string} pathVariable
	 * @param {string} alias
	 * @example
	 * routes: product/[productId]
	 * call: createPathVariableAlias("[productId]", "productName")
	 * Breadcrumb: Home > Product > ProductName
	 */
	function createPathVariableAlias(pathVariable, alias) {
		breadcrumbStore.update((curr) => ({ ...curr, [pathVariable]: alias }));
	}

	/**
	 * @param {import("@sveltejs/kit").Page<Record<string, string>, string | null>} page
	 * @param {{ [x: string]: string | undefined; }} mapper
	 */
	function buildAliasPaths(page, mapper) {
		const paths = page.route.id?.split('/').filter((path) => path && path[0] !== '(') ?? [];
		const pathParams = page.params;

		const builtPaths = [{ alias: _.startCase('home'), href: paths.length ? '/' : null }];

		for (let idx = 0; idx < paths.length; idx++) {
			let alias = null;
			let href = null;

			// @ts-ignore
			if (paths[idx].startsWith('[')) {
				// @ts-ignore
				if (mapper && mapper[paths[idx]]) alias = _.startCase(mapper[paths[idx]]);
				else alias = pathParams[paths[idx].slice(1, -1)];
			} else {
				alias = _.startCase(paths[idx]);
			}

			if (idx < paths.length - 1) {
				const prefPath = builtPaths.slice(-1)[0].href;
				href =
					prefPath === '/'
						? `${builtPaths.slice(-1)[0].href}${paths[idx]}`
						: `${builtPaths.slice(-1)[0].href}/${paths[idx]}`;
			}
			builtPaths.push({ alias, href });
		}

		return builtPaths;
	}

	export const breadcrumbService = {
		buildAliasPaths,
		createPathVariableAlias
	};
</script>

<script>
	export { styleClasses as class };
	let styleClasses = '';
	styleClasses = 'breadcrumb ' + styleClasses.trim();
	styleClasses.trim();
	/**
	 * @type {{alias: string, href: string | null}[]}
	 */
	export let breadcrumbs;
</script>

<ol class={styleClasses} aria-label="breadcrumb" {...$$restProps}>
	{#each breadcrumbs as path, idx}
		{#if !path.href && !idx}
			<li class="breadcrumb-item active" aria-current="page">
				{path.alias}
			</li>
		{:else if !path.href}
			<li class="breadcrumb-item active" aria-current="page">
				{path.alias}
			</li>
		{:else if !idx}
			<li class="breadcrumb-item">
				<a class="link-primary fw-semibold text-decoration-none" href={path.href}>
					{path.alias}
				</a>
			</li>
		{:else}
			<li class="breadcrumb-item">
				<a class="link-primary fw-semibold text-decoration-none" href={path.href}>
					{path.alias}
				</a>
			</li>
		{/if}
	{/each}
</ol>
