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
export function createPathVariableAlias(pathVariable, alias) {
	breadcrumbStore.update((curr) => ({ ...curr, [pathVariable]: alias }));
}

/**
 * @param {import("@sveltejs/kit").Page<Record<string, string>, string | null>} page
 * @param {{ [x: string]: string | undefined; }} mapper
 */
export function buildAliasPaths(page, mapper) {
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
