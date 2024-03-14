<script>
	import 'bootswatch/dist/flatly/bootstrap.min.css';
	import '@fortawesome/fontawesome-free/css/all.min.css';
	import BackToTopButton from '$lib/shared/components/back-to-top-button.svelte';
	import Toastr from '$lib/shared/toasts/toastr.svelte';
</script>

<Toastr singleToast={true} />
<BackToTopButton />
<slot />
