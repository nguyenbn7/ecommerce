import { ProductRequest } from "./_request";

/**
 * @param {ShopParams} shopParams
 */
async function loadShopData(shopParams) {
	const result = await Promise.all([
		ProductRequest.getProductBrands(),
		ProductRequest.getProductTypes(),
		ProductRequest.getPageProduct(shopParams)
	]);

	return {
		productBrands: [{ id: 0, name: 'All' }, ...result[0]],
		productTypes: [{ id: 0, name: 'All' }, ...result[1]],
		products: [...result[2].data],
		pageNumber: result[2].pageNumber,
		pageSize: result[2].pageSize,
		totalItems: result[2].totalItems
	};
}

/**
 * @returns {ShopParams}
 */
function getDefaultShopParams() {
	return {
		pageNumber: 1,
		pageSize: 6,
		sort: 'name',
		search: undefined,
		brandId: 0,
		typeId: 0
	}
}

export const ProductService = {
	loadShopData,
	getDefaultShopParams
};