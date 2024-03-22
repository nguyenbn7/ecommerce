<script>
	import ValidationFeedback from '$lib/shared/form/validation-feedback.svelte';

	/**
	 * @type {import("./form").AddressFormGroup}
	 */
	export let addressForm;
</script>

<div class="col-12" use:addressForm.bind>
	<div class="col-12">
		<label for="full-name">Full Name</label>
		<input
			id="full-name"
			class="form-control rounded-1"
			placeholder="Example: John Doe"
			use:addressForm.fullName.bind
			class:is-invalid={$addressForm.fullName.isTouched && !$addressForm.fullName.isValid}
			class:is-valid={$addressForm.fullName.isTouched && $addressForm.fullName.isValid}
		/>
		<ValidationFeedback field={$addressForm.fullName} />
	</div>
</div>

<!-- <div class="col-12">
	<label for="phone">Phone Number</label>
	<input
		id="phone"
		class="form-control rounded-1"
		placeholder="+1 (999) 999-9999"
		use:addressForm.phoneNumber.bind
		class:is-invalid={$addressForm.phoneNumber.isTouched && !$addressForm.phoneNumber.isValid}
		class:is-valid={$addressForm.phoneNumber.isTouched && $addressForm.phoneNumber.isValid}
	/>
	<ValidationFeedback field={$addressForm.phoneNumber} />
</div> -->

<div class="col-12">
	<label for="email">Email</label>
	<input
		id="email"
		class="form-control rounded-1"
		placeholder="johndoe@gmail.com"
		use:addressForm.email.bind
		class:is-invalid={$addressForm.email.isTouched && !$addressForm.email.isValid}
		class:is-valid={$addressForm.email.isTouched && $addressForm.email.isValid}
	/>
	<ValidationFeedback field={$addressForm.email} />
</div>

<div class="col-12">
	<label for="address">Address</label>
	<input
		id="address"
		class="form-control rounded-1"
		placeholder="1234 Main St"
		use:addressForm.address.bind
		class:is-invalid={$addressForm.address.isTouched && !$addressForm.address.isValid}
		class:is-valid={$addressForm.address.isTouched && $addressForm.address.isValid}
	/>
	<ValidationFeedback field={$addressForm.address} />
</div>

<div class="col-12">
	<label for="address2">
		Address 2 <span class="text-body-secondary">(Optional)</span>
	</label>
	<input
		id="address2"
		class="form-control rounded-1"
		placeholder="Apartment or suite"
		use:addressForm.address2.bind
		class:is-invalid={$addressForm.address2.isTouched && !$addressForm.address2.isValid}
		class:is-valid={$addressForm.address2.isTouched && $addressForm.address2.isValid}
	/>
	<ValidationFeedback field={$addressForm.address2} />
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
