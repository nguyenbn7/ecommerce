<script>
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import { ToastrService, ValidationFeedback } from '$lib/components';
	import { AccountService } from '$lib/module/account';
	import { APP_NAME } from '$lib/module/shared';
	import { RegisterForm } from '$lib/page/register';

	let registerForm = new RegisterForm();
	let isSubmitted = false;

	async function onSubmitForm() {
		try {
			isSubmitted = true;
			const userInfo = await AccountService.registerAsUser({
				email: registerForm.email.value,
				password: registerForm.password.value,
				firstName: registerForm.firstName.value,
				lastName: registerForm.lastName.value,
				confirmPassword: registerForm.confirmPassword.value
			});

			if (userInfo) {
				ToastrService.notifySuccess(`Welcome ${userInfo.displayName}`);

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
	<title>{APP_NAME} - Sign Up</title>
</svelte:head>

<div class="pt-4 pb-2">
	<h5 class="card-title text-center pb-0 fs-4">Create an Account</h5>
	<p class="text-center small">Enter your personal details to create account</p>
</div>

<form class="row g-3 px-1" on:submit={onSubmitForm}>
	<div class="col-12">
		<div class="form-floating">
			<input
				type="text"
				id="firstName"
				class="form-control rounded-3"
				placeholder="John"
				on:focusout={(e) => (registerForm.firstName.onFocusOut = e)}
				on:input={(e) => (registerForm.firstName.onInput = e)}
				class:is-invalid={registerForm.firstName.touched && !registerForm.firstName.valid}
				class:is-valid={registerForm.firstName.touched && registerForm.firstName.valid}
				disabled={isSubmitted}
			/>
			<label for="firstName">First Name</label>
			<ValidationFeedback bind:field={registerForm.firstName} />
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating">
			<input
				type="text"
				id="lastName"
				class="form-control rounded-3"
				placeholder="Doe"
				on:focusout={(e) => (registerForm.lastName.onFocusOut = e)}
				on:input={(e) => (registerForm.lastName.onInput = e)}
				class:is-invalid={registerForm.lastName.touched && !registerForm.lastName.valid}
				class:is-valid={registerForm.lastName.touched && registerForm.lastName.valid}
				disabled={isSubmitted}
			/>
			<label for="lastName">Last Name</label>
			<ValidationFeedback bind:field={registerForm.lastName} />
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating">
			<input
				type="text"
				id="email"
				class="form-control rounded-3"
				placeholder="name@example.com"
				on:focusout={(e) => (registerForm.email.onFocusOut = e)}
				on:input={(e) => (registerForm.email.onInput = e)}
				class:is-invalid={registerForm.email.touched && !registerForm.email.valid}
				class:is-valid={registerForm.email.touched && registerForm.email.valid}
				disabled={isSubmitted}
			/>
			<label for="email">Email</label>
			<ValidationFeedback bind:field={registerForm.email} />
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating">
			<input
				type="password"
				id="password"
				class="form-control rounded-3"
				placeholder="*********"
				on:focusout={(e) => (registerForm.password.onFocusOut = e)}
				on:input={(e) => (registerForm.password.onInput = e)}
				class:is-invalid={registerForm.password.touched && !registerForm.password.valid}
				class:is-valid={registerForm.password.touched && registerForm.password.valid}
				disabled={isSubmitted}
			/>
			<label for="password">Password</label>
			<ValidationFeedback bind:field={registerForm.password} />
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating mt-2 mb-3">
			<input
				type="password"
				id="confirmPassword"
				class="form-control rounded-3"
				placeholder="*********"
				on:focusout={(e) => (registerForm.confirmPassword.onFocusOut = e)}
				on:input={(e) => (registerForm.confirmPassword.onInput = e)}
				class:is-invalid={registerForm.confirmPassword.touched &&
					!registerForm.confirmPassword.valid}
				class:is-valid={registerForm.confirmPassword.touched && registerForm.confirmPassword.valid}
				disabled={isSubmitted}
			/>
			<label for="confirmPassword">Confirm Password</label>
			<ValidationFeedback bind:field={registerForm.confirmPassword} />
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
			disabled={!registerForm.valid || isSubmitted}
		>
			Create Account
			<!-- {#if isSubmitted}
				<ButtonSpinner />
			{/if} -->
		</button>
	</div>
	<div class="col-12">
		<p class="small mb-3 text-center">
			Already have an account? <a href="/login" class="text-decoration-none">Log in</a>
		</p>
	</div>
</form>
