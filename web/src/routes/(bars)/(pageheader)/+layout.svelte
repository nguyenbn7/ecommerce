<script>
	import { page } from '$app/stores';
	import { APP_NAME } from '$lib/shared/constant';
	import Breadcrumb, {
		aliases,
		createBreadcrumbs,
		getLastBreadcrumbItem
	} from '$lib/core/breadcrumb.svelte';

	$: breadcrumbs = createBreadcrumbs($page, $aliases);
	$: pageName = getLastBreadcrumbItem(breadcrumbs)?.name;
</script>

<svelte:head>
	<title>{APP_NAME} | {pageName}</title>
</svelte:head>

<div class="container-fluid bg-secondary pb-2 pb-5 page-title">
	<div
		class="d-flex flex-column align-items-center justify-content-center"
		style="min-height: 300px"
	>
		<h2 class="fw-semibold text-uppercase mb-3">
			{pageName}
		</h2>
		<div class="d-inline-flex">
			<Breadcrumb class="breadcrumb ps-1 pb-3" {breadcrumbs} --bs-breadcrumb-divider="'>'" />
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
	}
</style>
