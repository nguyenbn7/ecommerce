<script>
	async function onLoginCustomer() {
		await handleLogin({
			email: 'customer@test.com',
			password: 'Pa$$w0rd'
		});
	}

	async function onLoginCustomer1() {
		await handleLogin({
			email: 'customer1@test.com',
			password: 'Pa$$w0rd'
		});
	}
</script>

<div class="col-12">
	<div class="d-flex mb-2">
		<div class="border-secondary-subtle w-100 flex-shrink-1">
			<hr />
		</div>
		<span class="text-center text-nowrap text-secondary">or sign in as</span>
		<div class="border-secondary-subtle w-100 flex-shrink-1">
			<hr />
		</div>
	</div>
	<div class="row row-cols-2 g-2">
		<div class="col d-flex justify-content-center">
			<button
				class="btn btn-outline-info rounded-4"
				type="submit"
				on:click={onLoginCustomer}
				disabled={isSubmitted}
			>
				Demo Customer
				<!-- {#if isSubmitted}
                    <ButtonSpinner />
                {/if} -->
			</button>
		</div>
		<div class="col d-flex justify-content-center">
			<button
				class="btn btn-outline-info rounded-4"
				type="submit"
				on:click={onLoginCustomer1}
				disabled={isSubmitted}
			>
				Demo Customer1
				<!-- {#if isSubmitted}
                    <ButtonSpinner />
                {/if} -->
			</button>
		</div>
	</div>
</div>
