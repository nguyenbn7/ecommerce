<script>
	import { onMount } from 'svelte';
	import { startCase, toLower } from 'lodash';
	import { OrderService, basket, basketTotals } from '$lib/share/service';
	import { APP_NAME } from '$lib/share/constant';
	import OrderSummary from '$lib/component/(root)/(no-navbar-footer)/checkout/order-summary.svelte';
	import Address, {
		AddressFormGroup
	} from '$lib/component/(root)/(no-navbar-footer)/checkout/address.svelte';
	import { currency } from '$lib/share/helper';

	let hasSameAddress = true;
	/**
	 * @type {string[]}
	 */
	let paymentTypes = [];
	/**
	 * @type {DeliveryMethod[]}
	 */
	let deliveryMethods = [];

	let orderForm = new OrderFormGroup();

	/**
	 * @type {string}
	 */
	let paymentType;
	/**
	 * @type {number}
	 */
	let deliveryMethodId;

	/**
	 * @param {Event} $event
	 */
	function onChangePaymentType($event) {
		/**
		 * @type {HTMLInputElement}
		 */
		// @ts-ignore
		const target = $event.target;
		paymentType = target.value;
	}

	/**
	 * @param {Event} $event
	 */
	function onChangeDeliveryMethod($event) {
		/**
		 * @type {HTMLInputElement}
		 */
		// @ts-ignore
		const target = $event.target;
		deliveryMethodId = Number(target.value);
	}

	async function onSubmitForm() {
		if ($basket === null) return;

		/**
		 * @type {Order}
		 */
		const order = {};

		order.basketId = $basket?.id ?? '';
		/**
		 * @type {OrderAddress}
		 */
		order.billingAddress = {
			fullName: orderForm.billingAddress.fullName.value ?? '',
			email: orderForm.billingAddress.email.value ?? '',
			phoneNumber: orderForm.billingAddress.phoneNumber.value ?? '',
			address: orderForm.billingAddress.address.value ?? '',
			address2: orderForm.billingAddress.address2.value ?? '',
			country: 'USA',
			state: 'Texas',
			zipCode: '74494'
		};

		if (!hasSameAddress) {
			order.shippingAddress = {
				fullName: orderForm.shippingAddress.fullName.value,
				email: orderForm.shippingAddress.email.value,
				phoneNumber: orderForm.shippingAddress.phoneNumber.value,
				address: orderForm.shippingAddress.address.value,
				address2: orderForm.shippingAddress.address.value,
				country: 'USA',
				state: 'Texas',
				zipCode: '74494'
			};
		}

		order.paymentType = paymentType;
		order.deliveryMethodId = deliveryMethodId;

		await OrderService.createOrder(order);
	}
    
	onMount(async () => {
		const result = await Promise.all([
			OrderService.getPaymentTypes(),
			OrderService.getDeliveryMethods()
		]);
		paymentTypes = [...result[0]];
		deliveryMethods = [...result[1]];
	});
</script>

<svelte:head>
	<title>{APP_NAME} | Checkout</title>
</svelte:head>

<div class="container" style="margin-bottom: 7%;">
	<main>
		<div class="py-5 text-center">
			<a href="/">
				<img class="d-block mx-auto mb-4" src="/images/logo.png" alt="" width="250" height="100" />
			</a>
			<h2>Checkout</h2>
		</div>

		<div class="row g-5">
			<div class="col-md-5 col-lg-4 order-md-last">
				<OrderSummary basket={$basket} basketTotals={$basketTotals} />
			</div>
			<div class="col-md-7 col-lg-8">
				<form on:submit={onSubmitForm}>
					<div class="card">
						<div class="card-header">
							<h4 class="mb-0">Billing address</h4>
						</div>
						<div class="card-body px-4 pb-4">
							<div class="row g-3">
								<Address bind:addressForm={orderForm.billingAddress}></Address>

								<div class="form-check mt-4 ms-2">
									<input
										type="checkbox"
										class="form-check-input"
										id="same-address"
										bind:checked={hasSameAddress}
									/>
									<label class="form-check-label" for="same-address">
										Shipping address is the same as my billing address
									</label>
								</div>
							</div>
						</div>
					</div>

					{#if !hasSameAddress}
						<div class="card my-5">
							<div class="card-header">
								<h4 class="mb-0">Shipping address</h4>
							</div>
							<div class="card-body px-4 pb-4">
								<div class="row g-3">
									<Address addressForm={orderForm.shippingAddress} />
								</div>
							</div>
						</div>
					{/if}

					<div class="card my-5">
						<div class="card-header">
							<h4 class="mb-0">Delivery</h4>
						</div>
						<div class="card-body px-4 pb-4">
							<div class="row g-3">
								{#each deliveryMethods as method}
									<div class="form-check">
										<input
											type="radio"
											class="form-check-input"
											value={method.id}
											id={method.short_name}
											checked={deliveryMethodId === method.id}
											on:change={onChangeDeliveryMethod}
										/>
										<label class="form-check-label" for={method.short_name}>
											{method.short_name} - {currency(method.price)} ({method.delivery_time})
										</label>
									</div>
								{/each}
							</div>
						</div>
					</div>

					<div class="card my-5">
						<div class="card-header">
							<h4 class="mb-0">Payment</h4>
						</div>
						<div class="card-body px-4 pb-4">
							<div class="my-3">
								{#each paymentTypes as type}
									<div class="form-check">
										<input
											type="radio"
											class="form-check-input"
											value={type}
											id={type}
											checked={paymentType === type}
											on:change={onChangePaymentType}
										/>
										<label class="form-check-label" for={type}>
											{startCase(toLower(type.split('_').join(' ')))}
										</label>
									</div>
								{/each}
							</div>
							<!-- TODO: -->
							<div class="row gy-3">
								<div class="col-md-6">
									<label for="cc-name" class="form-label">Name on card</label>
									<input type="text" class="form-control" id="cc-name" placeholder="" />
									<small class="text-body-secondary">Full name as displayed on card</small>
									<div class="invalid-feedback">Name on card is required</div>
								</div>

								<div class="col-md-6">
									<label for="cc-number" class="form-label">Credit card number</label>
									<input type="text" class="form-control" id="cc-number" placeholder="" />
									<div class="invalid-feedback">Credit card number is required</div>
								</div>

								<div class="col-md-3">
									<label for="cc-expiration" class="form-label">Expiration</label>
									<input type="text" class="form-control" id="cc-expiration" placeholder="" />
									<div class="invalid-feedback">Expiration date required</div>
								</div>

								<div class="col-md-3">
									<label for="cc-cvv" class="form-label">CVV</label>
									<input type="text" class="form-control" id="cc-cvv" placeholder="" />
									<div class="invalid-feedback">Security code required</div>
								</div>
							</div>
						</div>
					</div>

					<button class="w-100 btn btn-primary btn-lg" type="submit" disabled={!orderForm.valid}>
						Continue to checkout
					</button>
				</form>
			</div>
		</div>
	</main>
</div>
