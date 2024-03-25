<script>
	import { page } from '$app/stores';
	import Footer from '$lib/component/footer.svelte';
	import Navbar from '$lib/component/navbar.svelte';
</script>

<div class="fixed-top">
	<Navbar />
</div>
<div id="page-header" class="container-fluid pb-2 pb-5 page-title">
	<div
		class="d-flex flex-column align-items-center justify-content-center"
		style="min-height: 300px"
	>
		<h2 class="fw-semibold text-uppercase mb-3 text-white">error page</h2>
	</div>
</div>

<main class="flex-grow-1 mb-4 mt-5">
	<div class="container px-5 text-center py-5">
		<div class="my-5 py-5">
			<i class="fa-solid fa-triangle-exclamation text-warning display-1"></i>
			<h1 class="display-1 text-secondary fw-bold">{$page.status}</h1>
			<h1 class="mb-4 fw-semibold">{$page.error?.message}</h1>
			<p class="mb-4 h5">
				We're sorry, the page you have looked for does not exist in our website! Maybe go to our
				home page or try to use a search?
			</p>
			<a class="btn btn-outline-info rounded-pill py-3 px-5 me-3" href="/">Go Back To Home</a>
		</div>
	</div>
</main>
<Footer />

<style lang="scss">
	.page-title {
		padding-top: 5.3rem !important;
	}

	#page-header {
		background-image: linear-gradient(rgba(0, 0, 0, 0.35), rgba(0, 0, 0, 0.35)),
			url('/page-header.webp');
		background-repeat: no-repeat;
		background-attachment: fixed;
		background-position: center;
	}
</style>
