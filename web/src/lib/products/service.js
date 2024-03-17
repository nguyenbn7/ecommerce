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
