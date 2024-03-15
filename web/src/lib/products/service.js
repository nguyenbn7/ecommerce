import { getPageProduct, getProductBrands, getProductTypes } from "./client";

/**
 * @param {ShopParams} shopParams
 */
export async function loadShopData(shopParams) {
    const result = await Promise.all([
        getProductBrands(),
        getProductTypes(),
        getPageProduct(shopParams)
    ]);

    return {
        productBrands: [{ id: 0, name: 'All' }, ...result[0]],
        productTypes: [{ id: 0, name: 'All' }, ...result[1]],
        pageProducts: result[2]
    };
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
    }
}

export function getSortOptions() {
    const sortOptions = [
        { name: 'Alphabetical', value: 'name' },
        { name: 'Price: Low to High', value: 'price' },
        { name: 'Price: High to Low', value: '-price' }
    ];

    return sortOptions;
}