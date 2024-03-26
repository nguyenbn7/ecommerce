<script>
	import { page } from '$app/stores';
	import { goto } from '$app/navigation';
	import { APP_NAME } from '$lib/constant';
	import { loginAsCustomer } from '$lib/core/auth/service';
	import { showSuccess } from '$lib/component/toastr.svelte';
	import LoginForm from '$lib/login/form';
	import TextField from '$lib/component/form/text-field.svelte';
	import PasswordField from '$lib/component/form/password-field.svelte';
	import ButtonLoader from '$lib/component/spinner/button-loader.svelte';

	let loginForm = new LoginForm();
	let disabled = false;

	async function login() {
		disabled = true;

		const displayName = await loginAsCustomer({
			email: loginForm.email.value,
			password: loginForm.password.value
		});

		if (!displayName) {
			disabled = false;

			return;
		}
		showSuccess(`Welcome back ${displayName}`);

		const returnUrl = $page.url.searchParams.get('next');
		if (returnUrl) return goto(returnUrl);

		return goto('/');
	}
</script>

<svelte:head>
	<title>{APP_NAME} | Sign In</title>
</svelte:head>

<div class="py-1 mb-4">
	<h5 class="card-title text-center pb-0 fs-4">Login to Your Account</h5>
</div>

<form class="row g-3 px-1 needs-validation" on:submit={login} bind:this={loginForm.instance}>
	<!-- Email -->
	<div class="col-12">
		<div class="form-floating">
			<TextField
				class="form-control rounded-3"
				name="email"
				inputAbove={true}
				placeholder="john.doe@test.com"
				validationFeedback={true}
				bind:reactiveFormField={loginForm.email}
				bind:disabled>
				<svelte:fragment slot="label">
					<label for="email">Email</label>
				</svelte:fragment>
			</TextField>
		</div>
	</div>

	<!-- Password -->
	<div class="col-12">
		<div class="form-floating">
			<PasswordField
				class="form-control rounded-3"
				name="password"
				inputAbove={true}
				placeholder="*********"
				validationFeedback={true}
				bind:reactiveFormField={loginForm.password}
				{disabled}>
				<svelte:fragment slot="label">
					<label for="password">Password</label>
				</svelte:fragment>
			</PasswordField>
		</div>
	</div>

	<div class="col-12">
		<button
			class="btn btn-info w-100 py-2 mt-2 mb-3 rounded-4"
			type="submit"
			disabled={!$loginForm.valid || disabled}>
			Login
			{#if disabled}
				<ButtonLoader />
			{/if}
		</button>
	</div>

	<div class="col-12 mb-3">
		<p class="small mb-3 text-center">
			Don't have account? <a
				href="/account/register{$page.url.search}"
				class="text-decoration-none">
				Create an account
			</a>
		</p>
		<p class="small mb-0 text-center">
			Sign in as <a href="/account/login/demo{$page.url.search}" class="text-decoration-none">
				Demo Users
			</a>
		</p>
	</div>
</form>
