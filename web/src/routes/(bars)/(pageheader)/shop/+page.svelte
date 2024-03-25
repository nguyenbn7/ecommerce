<script>
	import { onMount } from 'svelte';
	import { getDefaultShopParams, getSortOptions } from '$lib/core/product/service';
	import { getPageProduct, getProductBrands, getProductTypes } from '$lib/core/product/request';
	import Pagination from '$lib/component/pagination.svelte';
	import PageLoader from '$lib/component/page-loader.svelte';
	import SectionLoader from '$lib/component/section-loader.svelte';
	import ProductItem from '$lib/component/shop/product-item.svelte';
	import PaginationHeader from '$lib/component/shop/pagination-header.svelte';

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
		maxPageDisplay: 5,
		shopParamsUpdated: false
	};

	/**
	 * @param {number} brandId
	 */
	async function onBrandIdSelected(brandId) {
		shopParams.brandId = brandId;
		shopParams.pageNumber = 1;
		shopParams.pageSize = shopParams.pageSize < 6 ? 6 : shopParams.pageSize;
	}

	/**
	 * @param {number} typeId
	 */
	async function onTypeIdSelected(typeId) {
		shopParams.typeId = typeId;
		shopParams.pageNumber = 1;
		shopParams.pageSize = shopParams.pageSize < 6 ? 6 : shopParams.pageSize;
	}

	/**
	 * @param {CustomEvent<any>} $event
	 */
	async function pageChanged($event) {
		shopParams.pageNumber = $event.detail.pageNumber;
	}

	async function search() {
		if (pageData.searchTerm) {
			shopParams.search = pageData.searchTerm;
			shopParams.pageNumber = 1;
			return;
		} else {
			shopParams = getDefaultShopParams();
		}
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

		return pageData.products;
	}

	onMount(async () => {
		try {
			isLoadingPage = true;
			loadingError = false;

			const data = await Promise.all([getProductBrands(), getProductTypes()]);

			pageData.productBrands = [{ id: 0, name: 'All' }, ...data[0]];
			pageData.productTypes = [{ id: 0, name: 'All' }, ...data[1]];
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

			<div
				class="row row-cols-1 row-cols-md-2 row-cols-xl-3 row-cols-xxl-3 g-3 mb-4"
				style="min-height: 80%;"
			>
				{#await getNewPageProduct(shopParams)}
					<div class="d-flex align-items-center flex-fill">
						<SectionLoader class="text-center flex-fill" />
					</div>
				{:then products}
					{#each products as product}
						<div class="col">
							<ProductItem {product} />
						</div>
					{/each}
				{/await}
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
{/if}
