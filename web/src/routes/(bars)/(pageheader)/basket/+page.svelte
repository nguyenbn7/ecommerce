<script>
	import OrderTotals from '$lib/core/basket/order-totals.svelte';
	import { addItemToBasket, basket, removeItemFromBasket } from '$lib/core/basket/service';
	import { currency } from '$lib/shared/service';
</script>

{#if !$basket?.items.length}
	<div>
		<p>There are no items in your basket</p>
	</div>
{:else}
	<div class="container px-0 my-5">
		<div class="row">
			<div class="table-responsive">
				<table class="table">
					<thead class="table-light text-uppercase">
						<tr>
							<th>
								<div class="py-2">Product</div>
							</th>
							<th>
								<div class="py-2">Price</div>
							</th>
							<th>
								<div class="py-2">Quantity</div>
							</th>
							<th>
								<div class="py-2">Total</div>
							</th>
							<th>
								<div class="py-2">Remove</div>
							</th>
						</tr>
					</thead>
					<tbody>
						{#each $basket.items as item}
							<tr>
								<th>
									<div class="p-2 d-inline-block">
										<img
											src={item.pictureUrl}
											alt={item.productName}
											class="img-fluid"
											style="max-height: 50px;"
										/>
									</div>
									<div class="ms-3 d-inline-block align-middle">
										<h5 class="mb-0">
											<a href="/shop/{item.id}" class="text-dark text-decoration-none">
												{item.productName}
											</a>
										</h5>
										<span class="text-muted fst-italic"> Type: {item.type} </span>
									</div>
								</th>
								<td class="align-middle">
									<strong>{currency(item.price)}</strong>
								</td>
								<td class="align-middle">
									<div class="d-flex align-items-center">
										<button
											class="p-0 m-0 me-2 border-0 quantity-btn"
											on:click={() => removeItemFromBasket(item.id, 1)}
											disabled={item.quantity < 1}
										>
											<i class="fa-solid fa-circle-minus"></i>
										</button>
										<strong class="fw-semibold" style="font-size: 1.5em;">{item.quantity}</strong>
										<button
											class="p-0 m-0 ms-2 border-0 quantity-btn"
											on:click={() => addItemToBasket(item, 1)}
										>
											<i class="fa-solid fa-circle-plus"></i>
										</button>
									</div>
								</td>
								<td class="align-middle">
									<strong>{currency(item.price * item.quantity)}</strong>
								</td>
								<td class="align-middle">
									<a
										class="text-danger"
										href={'javascript:;'}
										on:click={() => removeItemFromBasket(item.id, item.quantity)}
									>
										<i class="fa-solid fa-x remove-btn"></i>
									</a>
								</td>
							</tr>
						{/each}
					</tbody>
				</table>
			</div>
		</div>
	</div>
{/if}

<div class="row">
	<div class="col-6 offset-6">
		<OrderTotals />
		<div class="d-grid">
			<a
				href="/checkout"
				class="btn py-2"
				class:btn-outline-primary={!$basket || ($basket && !$basket.items.length)}
				class:disabled={!$basket || ($basket && !$basket.items.length)}
				class:btn-primary={$basket && $basket.items.length}
			>
				Proceed to checkout
			</a>
		</div>
	</div>
</div>

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

	.remove-btn {
		font-size: 1.5rem;
		cursor: pointer;
	}
</style>
