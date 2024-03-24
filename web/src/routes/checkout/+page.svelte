<script>
	import { APP_NAME } from '$lib/core/constant';
	import { onMount } from 'svelte';
	import { startCase, toLower } from 'lodash';
	import OrderSummary from '$lib/order/order-summary.svelte';
	import { OrderAddressForm, OrderForm } from '$lib/order/form';
	import { basket, basketTotals, updateBasketAndBasketTotals } from '$lib/baskets/service';
	import { currency } from '$lib/core/helper';
	import { getDeliveryMethods, getPaymentTypes } from '$lib/order/request';
	import { getUserProfile } from '$lib/auth/request';
	import { goto } from '$app/navigation';
	import { notifySuccess } from '$lib/shared/toastr/service';
	import { createOrder } from '$lib/order/service';
	import Address from '$lib/order/address.svelte';
	import RadioField from '$lib/core/form/radio-field.svelte';
	import ReactiveFormField from '$lib/core/form/reactive.field';
	import PageLoader from '$lib/core/loader/page-loader.svelte';
	import Breadcrumb, {
		aliases,
		createBreadcrumbs,
		getLastBreadcrumbItem
	} from '$lib/core/breadcrumb.svelte';
	import { page } from '$app/stores';
	import ButtonLoader from '$lib/core/loader/button-loader.svelte';

	let hasSameAddress = true;
	let orderForm = new OrderForm();
	let disabled = false;

	$: if (!hasSameAddress) {
		orderForm.shippingAddress = new OrderAddressForm();
	} else {
		orderForm.shippingAddress = null;
	}

	async function onSubmitForm() {
		if ($basket === null) return;

		disabled = true;

		const response = await createOrder(orderForm, $basket);

		if (response.status === 200) {
			notifySuccess(`Create order successfully`);

			updateBasketAndBasketTotals();

			return goto('/order');
		}

		disabled = false;
	}

	onMount(async () => {
		const result = await getUserProfile();
		orderForm.billingAddress.fullName.value = result.fullName;
		orderForm.billingAddress.email.value = result.email;
	});

	$: breadcrumbs = createBreadcrumbs($page, $aliases);
	$: pageName = getLastBreadcrumbItem(breadcrumbs)?.name;
</script>

<svelte:head>
	<title>{APP_NAME} | {pageName}</title>
</svelte:head>

{#await Promise.all([getDeliveryMethods(), getPaymentTypes()])}
	<PageLoader />
{:then [deliveryMethods, paymentTypes]}
	<div class="container" style="margin-bottom: 7%;">
		<main>
			<div class="pt-5 pb-1">
				<Breadcrumb {breadcrumbs} class="breadcrumb" --bs-breadcrumb-divider="'>'" />
			</div>
			<div class="row g-5">
				<div class="col-md-5 col-lg-4 order-md-last">
					<OrderSummary basket={$basket} basketTotals={$basketTotals} />
				</div>
				<!-- promise was fulfilled -->
				<div class="col-md-7 col-lg-8">
					<form
						on:submit|preventDefault={onSubmitForm}
						action="POST"
						bind:this={orderForm.instance}
					>
						<div class="card">
							<div class="card-header">
								<h4 class="mb-0">Billing address</h4>
							</div>
							<div class="card-body px-4 pb-4">
								<div class="row g-3">
									<Address bind:addressForm={orderForm.billingAddress} bind:disabled />

									<div class="form-check mt-4 ms-2">
										<input
											type="checkbox"
											class="form-check-input"
											id="same-address"
											bind:checked={hasSameAddress}
											{disabled}
										/>
										<label class="form-check-label" for="same-address">
											Shipping address is the same as my billing address
										</label>
									</div>
								</div>
							</div>
						</div>

						{#if !hasSameAddress && orderForm.shippingAddress}
							<div class="card my-5">
								<div class="card-header">
									<h4 class="mb-0">Shipping address</h4>
								</div>
								<div class="card-body px-4 pb-4">
									<div class="row g-3">
										<Address bind:addressForm={orderForm.shippingAddress} bind:disabled />
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
											<RadioField
												class="form-check-input"
												value={method.id}
												bind:reactiveFormField={orderForm.deliveryMethodId}
												bind:disabled
											>
												<svelte:fragment slot="label">
													<label class="form-check-label" for={method.shortName}>
														{method.shortName} - {currency(method.price)} ({method.deliveryTime})
													</label>
												</svelte:fragment>
											</RadioField>
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
											<RadioField
												class="form-check-input"
												value={type}
												reactiveFormField={new ReactiveFormField()}
												bind:disabled
											>
												<svelte:fragment slot="label">
													<label class="form-check-label" for={type}>
														{startCase(toLower(type.split('_').join(' ')))}
													</label>
												</svelte:fragment>
											</RadioField>
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

						<button
							class="w-100 btn btn-primary btn-lg"
							type="submit"
							disabled={!$orderForm.valid || !Boolean($basket) || disabled}
						>
							Continue to checkout
							{#if disabled}
								<ButtonLoader />
							{/if}
						</button>
					</form>
				</div>
			</div>
		</main>
	</div>
{/await}
