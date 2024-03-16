<script>
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import { RegisterForm } from '$lib/auth/form';
	import { registerAsUser } from '$lib/auth/service';
	import { APP_NAME } from '$lib/shared/constant';
	import ValidationFeedback from '$lib/shared/form/validation-feedback.svelte';
	import ButtonLoader from '$lib/shared/spinner/button-loader.svelte';
	import { notifySuccess } from '$lib/shared/toastr/service';

	let registerForm = new RegisterForm();
	let isSubmitted = false;

	async function onSubmitForm() {
		try {
			isSubmitted = true;
			const userInfo = await registerAsUser({
				email: registerForm.emailField.value,
				password: registerForm.passwordField.value,
				firstName: registerForm.firstNameField.value,
				lastName: registerForm.lastNameField.value,
				confirmPassword: registerForm.confirmPasswordField.value
			});

			if (userInfo) {
				notifySuccess(`Welcome ${userInfo.displayName}`);

				const returnUrl = $page.url.searchParams.get('redirect');
				if (returnUrl) return goto(returnUrl);

				return goto('/');
			}
		} finally {
			isSubmitted = false;
		}
	}
</script>

<svelte:head>
	<title>{APP_NAME} | Sign Up</title>
</svelte:head>

<div class="pt-4 pb-2">
	<h5 class="card-title text-center pb-0 fs-4">Create an Account</h5>
	<p class="text-center small">Enter your personal details to create account</p>
</div>

<form class="row g-3 px-1" on:submit={onSubmitForm} use:registerForm.bind>
	<div class="col-12">
		<div class="form-floating">
			<input
				type="text"
				id="firstName"
				class="form-control rounded-3"
				placeholder="John"
				use:registerForm.firstNameField.bind
				class:is-invalid={$registerForm.firstNameField.isTouched &&
					!$registerForm.firstNameField.isValid}
				class:is-valid={$registerForm.firstNameField.isTouched &&
					$registerForm.firstNameField.isValid}
				disabled={isSubmitted}
			/>
			<label for="firstName">First Name</label>
			<ValidationFeedback field={$registerForm.firstNameField} />
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating">
			<input
				type="text"
				id="lastName"
				class="form-control rounded-3"
				placeholder="Doe"
				use:registerForm.lastNameField.bind
				class:is-invalid={$registerForm.lastNameField.isTouched &&
					!$registerForm.lastNameField.isValid}
				class:is-valid={$registerForm.lastNameField.isTouched &&
					$registerForm.lastNameField.isValid}
				disabled={isSubmitted}
			/>
			<label for="lastName">Last Name</label>
			<ValidationFeedback field={$registerForm.lastNameField} />
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating">
			<input
				type="text"
				id="email"
				class="form-control rounded-3"
				placeholder="name@example.com"
				use:registerForm.emailField.bind
				class:is-invalid={$registerForm.emailField.isTouched && !$registerForm.emailField.isValid}
				class:is-valid={$registerForm.emailField.isTouched && $registerForm.emailField.isValid}
				disabled={isSubmitted}
			/>
			<label for="email">Email</label>
			<ValidationFeedback field={$registerForm.emailField} />
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating">
			<input
				type="password"
				id="password"
				class="form-control rounded-3"
				placeholder="*********"
				use:registerForm.passwordField.bind
				class:is-invalid={$registerForm.passwordField.isTouched &&
					!$registerForm.passwordField.isValid}
				class:is-valid={$registerForm.passwordField.isTouched &&
					$registerForm.passwordField.isValid}
				disabled={isSubmitted}
			/>
			<label for="password">Password</label>
			<ValidationFeedback field={$registerForm.passwordField} />
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating mt-2 mb-3">
			<input
				type="password"
				id="confirmPassword"
				class="form-control rounded-3"
				placeholder="*********"
				use:registerForm.confirmPasswordField.bind
				class:is-invalid={$registerForm.confirmPasswordField.isTouched &&
					!$registerForm.confirmPasswordField.isValid}
				class:is-valid={$registerForm.confirmPasswordField.isTouched &&
					$registerForm.confirmPasswordField.isValid}
				disabled={isSubmitted}
			/>
			<label for="confirmPassword">Confirm Password</label>
			<ValidationFeedback field={$registerForm.confirmPasswordField} />
		</div>
	</div>

	<div class="col-12">
		<div class="form-check">
			<input class="form-check-input" name="terms" type="checkbox" value="" id="acceptTerms" />
			<label class="form-check-label" for="acceptTerms">
				I agree and accept the <a href={'#'} class="text-decoration-none">terms and conditions</a>
			</label>
			<!-- TODO:  -->
			<div class="invalid-feedback">You must agree before submitting.</div>
		</div>
	</div>
	<div class="col-12">
		<button
			class="btn btn-info w-100 py-2 mt-2 mb-3 rounded-4"
			type="submit"
			disabled={!$registerForm.isValid || isSubmitted}
		>
			Create Account
			{#if isSubmitted}
				<ButtonLoader />
			{/if}
		</button>
	</div>
	<div class="col-12">
		<p class="small mb-3 text-center">
			Already have an account? <a href="/account/login" class="text-decoration-none">Log in</a>
		</p>
	</div>
</form>
