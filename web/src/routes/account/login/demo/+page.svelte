<script>
	import { loginAsUser } from '$lib/auth/service';
	import ButtonLoader from '$lib/shared/spinner/button-loader.svelte';

	let isSubmitting = false;
	/**
	 * @type {string | undefined}
	 */
	let accountSubmitting = undefined;

	/**
	 * @param {string} accountName
	 */
	async function loginAs(accountName) {
		try {
			isSubmitting = true;
			accountSubmitting = accountName;
			const loginForm = {
				email: `${accountName}@test.com`,
				password: 'Pa$$w0rd'
			};

			await loginAsUser(loginForm);
		} catch (error) {
			console.log(error);
		} finally {
			isSubmitting = false;
			accountSubmitting = undefined;
		}
	}
</script>

<div class="col-12 mb-3">
	<p class="small mb-3 text-center">
		Don't have account? <a href="/account/register" class="text-decoration-none">
			Create an account
		</a>
	</p>
	<p class="small mb-3 text-center">
		Already have an account? <a href="/account/login" class="text-decoration-none">Log in</a>
	</p>
</div>

<div class="col-12">
	<div class="d-flex mb-2">
		<div class="border-secondary-subtle w-100 flex-shrink-1">
			<hr />
		</div>
	</div>
	<div class="d-grid gap-3">
		<button
			class="btn btn-outline-info rounded-4"
			type="submit"
			disabled={isSubmitting}
			on:click={() => loginAs('customer')}
		>
			Demo Customer
			{#if isSubmitting && accountSubmitting === 'customer'}
				<ButtonLoader />
			{/if}
		</button>
		<button
			class="btn btn-outline-info rounded-4"
			type="submit"
			disabled={isSubmitting}
			on:click={() => loginAs('customer1')}
		>
			Demo Customer1
			{#if isSubmitting && accountSubmitting === 'customer1'}
				<ButtonLoader />
			{/if}
		</button>
	</div>
</div>
