<script>
	import { onMount } from 'svelte';
	import ProductItem from '$lib/products/components/product-item.svelte';
	import { getDefaultShopParams, loadShopData, getSortOptions } from '$lib/products/service';
	import { getPageProduct } from '$lib/products/client';
	import Pagination from '$lib/shared/components/pagination.svelte';
	import PaginationHeader from '$lib/shared/components/pagination-header.svelte';
	import PageLoader from '$lib/shared/spinner/page-loader.svelte';

	const sortOptions = getSortOptions();

	/** @type {ShopParams} */
	let shopParams = getDefaultShopParams();
	let isLoadingPage = false;
	let loadingError = false;
	let pageData = {
		/** @type {Product[]} */
		products: [],
		/** @type {ProductType[]} */
		productTypes: [],
		/** @type {ProductBrand[]} */
		productBrands: [],
		/** @type {string | undefined} */
		searchTerm: undefined,
		totalItems: 0,
		maxPageDisplay: 5
	};

	/**
	 * @param {number} brandId
	 */
	async function onBrandIdSelected(brandId) {
		shopParams.brandId = brandId;
		shopParams.pageNumber = 1;
		shopParams.pageSize = shopParams.pageSize < 6 ? 6 : shopParams.pageSize;
		await getNewPageProduct(shopParams);
	}

	/**
	 * @param {number} typeId
	 */
	async function onTypeIdSelected(typeId) {
		shopParams.typeId = typeId;
		shopParams.pageNumber = 1;
		shopParams.pageSize = shopParams.pageSize < 6 ? 6 : shopParams.pageSize;
		await getNewPageProduct(shopParams);
	}

	/**
	 * @param {CustomEvent<any>} $event
	 */
	async function pageChanged($event) {
		shopParams.pageNumber = $event.detail.pageNumber;
		await getNewPageProduct(shopParams);
	}

	async function search() {
		if (pageData.searchTerm) {
			shopParams.search = pageData.searchTerm;
			shopParams.pageNumber = 1;
			return;
		}
		shopParams = getDefaultShopParams();
	}

	/**
	 * @param {Event & { currentTarget: EventTarget & HTMLSelectElement; }} $event
	 */
	async function onSortSelected($event) {
		shopParams.sort = $event.currentTarget.value ?? 'name';
	}

	/**
	 * @param {KeyboardEvent & { currentTarget: EventTarget & HTMLInputElement; }} $event
	 */
	async function onPressEnter($event) {
		if ($event.code === 'Enter') return await search();
	}

	/**
	 * @param {ShopParams} shopParams
	 */
	async function getNewPageProduct(shopParams) {
		// TODO: handle error
		const pageProduct = await getPageProduct(shopParams);
		pageData.products = [...pageProduct.data];
		pageData.totalItems = pageProduct.totalItems;
	}

	onMount(async () => {
		try {
			isLoadingPage = true;
			loadingError = false;

			const data = await loadShopData(shopParams);

			pageData.products = [...data.pageProducts.data];
			pageData.productBrands = [...data.productBrands];
			pageData.productTypes = [...data.productTypes];
			pageData.totalItems = data.pageProducts.totalItems;
		} catch (error) {
			loadingError = true;
			console.log(error);
		} finally {
			isLoadingPage = false;
			loadingError = false;
		}
	});
</script>

{#if isLoadingPage}
	<PageLoader />
{:else}
	<main class="flex-grow-1 mb-4 mt-5">
		<div class="container-fluid px-5">
			<div class="row">
				<aside class="col-12 col-md-3">
					<div class="d-flex mt-2 mb-3">
						<div class="input-group mb-3">
							<input
								type="text"
								class="form-control"
								placeholder="Search by product name"
								aria-describedby="button-addon2"
								bind:value={pageData.searchTerm}
								on:keyup={onPressEnter}
							/>
							<button class="btn btn-outline-secondary" type="button" on:click={search}>
								<i class="fa-solid fa-magnifying-glass"></i>
							</button>
						</div>
					</div>
					<div class="mt-2 mb-4">
						<h5 class="text-warning mb-3">Sort By</h5>
						<select class="form-select" on:change={onSortSelected}>
							{#each sortOptions as sort}
								<option value={sort.value}>{sort.name}</option>
							{/each}
						</select>
					</div>
					<div class="mt-2 mb-4">
						<h5 class="text-warning mb-3">Brands</h5>
						<ul class="list-group">
							{#each pageData.productBrands as brand}
								<a
									href={'javascript:;'}
									class="list-group-item"
									on:click={() => onBrandIdSelected(brand.id)}
									class:active={brand.id === shopParams.brandId}
								>
									{brand.name}
								</a>
							{/each}
						</ul>
					</div>
					<div class="mt-2 mb-4">
						<h5 class="text-warning mb-3">Types</h5>
						<ul class="list-group">
							{#each pageData.productTypes as type}
								<a
									href={'javascript:;'}
									class="list-group-item"
									on:click={() => onTypeIdSelected(type.id)}
									class:active={type.id === shopParams.typeId}
								>
									{type.name}
								</a>
							{/each}
						</ul>
					</div>
				</aside>
				<div class="col-12 col-md-9">
					<div class="d-flex justify-content-between pt-3 pb-4p pt-md-0">
						<PaginationHeader
							pageNumber={shopParams.pageNumber}
							totalItems={pageData.totalItems}
							pageSize={shopParams.pageSize}
						/>
					</div>

					<div class="row row-cols-1 row-cols-md-2 row-cols-xl-3 row-cols-xxl-3 g-3 mb-4">
						{#each pageData.products as product}
							<div class="col">
								<ProductItem {product} />
							</div>
						{/each}
					</div>

					<div class="d-flex justify-content-center mb-3">
						<Pagination
							totalItems={pageData.totalItems}
							itemsPerPage={shopParams.pageSize}
							pageNumber={shopParams.pageNumber}
							maxSize={pageData.maxPageDisplay}
							previousText="&lsaquo;"
							nextText="&rsaquo;"
							firstText="&laquo;"
							lastText="&raquo;"
							on:pageChanged={pageChanged}
						/>
					</div>
				</div>
			</div>
		</div>
	</main>
{/if}
