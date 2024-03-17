<script>
	import logo from '$lib/assets/images/logo.png';
	import { page } from '$app/stores';
	import { userInfo } from '$lib/auth/service';
	import { onMount } from 'svelte';
	import { basket } from '$lib/baskets/service';
	import { readMoreString } from '$lib/shared/helper';

	/**
	 * @type {number}
	 */
	let y;

	const paths = [
		{ link: '/shop', name: 'Shop' },
		{ link: '/about', name: 'About' },
		{ link: '/blog', name: 'Blog' },
		{ link: '/contact', name: 'Contact' }
	];

	/**
	 * @param {BasketItem[]} items
	 */
	function getCount(items) {
		return items.reduce((total, item) => total + item.quantity, 0);
	}

	onMount(async () => {
		(await import('bootstrap/js/dist/dropdown')).default;
	});
</script>

<svelte:window bind:scrollY={y} />

<nav class="navbar navbar-expand-md py-3" class:bg-primary={y >= 56}>
	<div class="container">
		<div class="col-3">
			<a class="navbar-brand" href="/">
				<img src={logo} alt="logo" style="max-height: 2.5em;" class="logo" />
			</a>
		</div>
		<button
			class="navbar-toggler"
			type="button"
			data-bs-toggle="collapse"
			data-bs-target="#linkNavbar"
			aria-controls="linkNavbar"
			aria-expanded="false"
			aria-label="Toggle navigation"
		>
			<span class="navbar-toggler-icon"></span>
		</button>
		<div class="collapse navbar-collapse" id="linkNavbar">
			<ul class="navbar-nav mx-md-auto mb-2 mb-lg-0">
				{#each paths as path}
					<li>
						<a
							href={path.link}
							class="nav-link text-white"
							class:active-link={$page.url.pathname === path.link ||
								$page.url.pathname === path.link + '/'}
						>
							{path.name}
						</a>
					</li>
				{/each}
			</ul>
			<ul class="navbar-nav ms-md-auto mb-2 mb-lg-0">
				<a href={'#'} class="nav-link text-white me-3">
					<i class="fa-solid fa-magnifying-glass"></i>
				</a>
				<a class="nav-link text-white me-3" href="/favorites" title="Wish list">
					<i class="fa-solid fa-heart"></i>
				</a>
				<a
					class="nav-link text-white me-3"
					class:text-secondary={!$basket || !$basket.items.length}
					class:text-info={$basket && $basket.items.length}
					href="/basket"
					title="Basket"
				>
					<i class="fa-solid fa-basket-shopping"></i>
					{#if $basket && $basket.items.length}
						<span class="position-absolute translate-middle bg-danger badge rounded-pill">
							{getCount($basket.items)}
						</span>
					{/if}
				</a>
				{#if !$userInfo}
					<a href="/account/login" class="btn btn-outline-success text-white px-3 me-3"> Login </a>
				{:else}
					<a
						class="nav-btn text-info me-3 d-flex align-items-center"
						data-bs-toggle="dropdown"
						aria-expanded="false"
						href={'#'}
						title="Profile"
					>
						<i class="fa-regular fa-circle-user fs-4"></i>
					</a>
					<ul class="dropdown-menu dropdown-menu-end me-5">
						<li>
							<h2 class="dropdown-header" title={$userInfo.displayName}>
								{readMoreString($userInfo.displayName, 25)}
							</h2>
						</li>
						<li>
							<a class="dropdown-item" href={'#'}>
								<i class="bi bi-card-checklist"></i> View Order
							</a>
						</li>
						<li>
							<a class="dropdown-item" href={'#'}>
								<i class="bi bi-person-fill-gear"></i> View Profile
							</a>
						</li>
						<li><hr class="dropdown-divider" /></li>
						<!-- <li>
							<a class="dropdown-item" href={'#'} on:click={AccountService.logout}>
								<i class="fa-solid fa-right-from-bracket"></i> Logout
							</a>
						</li> -->
					</ul>
				{/if}
			</ul>
		</div>
	</div>
</nav>

<style lang="scss">
	.navbar {
		transition: all 0.5s;
	}
	.active-link {
		color: var(--bs-warning) !important;
		pointer-events: none;
		cursor: default;
	}

	.logo {
		cursor: pointer;
	}
</style>
