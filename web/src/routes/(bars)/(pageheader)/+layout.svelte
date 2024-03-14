<script>
	import { page } from '$app/stores';
	import Breadcrumb from '$lib/shared/breadcrumbs/components/breadcrumb.svelte';
	import { buildAliasPaths, mapper } from '$lib/shared/breadcrumbs/service';
	import { APP_NAME } from '$lib/shared/constant';

	$: breadcrumbs = buildAliasPaths($page, $mapper);
</script>

<svelte:head>
	<title>{APP_NAME} | {breadcrumbs[breadcrumbs.length - 1].alias}</title>
</svelte:head>

<div class="container-fluid bg-secondary pb-2 pb-5 page-title">
	<div
		class="d-flex flex-column align-items-center justify-content-center"
		style="min-height: 300px"
	>
		<h2 class="fw-semibold text-uppercase mb-3">
			{breadcrumbs[breadcrumbs.length - 1].alias}
		</h2>
		<div class="d-inline-flex">
			<Breadcrumb class="ps-1 pb-3" {breadcrumbs} style="--bs-breadcrumb-divider: '>';" />
		</div>
	</div>
</div>

<slot />

<style lang="scss">
	.page-title {
		padding-top: 5.3rem !important;
	}
</style>
