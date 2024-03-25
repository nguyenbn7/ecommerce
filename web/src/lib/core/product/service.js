import { getProducts } from './request';

/**
 * @param {ShopParams} shopParams
 * @returns {Promise<Page<Product>>}
 */
export async function getPageProduct(shopParams) {
	const params = {};
	if (shopParams.brandId > 0) params['brandId'] = shopParams.brandId;
	if (shopParams.typeId > 0) params['typeId'] = shopParams.typeId;
	params['sort'] = shopParams.sort;
	params['pageNumber'] = shopParams.pageNumber;
	params['pageSize'] = shopParams.pageSize;
	if (shopParams.search) params['search'] = shopParams.search;

	return getProducts(params);
}

/**
 * @returns {ShopParams}
 */
export function getDefaultShopParams() {
	return {
		pageNumber: 1,
		pageSize: 6,
		sort: 'name',
		search: undefined,
		brandId: 0,
		typeId: 0
	};
}

export function getSortOptions() {
	const sortOptions = [
		{ name: 'Alphabetical', value: 'name' },
		{ name: 'Price: Low to High', value: 'price' },
		{ name: 'Price: High to Low', value: '-price' }
	];

	return sortOptions;
}
