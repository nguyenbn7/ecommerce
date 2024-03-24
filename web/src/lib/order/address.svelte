<script>
	import TextField from '$lib/core/form/text-field.svelte';

	/**
	 * @type {import("./form").OrderAddressForm}
	 */
	export let addressForm;
</script>

<div class="col-12">
	<TextField
		class="form-control rounded-1"
		name="fullName"
		placeholder="John Doe"
		validationFeedback={true}
		bind:reactiveFormField={addressForm.fullName}
	>
		<svelte:fragment slot="label">
			<label for="fullName">Full Name</label>
		</svelte:fragment>
	</TextField>
</div>

<!-- <div class="col-12">
	<label for="phone">Phone Number</label>
	<input
		id="phone"
		class="form-control rounded-1"
		placeholder="+1 (999) 999-9999"
		use:addressForm.phoneNumber.bind
		class:is-invalid={addressForm.phoneNumber.isTouched && !addressForm.phoneNumber.isValid}
		class:is-valid={addressForm.phoneNumber.isTouched && addressForm.phoneNumber.isValid}
	/>
	<ValidationFeedback field={addressForm.phoneNumber} />
</div> -->

<div class="col-12">
	<TextField
		class="form-control rounded-1"
		name="email"
		placeholder="johndoe@gmail.com"
		validationFeedback={true}
		bind:reactiveFormField={addressForm.email}
	>
		<svelte:fragment slot="label">
			<label for="email">Email</label>
		</svelte:fragment>
	</TextField>
</div>

<div class="col-12">
	<TextField
		class="form-control rounded-1"
		name="address"
		placeholder="1234 Main St"
		validationFeedback={true}
		bind:reactiveFormField={addressForm.address}
	>
		<svelte:fragment slot="label">
			<label for="address">Full Name</label>
		</svelte:fragment>
	</TextField>
</div>

<div class="col-12">
	<TextField
		class="form-control rounded-1"
		name="address2"
		placeholder="Apartment or suite"
		validationFeedback={true}
		bind:reactiveFormField={addressForm.address2}
	>
		<svelte:fragment slot="label">
			<label for="address2">
				Address 2 <span class="text-body-secondary">(Optional)</span>
			</label>
		</svelte:fragment>
	</TextField>
</div>

<!-- TODO: Validate Country -->
<div class="col-md-5">
	<label for="country" class="form-label">Country</label>
	<select class="form-select" id="country">
		<option value="">Choose...</option>
		<option>United States</option>
	</select>
	<div class="invalid-feedback">Please select a valid country.</div>
</div>

<div class="col-md-4">
	<label for="state" class="form-label">State</label>
	<select class="form-select" id="state">
		<option value="">Choose...</option>
		<option>California</option>
	</select>
	<div class="invalid-feedback">Please provide a valid state.</div>
</div>

<div class="col-md-3">
	<label for="zip" class="form-label">Zip</label>
	<input type="text" class="form-control" id="zip" placeholder="" />
	<div class="invalid-feedback">Zip code required.</div>
</div>
