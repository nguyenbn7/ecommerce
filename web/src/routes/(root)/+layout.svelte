<script>
	import Footer from '$lib/components/footer.svelte';
	import Navbar from '$lib/components/navbar.svelte';
</script>

<div class="fixed-top">
	<Navbar />
</div>
<slot />
<Footer />
