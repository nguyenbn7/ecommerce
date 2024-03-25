<script>
	import favicon from '$lib/assets/images/favicon.ico';
	import 'bootswatch/dist/flatly/bootstrap.min.css';
	import '@fortawesome/fontawesome-free/css/all.min.css';
	import Toastr from '$lib/component/toastr.svelte';
	import BackToTopButton from '$lib/component/back-to-top-button.svelte';
</script>

<svelte:head>
	<link rel="shortcut icon" href={favicon} type="image/x-icon" />
</svelte:head>

<Toastr class="p-4 mt-5 me-5" />
<BackToTopButton />

<slot />
