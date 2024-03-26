<script>
	import TextField from '$lib/component/form/text-field.svelte';

	/**
	 * @type {import("./order.address.form").default}
	 */
	export let addressForm;

	export let disabled = false;

	function getCountries() {
		return ['United States', 'Canada', 'Australia'];
	}

	/**
	 * @param {string} country
	 */
	function getCountryStates(country) {
		/**
		 * @type {{[country: string]: string[]}}
		 */
		const countryStates = {
			'United States': ['California', 'Texas', 'Florida'],
			Canada: ['Ontario', 'Quebec', 'Alberta'],
			Australia: ['Victoria', 'New South Wales', 'Queensland']
		};
		return countryStates[country] ?? [];
	}

	/**
	 * @param {string} countryState
	 */
	function getCities(countryState) {
		/**
		 * @type {{[state: string]: string[]}}
		 */
		const citiesByStates = {
			California: ['Los Angeles', 'Sacramento'],
			Texas: ['Austin', 'Houston'],
			Florida: ['Tallahassee', 'Jacksonville'],
			Ontario: ['Toronto', 'Ottawa'],
			Quebec: ['Quebec City', 'Montreal'],
			Alberta: ['Edmonton', 'Calgary'],
			Victoria: ['Melbourne', 'Geelong'],
			'New South Wales': ['Sydney', 'Wollongong'],
			Queensland: ['Brisbane', 'Mackay']
		};
		return citiesByStates[countryState] ?? [];
	}
</script>

<div class="col-12">
	<TextField
		class="form-control rounded-1"
		name="fullName"
		placeholder="John Doe"
		validationFeedback={true}
		bind:reactiveFormField={addressForm.fullName}>
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
		bind:disabled>
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
		bind:disabled>
		<svelte:fragment slot="label">
			<label for="address">Address</label>
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
		bind:disabled>
		<svelte:fragment slot="label">
			<label for="address2">
				Address 2 <span class="text-body-secondary">(Optional)</span>
			</label>
		</svelte:fragment>
	</TextField>
</div>

<div class="col-md-5">
	<label for="country" class="form-label">Country</label>
	<select
		class="form-select"
		id="country"
		bind:value={addressForm.country.value}
		bind:this={addressForm.country.instance}
		on:change={() => {
			addressForm.state.value = null;
			addressForm.city.value = null;
			addressForm.zipCode.value = null;
		}}>
		{#each getCountries() as country}
			<option value={country}>{country}</option>
		{/each}
	</select>
</div>

<div class="col-md-4">
	<label for="state" class="form-label">State</label>
	<select
		class="form-select"
		id="state"
		bind:value={addressForm.state.value}
		bind:this={addressForm.state.instance}>
		{#each getCountryStates(addressForm.country.value) as state}
			<option value={state}>{state}</option>
		{/each}
	</select>
</div>

<div class="col-md-3">
	<label for="city" class="form-label">City</label>
	<select
		class="form-select"
		id="city"
		bind:value={addressForm.city.value}
		bind:this={addressForm.city.instance}>
		{#each getCities(addressForm.state.value) as city}
			<option value={city}>{city}</option>
		{/each}
	</select>
</div>

<div class="col-12">
	<label for="zip" class="form-label">Zip</label>
	<input
		type="text"
		class="form-control"
		id="zip"
		bind:value={addressForm.zipCode.value}
		bind:this={addressForm.zipCode.instance} />
</div>
