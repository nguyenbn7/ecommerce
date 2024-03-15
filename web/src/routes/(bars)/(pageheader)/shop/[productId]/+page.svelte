<script>
	import { page } from '$app/stores';
	import { onMount } from 'svelte';
	import { getProduct } from '$lib/products/request';
	import { createPathVariableAlias } from '$lib/shared/breadcrumbs/service';
	import { APP_NAME } from '$lib/shared/constant';
	import { currency } from '$lib/shared/helper';
	import { addItemToBasket, basket, removeItemFromBasket } from '$lib/baskets/service';
	import PageLoader from '$lib/shared/spinner/page-loader.svelte';

	/**
	 * @type {Product | null}
	 */
	let product = null;
	let productName = '';
	let quantityInBasket = 0;
	let quantity = 1;

	$: buttonText = quantityInBasket === 0 ? 'Add to basket' : 'Update basket';

	$: if ($basket && product) {
		const item = $basket?.items.find((i) => i.id === product?.id);

		if (item) {
			quantity = item.quantity;
			quantityInBasket = item.quantity;
		}
	}

	onMount(async () => {
		const productId = Number($page.params.productId);
		product = await getProduct(productId);
		productName = product.name;
		createPathVariableAlias('[productId]', productName);
	});

	function incrementQuantity() {
		quantity++;
	}

	function decrementQuantity() {
		if (quantity < 1) return;
		quantity--;
	}

	function updateBasket() {
		if (product) {
			if (quantity > quantityInBasket) {
				const itemToAdd = quantity - quantityInBasket;
				quantityInBasket += itemToAdd;
				addItemToBasket(product, itemToAdd);
			} else {
				const itemToRemove = quantityInBasket - quantity;
				quantityInBasket -= itemToRemove;
				removeItemFromBasket(product.id, itemToRemove);
			}
		}
	}
</script>

<svelte:head>
	{#if productName}
		<title>{APP_NAME} | {productName}</title>
	{/if}
</svelte:head>

{#if product}
	<div class="container">
		<div class="row">
			<div class="col-6">
				<img src={product.pictureUrl} alt={product.name} class="w-100" />
			</div>
			<div class="col-6 mt-5">
				<h2>{product.name}</h2>
				<p class="fw-semibold fst-italic">{currency(product.price)}</p>

				<h5 class="text-primary mb-3">
					You have {quantityInBasket} of this item in your basket
				</h5>

				<div class="d-flex justify-content-start align-items-center">
					<button
						class="p-0 m-0 me-2 border-0 quantity-btn"
						on:click={decrementQuantity}
						disabled={quantity < 1}
					>
						<i class="fa-solid fa-circle-minus"></i>
					</button>
					<span class="fw-semibold" style="font-size: 1.5em;">{quantity}</span>
					<button class="p-0 m-0 ms-2 border-0 quantity-btn" on:click={incrementQuantity}>
						<i class="fa-solid fa-circle-plus"></i>
					</button>
					<button
						class="btn btn-danger ms-4"
						on:click={updateBasket}
						disabled={quantity === quantityInBasket}>{buttonText}</button
					>
				</div>
				<div class="row mt-4">
					<h4>Description</h4>
					<p>{product.description}</p>
				</div>
			</div>
		</div>
	</div>
{:else}
	<PageLoader />
{/if}

<style lang="scss">
	.quantity-btn {
		--bs-text-opacity: 1;
		color: rgba(var(--bs-warning-rgb), var(--bs-text-opacity));
		background-color: transparent;
	}

	.quantity-btn i {
		font-size: 2em;
	}

	.quantity-btn:hover i {
		color: rgba(245, 197, 53, 0.8);
	}

	.quantity-btn:active i {
		color: rgb(185, 143, 18);
	}

	.quantity-btn:disabled i {
		color: rgb(207, 183, 109, 0.65);
		pointer-events: none;
	}
</style>
