<script>
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import { PUBLIC_DEMO_CUSTOMER_PWD } from '$env/static/public';
	import { loginAsUserDemo } from '$lib/auth/service';
	import ButtonLoader from '$lib/shared/spinner/button-loader.svelte';
	import { notifySuccess } from '$lib/shared/toastr/service';

	let isSubmitting = false;
	/**
	 * @type {string | undefined}
	 */
	let accountSubmitting = undefined;

	/**
	 * @param {string} accountName
	 */
	async function loginAs(accountName) {
		isSubmitting = true;
		accountSubmitting = accountName;

		const loginDTO = {
			email: `demo.${accountName}@test.com`,
			password: `${PUBLIC_DEMO_CUSTOMER_PWD}`
		};

		const displayName = await loginAsUserDemo(loginDTO);

		if (displayName) {
			notifySuccess(`Welcome back ${displayName}`);

			const returnUrl = $page.url.searchParams.get('returnUrl');
			if (returnUrl) return goto(returnUrl);

			return goto('/');
		}

		accountSubmitting = undefined;
		isSubmitting = false;
	}
</script>

<div class="col-12 mb-3">
	<p class="small mb-3 text-center">
		Don't have account? <a href="/account/register{$page.url.search}" class="text-decoration-none">
			Create an account
		</a>
	</p>
	<p class="small mb-3 text-center">
		Already have an account? <a
			href="/account/login{$page.url.search}"
			class="text-decoration-none"
		>
			Log in
		</a>
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
