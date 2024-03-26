<script>
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import { APP_NAME } from '$lib/constant';
	import { showSuccess } from '$lib/component/toastr.svelte';
	import { registerAsCustomer } from '$lib/core/auth/service';
	import RegisterForm from '$lib/register/form';
	import TextField from '$lib/component/form/text-field.svelte';
	import PasswordField from '$lib/component/form/password-field.svelte';
	import ButtonLoader from '$lib/component/spinner/button-loader.svelte';

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

		showSuccess(`Welcome ${displayName}`);

		if (returnUrl) return goto(returnUrl);

		return goto('/', { invalidateAll: true });
	}
</script>

<svelte:head>
	<title>{APP_NAME} | Sign Up</title>
</svelte:head>

<div class="p-2">
	<h5 class="card-title text-center pb-0 fs-4">Create an Account</h5>
	<p class="text-center small">Enter your personal details to create account</p>
</div>

<form class="row g-3 px-1" on:submit={register} bind:this={registerForm.instance}>
	<!-- Full name -->
	<div class="col-12">
		<div class="form-floating">
			<TextField
				class="form-control rounded-3"
				name="fullName"
				inputAbove={true}
				placeholder="John Doe"
				validationFeedback={true}
				bind:reactiveFormField={registerForm.fullName}
				bind:disabled>
				<svelte:fragment slot="label">
					<label for="fullName">Full Name</label>
				</svelte:fragment>
			</TextField>
		</div>
	</div>

	<!-- Display name -->
	<div class="col-12">
		<div class="form-floating">
			<TextField
				class="form-control rounded-3"
				name="displayName"
				inputAbove={true}
				placeholder="John"
				validationFeedback={true}
				bind:reactiveFormField={registerForm.displayName}
				bind:disabled>
				<svelte:fragment slot="label">
					<label for="displayName">Full Name</label>
				</svelte:fragment>
			</TextField>
		</div>
	</div>

	<!-- Email -->
	<div class="col-12">
		<div class="form-floating">
			<TextField
				class="form-control rounded-3"
				name="email"
				inputAbove={true}
				placeholder="john.doe@example.com"
				validationFeedback={true}
				bind:reactiveFormField={registerForm.email}
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
				bind:reactiveFormField={registerForm.password}
				{disabled}>
				<svelte:fragment slot="label">
					<label for="password">Password</label>
				</svelte:fragment>
			</PasswordField>
		</div>
	</div>

	<!-- Confirm Password -->
	<div class="col-12">
		<div class="form-floating">
			<PasswordField
				class="form-control rounded-3"
				name="confirmPassword"
				inputAbove={true}
				placeholder="*********"
				validationFeedback={true}
				bind:reactiveFormField={registerForm.confirmPassword}
				{disabled}>
				<svelte:fragment slot="label">
					<label for="confirmPassword">Confirm Password</label>
				</svelte:fragment>
			</PasswordField>
		</div>
	</div>

	<!-- Agreement term -->
	<div class="col-12">
		<div class="form-check">
			<input
				class="form-check-input"
				name="terms"
				type="checkbox"
				bind:this={registerForm.agree.instance}
				bind:checked={registerForm.agree.value}
				id="acceptTerms" />
			<label class="form-check-label" for="acceptTerms">
				I agree and accept the <a href={'javascript:;'} class="text-decoration-none">
					terms and conditions
				</a>
			</label>
			<!-- TODO:  -->
			{#if !registerForm.agree.valid}
				<input
					type="hidden"
					class:is-invalid={registerForm.agree.touched && !registerForm.agree.valid} />
				<div class="invalid-feedback">You must agree before submitting.</div>
			{/if}
		</div>
	</div>

	<div class="col-12">
		<button
			class="btn btn-info w-100 py-2 mt-2 mb-3 rounded-4"
			type="submit"
			disabled={!$registerForm.valid || disabled}>
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
				class="text-decoration-none">
				Log in
			</a>
		</p>
	</div>
</form>
