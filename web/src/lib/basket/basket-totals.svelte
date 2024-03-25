<script>
	import { basketTotals } from '$lib/core/basket/service';
	import { currency } from '$lib/service';
</script>

<div class="bg-light px-4 py-3 text-uppercase fw-bold">Order Summary</div>

<div class="p-4">
	<p class="fst-italic mb-4">
		Shipping costs will be calculated based on choices made during checkout
	</p>
	{#if $basketTotals}
		<ul class="list-unstyled mb-4">
			<li class="d-flex justify-content-between py-3 border-bottom">
				<strong class="text-muted">Order Subtotal</strong>
				<strong>{currency($basketTotals.subtotal)}</strong>
			</li>
			<li class="d-flex justify-content-between py-3 border-bottom">
				<strong class="text-muted">Shipping and Handling</strong>
				<strong>{currency($basketTotals.shipping)}</strong>
			</li>
			<li class="d-flex justify-content-between py-3 border-bottom">
				<strong class="text-muted">Total</strong>
				<strong>{currency($basketTotals.total)}</strong>
			</li>
		</ul>
	{/if}
</div>
