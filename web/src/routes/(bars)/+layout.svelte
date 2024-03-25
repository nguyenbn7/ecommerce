<script>
	import Footer from '$lib/component/footer.svelte';
	import Navbar from '$lib/component/navbar.svelte';
</script>

<div class="fixed-top">
	<Navbar />
</div>
<slot />
<Footer />
