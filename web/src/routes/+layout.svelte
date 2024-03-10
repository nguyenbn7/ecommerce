<script>
	import 'bootstrap/dist/css/bootstrap.min.css';
	import '@fortawesome/fontawesome-free/css/all.min.css';
	import Toastr from '$lib/components/toastr.svelte';
	import BackToTopButton from '$lib/components/back-to-top-button.svelte';
</script>

<Toastr singleToast={true} />
<BackToTopButton />
<slot />
