<script>
	import { APP_NAME } from '$lib/constant';
	import FeatureSection from '$lib/home/feature-section.svelte';
	import HeroSection from '$lib/home/hero-section.svelte';
</script>

<svelte:head>
	<title>{APP_NAME} | Home Page</title>
</svelte:head>

<HeroSection />
<FeatureSection />
