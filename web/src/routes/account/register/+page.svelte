<script>
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import TextField from '$lib/core/form/text-field.svelte';
	import { registerAsCustomer } from '$lib/core/auth/service';
	import PasswordField from '$lib/core/form/password-field.svelte';
	import { APP_NAME } from '$lib/shared/constant';
	import { notifySuccess } from '$lib/shared/toastr/service';
	import { RegisterForm } from '$lib/component/register/form';
	import ButtonLoader from '$lib/component/button-loader.svelte';

	let registerForm = new RegisterForm();
	let disabled = false;
	$: returnUrl = $page.url.searchParams.get('next');

	async function register() {
		disabled = true;

		const displayName = await registerAsCustomer({
			email: registerForm.email.value,
			displayName: registerForm.displayName.value,
			fullName: registerForm.fullName.value,
			password: registerForm.password.value,
			confirmPassword: registerForm.confirmPassword.value
		});

		if (!displayName) {
			disabled = false;
			return;
		}

		notifySuccess(`Welcome ${displayName}`);

		if (returnUrl) return goto(returnUrl);

		return goto('/');
	}
</script>

<svelte:head>
	<title>{APP_NAME} | Sign Up</title>
</svelte:head>

<div class="pt-4 pb-2">
	<h5 class="card-title text-center pb-0 fs-4">Create an Account</h5>
	<p class="text-center small">Enter your personal details to create account</p>
</div>

<form class="row g-3 px-1" on:submit={register} bind:this={registerForm.instance}>
	<div class="col-12">
		<div class="form-floating">
			<TextField
				class="form-control rounded-3"
				name="fullName"
				inputAbove={true}
				placeholder="John Doe"
				validationFeedback={true}
				bind:reactiveFormField={registerForm.fullName}
				bind:disabled
			>
				<svelte:fragment slot="label">
					<label for="fullName">Full Name</label>
				</svelte:fragment>
			</TextField>
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating">
			<TextField
				class="form-control rounded-3"
				name="displayName"
				inputAbove={true}
				placeholder="John"
				validationFeedback={true}
				bind:reactiveFormField={registerForm.displayName}
				bind:disabled
			>
				<svelte:fragment slot="label">
					<label for="displayName">Full Name</label>
				</svelte:fragment>
			</TextField>
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating">
			<TextField
				class="form-control rounded-3"
				name="email"
				inputAbove={true}
				placeholder="john.doe@example.com"
				validationFeedback={true}
				bind:reactiveFormField={registerForm.email}
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
				bind:reactiveFormField={registerForm.password}
				{disabled}
			>
				<svelte:fragment slot="label">
					<label for="password">Password</label>
				</svelte:fragment>
			</PasswordField>
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating mt-2 mb-3">
			<PasswordField
				class="form-control rounded-3"
				name="confirmPassword"
				inputAbove={true}
				placeholder="*********"
				validationFeedback={true}
				bind:reactiveFormField={registerForm.confirmPassword}
				{disabled}
			>
				<svelte:fragment slot="label">
					<label for="confirmPassword">Confirm Password</label>
				</svelte:fragment>
			</PasswordField>
		</div>
	</div>

	<div class="col-12">
		<div class="form-check">
			<input
				class="form-check-input"
				name="terms"
				type="checkbox"
				bind:this={registerForm.agree.instance}
				bind:checked={registerForm.agree.value}
				id="acceptTerms"
			/>
			<label class="form-check-label" for="acceptTerms">
				I agree and accept the <a href={'javascript:;'} class="text-decoration-none">
					terms and conditions
				</a>
			</label>
			<!-- TODO:  -->
			<div class="invalid-feedback">You must agree before submitting.</div>
		</div>
	</div>
	<div class="col-12">
		<button
			class="btn btn-info w-100 py-2 mt-2 mb-3 rounded-4"
			type="submit"
			disabled={!$registerForm.valid || disabled}
		>
			Create Account
			{#if disabled}
				<ButtonLoader />
			{/if}
		</button>
	</div>
	<div class="col-12">
		<p class="small mb-3 text-center">
			Already have an account? <a
				href="/account/login{$page.url.search}"
				class="text-decoration-none"
			>
				Log in
			</a>
		</p>
	</div>
</form>
