<script>
	import 'animate.css/animate.min.css';
	import { onMount } from 'svelte';

	/**
	 * @type {HTMLElement}
	 */
	let heroCarousel;
	let active = 0;

	const heroSectionId = 'heroSection';

	const heroImages = ['hero1.jpg', 'hero2.jpg', 'hero3.jpg'];

	const captions = [
		{ subtitle: 'Mega Sale Going On!', heading: 'Get December Discount' },
		{ subtitle: 'Top quality board', heading: '10% off your first order' },
		{ subtitle: 'Unique style', heading: 'Fashionable and Reasonable Price' }
	];

	onMount(async () => {
		new (await import('bootstrap/js/dist/carousel')).default(heroCarousel, {
			ride: 'carousel',
			pause: false
		});

		heroCarousel.addEventListener('slid.bs.carousel', (event) => {
			// @ts-ignore
			active = event.to;
		});
	});
</script>

<section class="carousel slide" id={heroSectionId} bind:this={heroCarousel}>
	<div class="carousel-indicators">
		{#each heroImages as _, idx}
			<button
				type="button"
				data-bs-target="#{heroSectionId}"
				data-bs-slide-to={idx}
				class:active={idx === 0}
				aria-current="true"
				aria-label="Slide {idx + 1}"
			></button>
		{/each}
	</div>
	<div class="carousel-inner">
		{#each heroImages as image, idx}
			<div class="carousel-item" class:active={idx === 0}>
				<img
					src="/images/home/hero/{image}"
					class="m-0 min-vw-100 min-vh-100 hero-img"
					alt="slide"
				/>
				<div
					class="carousel-caption h-100 d-flex justify-content-center align-items-center hero-text"
				>
					<div>
						<p
							class="subtitle"
							class:animate__animated={idx === active}
							class:animate__fadeInUp={idx === active}
							class:animated={idx === active}
						>
							{captions[idx].subtitle}
						</p>
						<h1
							class:animate__animated={idx === active}
							class:animate__fadeInUp={idx === active}
							class:animated={idx === active}
							class:animation-duration-300ms={idx === active}
						>
							{captions[idx].heading}
						</h1>
						<div
							class="hero-btns"
							class:animate__animated={idx === active}
							class:animate__fadeInUp={idx === active}
							class:animated={idx === active}
							class:animation-duration-500ms={idx === active}
						>
							<a href="/shop" class="btn btn-warning boxed-btn">Visit Shop</a>
							<a href="/contact" class="btn btn-outline-warning bordered-btn">Contact Us</a>
						</div>
					</div>
				</div>
			</div>
		{/each}
	</div>
	<button
		class="carousel-control-prev"
		type="button"
		data-bs-target="#{heroSectionId}"
		data-bs-slide="prev"
	>
		<span class="carousel-control-prev-icon" aria-hidden="true"></span>
		<span class="visually-hidden">Previous</span>
	</button>
	<button
		class="carousel-control-next"
		type="button"
		data-bs-target="#{heroSectionId}"
		data-bs-slide="next"
	>
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

	a.boxed-btn {
		font-family: 'Poppins', sans-serif;
		display: inline-block;
		padding: 10px 20px;
		text-decoration: none;
	}

	a.bordered-btn {
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
		animation-delay: 0.3s !important;
	}

	.animated {
		-webkit-animation-duration: 1s !important;
		animation-duration: 1s !important;
	}
</style>
