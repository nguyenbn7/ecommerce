<script>
	import 'bootswatch/dist/flatly/bootstrap.min.css';
	import '@fortawesome/fontawesome-free/css/all.min.css';
	import Toastr from '$lib/component/toastr.svelte';
	import BackToTopButton from '$lib/component/back-to-top-button.svelte';
	import favicon from '$lib/assets/images/favicon.ico';
</script>

<svelte:head>
	<link rel="shortcut icon" href={favicon} type="image/x-icon" />
</svelte:head>

<Toastr class="pt-4 pe-4 mt-5 me-5" />
<BackToTopButton />

<slot />
