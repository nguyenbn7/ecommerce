<script>
	import { addItemToBasket } from '$lib/core/basket/service';
	import { currency } from '$lib/service';

	/**
	 * @type {Product}
	 */
	export let product;
</script>

<div class="card shadow-sm">
	<div class="image position-relative">
		<img src={product.pictureUrl} alt={product.name} class="img-fluid bg-info" />
		<div class="d-flex align-items-center justify-content-center hover-overlay">
			<button
				class="btn btn-success me-2"
				title="Add to basket"
				on:click={() => addItemToBasket(product)}
			>
				<i class="fa-solid fa-basket-shopping"></i>
			</button>
			<a class="btn btn-primary" href="/shop/{product.id}" title="Detail">
				<i class="fa-solid fa-info"></i>
			</a>
		</div>
	</div>

	<div class="card-body h-100 d-flex flex-column">
		<a href={'#'} class="text-decoration-none">
			<h5 class="text-uppercase">{product.name}</h5>
		</a>
		<span class="mb-2" style="font-size: 1.5em;">{currency(product.price)}</span>
	</div>
</div>

<style lang="scss">
	.btn {
		width: 30%;
		height: 40px;
	}

	.image :hover {
		cursor: pointer;
		opacity: 1;

		& button {
			transform: none;
			opacity: 1;
		}

		& a {
			transform: none;
			opacity: 1;
		}
	}

	.hover-overlay {
		position: absolute;
		width: 100%;
		height: 100%;
		top: 0;
		left: 0;
		background: rgba(255, 255, 255, 0.5);
		opacity: 0;
		transition: all 0.5s;

		& button {
			z-index: 1000;
			transition: all 0.5s;
		}

		& button:first-of-type {
			transform: translateX(-20px);
		}

		& a:last-of-type {
			transform: translateX(20px);
		}
	}
</style>
