<script>
	import { page } from '$app/stores';
	import { APP_NAME } from '$lib/constant';
	import Breadcrumb, {
		aliases,
		createBreadcrumbs,
		getLastBreadcrumbItem
	} from '$lib/component/breadcrumb.svelte';

	import pageHeaderImgUrl from '$lib/assets/images/page-header.webp?raw';
	// console.log(PageHeader);

	$: breadcrumbs = createBreadcrumbs($page, $aliases);
	$: pageName = getLastBreadcrumbItem(breadcrumbs)?.name;
</script>

<svelte:head>
	<title>{APP_NAME} | {pageName}</title>
</svelte:head>

<div class="container-fluid pb-2 pb-5 page-title">
	<div
		class="d-flex flex-column align-items-center justify-content-center"
		style="min-height: 300px">
		<h2 class="fw-semibold text-uppercase mb-3 text-white">
			{pageName}
		</h2>
		<div class="d-inline-flex">
			<Breadcrumb
				class="breadcrumb ps-1 pb-3 text-white"
				{breadcrumbs}
				--bs-breadcrumb-divider="'>'" />
		</div>
	</div>
</div>

<main class="flex-grow-1 mb-4 mt-5">
	<div class="container-fluid px-5">
		<slot />
	</div>
</main>

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
