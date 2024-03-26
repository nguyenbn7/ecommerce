<script>
	import { onMount } from 'svelte';
	import { page } from '$app/stores';
	import logo from '$lib/assets/images/logo.png';
	import { basket } from '$lib/core/basket/service';
	import { logout, userInfo } from '$lib/core/auth/service';
	import { readMoreString } from '$lib/service';

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
		(await import('bootstrap/js/dist/collapse')).default;
	});
</script>

<svelte:window bind:scrollY={y} />

<nav class="navbar navbar-expand-md py-3" class:bg-primary={y >= 56} class:navbar-shadow={y >= 56}>
	<div class="container">
		<div class="col-3">
			<a class="navbar-brand" href="/">
				<img src={logo} alt="logo" style="max-height: 2.5em;" class="logo" />
			</a>
		</div>
		<button
			class="navbar-toggler border-white"
			type="button"
			data-bs-toggle="collapse"
			data-bs-target="#appNavbar"
			aria-controls="appNavbar"
			aria-expanded="false"
			aria-label="Toggle navigation">
			<span class="navbar-toggler-icon">
				<i class="fa fa-bars" aria-hidden="true" style="color:#e6e6ff; font-size: 1.5em;"></i>
			</span>
		</button>
		<div class="collapse navbar-collapse" id="appNavbar">
			<ul
				class="navbar-nav mx-md-auto mb-2 mb-lg-0 navbar-nav-scroll"
				style="--bs-scroll-height: 100px;">
				{#each paths as path}
					<li>
						<a
							href={path.link}
							class="nav-link text-white fw-medium"
							class:active-link={$page.url.pathname === path.link ||
								$page.url.pathname === path.link + '/'}>
							{path.name}
						</a>
					</li>
				{/each}
			</ul>
			<ul class="navbar-nav ms-md-auto mb-2 mb-lg-0">
				<li class="nav-item me-3">
					<a href={'javascript:;'} class="nav-link text-white">
						<i class="fa-solid fa-magnifying-glass"></i>
					</a>
				</li>
				<li class="nav-item me-3">
					<a class="nav-link text-white" href="/favorites" title="Wish list">
						<i class="fa-solid fa-heart"></i>
					</a>
				</li>
				<li class="nav-item me-3">
					<a
						class="nav-link text-white"
						class:text-secondary={!$basket || !$basket.items.length}
						class:text-info={$basket && $basket.items.length}
						href="/basket"
						title="Basket">
						<i class="fa-solid fa-basket-shopping"></i>
						{#if $basket && $basket.items.length}
							<span class="position-absolute translate-middle bg-danger badge rounded-pill">
								{getCount($basket.items)}
							</span>
						{/if}
					</a>
				</li>
				{#if !$userInfo}
					<li class="nav-item me-3">
						<a href="/account/login" class="btn btn-outline-success text-white px-3"> Login </a>
					</li>
				{:else}
					<li class="nav-item dropdown">
						<a
							class="btn btn-link nav-link text-info dropdown-toggle d-flex align-items-center"
							data-bs-toggle="dropdown"
							aria-expanded="false"
							href={'javascript:;'}
							title={$userInfo.displayName}>
							<i class="fa-regular fa-circle-user fs-4"></i>
						</a>
						<ul class="dropdown-menu dropdown-menu-end position-absolute">
							<li>
								<h2 class="dropdown-header" title={$userInfo.displayName}>
									{readMoreString($userInfo.displayName, 25)}
								</h2>
							</li>
							<li>
								<a class="dropdown-item" href="/order">
									<i class="bi bi-card-checklist"></i> View Order
								</a>
							</li>
							<li>
								<a class="dropdown-item" href={'#'}>
									<i class="bi bi-person-fill-gear"></i> View Profile
								</a>
							</li>
							<li><hr class="dropdown-divider" /></li>
							<li>
								<a class="dropdown-item" href={'javascript:;'} on:click={logout}>
									<i class="fa-solid fa-right-from-bracket"></i> Logout
								</a>
							</li>
						</ul>
					</li>
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
		color: var(--bs-orange) !important;
		font-weight: bolder !important;
		pointer-events: none;
		cursor: default;
	}

	.navbar-shadow {
		box-shadow:
			rgba(0, 0, 0, 0.25) 0px 14px 28px,
			rgba(0, 0, 0, 0.22) 0px 10px 10px;
	}

	.logo {
		cursor: pointer;
	}
</style>
