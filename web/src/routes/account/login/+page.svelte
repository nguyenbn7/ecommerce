<script>
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import { loginAsCustomer } from '$lib/core/auth/service';
	import PasswordField from '$lib/core/form/password-field.svelte';
	import TextField from '$lib/core/form/text-field.svelte';
	import { APP_NAME } from '$lib/shared/constant';
	import { LoginForm } from '$lib/component/login/form';
	import { notifySuccess } from '$lib/shared/toastr/service';
	import ButtonLoader from '$lib/component/button-loader.svelte';

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
		notifySuccess(`Welcome back ${displayName}`);

		const returnUrl = $page.url.searchParams.get('next');
		if (returnUrl) return goto(returnUrl);

		return goto('/');
	}
</script>

<svelte:head>
	<title>{APP_NAME} | Sign In</title>
</svelte:head>

<div class="pt-4 pb-2 mb-4">
	<h5 class="card-title text-center pb-0 fs-4">Login to Your Account</h5>
</div>

<form class="row g-3 px-1 needs-validation" on:submit={login} bind:this={loginForm.instance}>
	<div class="col-12">
		<div class="form-floating">
			<TextField
				class="form-control rounded-3"
				name="email"
				inputAbove={true}
				placeholder="name@example.com"
				validationFeedback={true}
				bind:reactiveFormField={loginForm.email}
				bind:disabled
			>
				<svelte:fragment slot="label">
					<label for="email">Email</label>
				</svelte:fragment>
			</TextField>
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating">
			<PasswordField
				class="form-control rounded-3"
				name="password"
				inputAbove={true}
				placeholder="*********"
				validationFeedback={true}
				bind:reactiveFormField={loginForm.password}
				{disabled}
			>
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
			disabled={!$loginForm.valid || disabled}
		>
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
				class="text-decoration-none"
			>
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
