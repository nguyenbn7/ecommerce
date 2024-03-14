<script>
	import Footer from '$lib/shared/components/footer.svelte';
	import Navbar from '$lib/shared/components/navbar.svelte';
</script>

<div class="fixed-top">
	<Navbar />
</div>
<slot />
<Footer />
