<script>
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import { ToastrService, ValidationFeedback } from '$lib/components';
	import { AccountService } from '$lib/module/account';
	import { APP_NAME } from '$lib/module/shared';
	import { LoginForm } from '$lib/page/login';

	let loginForm = new LoginForm();
	let isSubmitted = false;

	async function onSubmit() {
		try {
			isSubmitted = true;

			const userInfo = await AccountService.loginAsUser({
				email: loginForm.email.value,
				password: loginForm.password.value
			});

			if (userInfo) {
				ToastrService.notifySuccess(`Welcome back ${userInfo.displayName}`);

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
	<title>{APP_NAME} - Sign In</title>
</svelte:head>

<div class="pt-4 pb-2 mb-4">
	<h5 class="card-title text-center pb-0 fs-4">Login to Your Account</h5>
</div>

<form class="row g-3 px-1 needs-validation" on:submit={onSubmit}>
	<div class="col-12">
		<div class="form-floating">
			<input
				type="text"
				id="email"
				class="form-control rounded-3"
				placeholder="name@example.com"
				on:focusout={(e) => (loginForm.email.onFocusOut = e)}
				on:input={(e) => (loginForm.email.onInput = e)}
				class:is-invalid={loginForm.email.touched && !loginForm.email.valid}
				class:is-valid={loginForm.email.touched && loginForm.email.valid}
				disabled={isSubmitted}
			/>
			<label for="email">Email</label>
			<ValidationFeedback bind:field={loginForm.email} />
		</div>
	</div>

	<div class="col-12">
		<div class="form-floating">
			<input
				type="password"
				id="password"
				class="form-control rounded-3"
				placeholder="*********"
				on:focusout={(e) => (loginForm.password.onFocusOut = e)}
				on:input={(e) => (loginForm.password.onInput = e)}
				class:is-invalid={loginForm.password.touched && !loginForm.password.valid}
				class:is-valid={loginForm.password.touched && loginForm.password.valid}
				disabled={isSubmitted}
			/>
			<label for="password">Password</label>
			<ValidationFeedback bind:field={loginForm.password} />
		</div>
	</div>

	<!-- <div class="col-12">
		<div class="form-check">
			<input
				class="form-check-input"
				type="checkbox"
				name="remember"
				value="true"
				id="rememberMe"
			/>
			<label class="form-check-label" for="rememberMe">Remember me</label>
		</div>
	</div> -->
	<div class="col-12">
		<button
			class="btn btn-info w-100 py-2 mt-2 mb-3 rounded-4"
			type="submit"
			disabled={!loginForm.valid || isSubmitted}
		>
			Login
			<!-- {#if isSubmitted}
				<ButtonSpinner />
			{/if} -->
		</button>
	</div>
	<div class="col-12 mb-3">
		<p class="small mb-3 text-center">
			Don't have account? <a href="/register" class="text-decoration-none">Create an account</a>
		</p>
		<p class="small mb-0 text-center">
			Sign in as <a href="/login/demo" class="text-decoration-none">Demo Users</a>
		</p>
	</div>
</form>
