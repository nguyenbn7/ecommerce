<script>
	import favicon from '$lib/assets/images/favicon.ico';
	import 'bootswatch/dist/flatly/bootstrap.min.css';
	import '@fortawesome/fontawesome-free/css/all.min.css';
	import BackToTopButton from '$lib/shared/components/back-to-top-button.svelte';
	import Toastr from '$lib/shared/toastr/toastr.svelte';
</script>

<svelte:head>
	<link rel="shortcut icon" href={favicon} type="image/x-icon" />
</svelte:head>

<Toastr showSingleToast={false} />
<BackToTopButton />

<slot />
