<script>
	import 'animate.css/animate.min.css';
	import { onMount } from 'svelte';

	const heroes = [
		{ subtitle: 'Mega Sale Going On!', heading: 'Get December Discount', image: 'hero1.jpg' },
		{ subtitle: 'Top quality board', heading: '10% off your first order', image: 'hero2.jpg' },
		{ subtitle: 'Unique style', heading: 'Fashionable and Reasonable Price', image: 'hero3.jpg' }
	];

	const heroImagesModules = import.meta.glob('$lib/assets/home/hero/*.jpg', {
		eager: true,
		import: 'default'
	});

	const heroImagesPath = Object.keys(heroImagesModules)[0].split('/').slice(0, -1).join('/');

	const id = 'hero-section';

	onMount(async () => {
		const heroCarousel = document.getElementById(id);

		if (heroCarousel)
			new (await import('bootstrap/js/dist/carousel')).default(heroCarousel, {
				ride: 'carousel',
				pause: false
			});
	});
</script>

<section class="carousel slide" {id}>
	<div class="carousel-indicators">
		{#each heroes as hero, idx}
			<button
				type="button"
				data-bs-target="#{id}"
				data-bs-slide-to={idx}
				class:active={idx === 0}
				aria-current="true"
				aria-label={hero.image}></button>
		{/each}
	</div>
	<div class="carousel-inner">
		{#each heroes as hero, idx}
			<div class="carousel-item" class:active={idx === 0}>
				<img
					src={heroImagesModules[`${heroImagesPath}/${hero.image}`] + ''}
					class="m-0 min-vw-100 min-vh-100 hero-img"
					alt="slide" />
				<div
					class="carousel-caption h-100 d-flex justify-content-center align-items-center hero-text">
					<div>
						<p class="subtitle animated animate__animated animate__fadeInUp">
							{hero.subtitle}
						</p>
						<h1 class="animated animate__animated animate__fadeInUp animation-duration-300ms">
							{hero.heading}
						</h1>
						<div
							class="hero-btns animated animate__animated animate__fadeInUp animation-duration-500ms">
							<a href="/shop" class="btn btn-warning boxed-btn">Visit Shop</a>
							<a href="/contact" class="btn btn-outline-warning bordered-btn">Contact Us</a>
						</div>
					</div>
				</div>
			</div>
		{/each}
	</div>
	<button class="carousel-control-prev" type="button" data-bs-target="#{id}" data-bs-slide="prev">
		<span class="carousel-control-prev-icon" aria-hidden="true"></span>
		<span class="visually-hidden">Previous</span>
	</button>
	<button class="carousel-control-next" type="button" data-bs-target="#{id}" data-bs-slide="next">
		<span class="carousel-control-next-icon" aria-hidden="true"></span>
		<span class="visually-hidden">Next</span>
	</button>
</section>

<style lang="scss">
	.hero-img {
		object-fit: cover;
		height: 600px;
		width: 100%;
		filter: brightness(55%);
	}

	.hero-text p.subtitle {
		color: #f28123;
		font-weight: 700;
		text-transform: uppercase;
		letter-spacing: 0.5rem;
		font-size: 1.25rem;
	}

	.hero-text h1 {
		font-size: 64px;
		font-weight: 700;
		line-height: 1.3;
		color: #fff;
	}

	.hero-btns {
		margin-top: 35px;
	}

	.hero-btns a.bordered-btn {
		margin-left: 15px;
	}

	a.bordered-btn,
	a.boxed-btn {
		font-family: 'Poppins', sans-serif;
		display: inline-block;
		padding: 10px 20px;
		text-decoration: none;
	}

	a.boxed-btn,
	a.bordered-btn {
		border-radius: 50px;
	}

	.animation-duration-300ms {
		animation-delay: 0.3s !important;
	}

	.animation-duration-500ms {
		animation-delay: 0.5s !important;
	}

	.animated {
		-webkit-animation-duration: 1s !important;
		animation-duration: 1s !important;
	}
</style>
