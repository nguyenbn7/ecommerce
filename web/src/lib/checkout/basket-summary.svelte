<script>
	import { currency } from '$lib/service';

	/**
	 * @type {Basket | null}
	 */
	export let basket;
	/**
	 * @type {BasketTotals | null}
	 */
	export let basketTotals;
</script>

<div class="card">
	<div class="card-header">
		<h4 class="d-flex justify-content-between align-items-center mb-0">
			<span class="text-primary">Order Summary</span>
			<a href="/basket" class="fs-5">Edit</a>
		</h4>
	</div>
	<div class="card-body p-0">
		<ul class="list-group">
			{#if basket}
				{#each basket.items as item}
					<li class="list-group-item d-flex justify-content-between align-items-center lh-sm">
						<div class="row align-items-center">
							<div class="col-3 p-1 position-relative">
								<img src={item.pictureUrl} class="img-thumbnail" alt={item.productName} />
								<span
									class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-secondary">
									{item.quantity}
								</span>
							</div>
							<div class="col-8 ms-1">
								<h5 class="my-0">{item.productName}</h5>
							</div>
						</div>
						<span class="text-body-secondary">{currency(item.price * item.quantity)}</span>
					</li>
				{/each}
			{/if}
			<li class="list-group-item d-flex justify-content-between">
				<span>Total (USD)</span>
				<strong>{currency(basketTotals?.total ?? 0)}</strong>
			</li>
		</ul>
	</div>
</div>
