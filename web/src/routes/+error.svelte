<script>
	import NotFoundImageUrl from '$lib/assets/images/404.jpg';
	import { page } from '$app/stores';
	import Footer from '$lib/component/footer.svelte';
	import Navbar from '$lib/component/navbar.svelte';
	import { APP_NAME } from '$lib/constant';
</script>

<svelte:head>
	<title>{APP_NAME} | {$page.status}</title>
</svelte:head>

<div class="fixed-top">
	<Navbar />
</div>
<div class="container-fluid pb-2 pb-5 page-title">
	<div
		class="d-flex flex-column align-items-center justify-content-center"
		style="min-height: 300px">
		<h2 class="fw-semibold text-uppercase mb-3 text-white">
			{$page.status} - {$page.error?.message}
		</h2>
	</div>
</div>

<main class="flex-grow-1 mb-4 mt-5">
	<div class="container px-5 text-center py-5">
		<div class="my-3 py-5">
			<img src={NotFoundImageUrl} alt="{$page.status} - {$page.error?.message}" />
		</div>
		<div class="mb-3">
			<a class="btn btn-outline-info rounded-pill py-3 px-5 me-3" href="/">Go Back To Home</a>
		</div>
	</div>
</main>
<Footer />

<style lang="scss">
	.page-title {
		padding-top: 5.3rem !important;
		background-image: linear-gradient(rgba(0, 0, 0, 0.45), rgba(255, 255, 255, 0.7)),
			url($lib/assets/images/page-header.webp);
		background-repeat: no-repeat;
		background-attachment: fixed;
		background-position: 20% 40%;
	}
</style>
