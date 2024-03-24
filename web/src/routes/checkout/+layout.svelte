<script>
	import { APP_NAME } from '$lib/shared/constant';
	import logo from '$lib/assets/images/logo.png';
</script>

<svelte:head>
	<title>{APP_NAME} | Checkout</title>
</svelte:head>

<div class="container" style="margin-bottom: 7%;">
	<main>
		<div class="py-5 text-center">
			<a href="/">
				<img class="d-block mx-auto mb-4" src={logo} alt="" width="250" height="100" />
			</a>
			<h2>Checkout</h2>
		</div>

		<slot />
	</main>
</div>
