<script>
	import ValidationFeedback from '$lib/shared/form/validation-feedback.svelte';

	/**
	 * @type {import("./form").AddressFormGroup}
	 */
	export let addressFormGroup;
</script>

<div class="col-12">
	<div class="col-12">
		<label for="full-name">Full Name</label>
		<input
			id="full-name"
			class="form-control rounded-1"
			placeholder="Example: John Doe"
			use:addressFormGroup.fullName.bind
			value={$addressFormGroup.fullName.value}
			class:is-invalid={$addressFormGroup.fullName.isTouched && !$addressFormGroup.fullName.isValid}
			class:is-valid={$addressFormGroup.fullName.isTouched && $addressFormGroup.fullName.isValid}
		/>
		<ValidationFeedback field={$addressFormGroup.fullName} />
	</div>
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
	<label for="email">Email</label>
	<input
		id="email"
		class="form-control rounded-1"
		placeholder="johndoe@gmail.com"
		use:addressFormGroup.email.bind
		value={$addressFormGroup.email.value}
		class:is-invalid={$addressFormGroup.email.isTouched && !$addressFormGroup.email.isValid}
		class:is-valid={$addressFormGroup.email.isTouched && $addressFormGroup.email.isValid}
	/>
	<ValidationFeedback field={$addressFormGroup.email} />
</div>

<div class="col-12">
	<label for="address">Address</label>
	<input
		id="address"
		class="form-control rounded-1"
		placeholder="1234 Main St"
		use:addressFormGroup.address.bind
		class:is-invalid={$addressFormGroup.address.isTouched && !$addressFormGroup.address.isValid}
		class:is-valid={$addressFormGroup.address.isTouched && $addressFormGroup.address.isValid}
	/>
	<ValidationFeedback field={$addressFormGroup.address} />
</div>

<div class="col-12">
	<label for="address2">
		Address 2 <span class="text-body-secondary">(Optional)</span>
	</label>
	<input
		id="address2"
		class="form-control rounded-1"
		placeholder="Apartment or suite"
		use:addressFormGroup.address2.bind
		class:is-invalid={$addressFormGroup.address2.isTouched && !$addressFormGroup.address2.isValid}
		class:is-valid={$addressFormGroup.address2.isTouched && $addressFormGroup.address2.isValid}
	/>
	<ValidationFeedback field={$addressFormGroup.address2} />
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
