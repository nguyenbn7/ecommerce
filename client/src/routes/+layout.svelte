<script>
	import { Toastr } from '$lib';
	import 'bootstrap/dist/css/bootstrap.min.css';
</script>

<Toastr class="toast-container position-fixed top-0 end-0 p-4 mt-5 me-5" singleToast={true} />

<slot />
