<script>
	import { onMount } from 'svelte';
	import { APP_NAME } from '$lib/module/shared/constant';
	import { ProductRequest, ProductService } from '$lib/module/product';
	import Pagination from '$lib/components/pagination.svelte';
	import PaginationHeader from '$lib/components/pagination-header.svelte';
	import ProductItem from '$lib/shop/product-item.svelte';

	/*** @type {Product[]} */
	let products = [];
	/*** @type {ProductBrand[]} */
	let productBrands = [];
	/*** @type {ProductType[]} */
	let productTypes = [];
	/*** @type {number} */
	let totalItems;
	/*** @type {string | undefined} */
	let searchTerm;
	const sortOptions = [
		{ name: 'Alphabetical', value: 'name' },
		{ name: 'Price: Low to High', value: 'price' },
		{ name: 'Price: High to Low', value: '-price' }
	];
	let shopParams = ProductService.getDefaultShopParams();
	const maxSize = 5;

	onMount(async () => {
		const result = await ProductService.loadShopData(shopParams);
		products = result.products;
		productBrands = result.productBrands;
		productTypes = result.productTypes;
		shopParams.pageNumber = result.pageNumber;
		shopParams.pageSize = result.pageSize;
		totalItems = result.totalItems;
	});

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
	 * @param {ShopParams} shopParams
	 */
	async function getNewPageProduct(shopParams) {
		const pageProduct = await ProductRequest.getPageProduct(shopParams);
		products = [...pageProduct.data];
		shopParams.pageNumber = pageProduct.pageNumber;
		shopParams.pageSize = pageProduct.pageSize;
		totalItems = pageProduct.totalItems;
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

	/**
	 * @param {Event} $event
	 */
	async function onSortSelected($event) {
		// @ts-ignore
		shopParams.sort = $event?.target?.value ?? 'name';
		await getNewPageProduct(shopParams);
	}

	/**
	 * @param {KeyboardEvent & { currentTarget: EventTarget & HTMLInputElement; }} $event
	 */
	async function onKeyUpSearch($event) {
		if ($event.code === 'Enter') {
			await onSearch();
		}
	}

	async function onSearch() {
		// TODO: handle more case
		if (searchTerm === shopParams.search) return;
		if (!searchTerm) {
			searchTerm = undefined;
			shopParams = ProductService.getDefaultShopParams();
			return await getNewPageProduct(shopParams);
		}
		shopParams.search = searchTerm;
		shopParams.pageNumber = 1;
		return await getNewPageProduct(shopParams);
	}
</script>

{#if productBrands.length}
	<div class="row">
		<aside class="col-12 col-md-3">
			<div class="d-flex mt-2 mb-3">
				<div class="input-group mb-3">
					<input
						type="text"
						class="form-control"
						placeholder="Search by product name"
						aria-describedby="button-addon2"
						bind:value={searchTerm}
						on:keyup={onKeyUpSearch}
					/>
					<button class="btn btn-outline-secondary" type="button" on:click={onSearch}>
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
					{#each productBrands as brand}
						<a
							href={'#'}
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
					{#each productTypes as type}
						<a
							href={'#'}
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
					{totalItems}
					pageSize={shopParams.pageSize}
				/>
				<!-- TODO: select page size -->
			</div>

			<div class="row row-cols-1 row-cols-md-2 row-cols-xl-3 row-cols-xxl-3 g-3 mb-4">
				{#each products as product}
					<div class="col">
						<ProductItem {product} />
					</div>
				{/each}
			</div>

			<div class="d-flex justify-content-center mb-3">
				<Pagination
					{totalItems}
					itemsPerPage={shopParams.pageSize}
					pageNumber={shopParams.pageNumber}
					{maxSize}
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

<style lang="scss">
	/* .sidebar-sticky {
		position: -webkit-sticky;
		position: sticky;
		top: 5rem;
		display: block !important;
		height: calc(100vh - 6rem);
		padding-left: 0.25rem;
		margin-left: -0.25rem;
		overflow-y: auto;
	} */
</style>
