<script>
	import { logout } from '$lib/core/auth/service';
	import { readMoreString } from '$lib/service';
	import { onMount } from 'svelte';

	/**
	 * @type {string}
	 */
	export let displayName;
    onMount(async () => {
        (await import("bootstrap")).Dropdown;
    })
</script>

<div class="dropdown">
	<button class="nav-link text-info text-center" data-bs-toggle="dropdown">
		<i class="fa-regular fa-circle-user fs-4"></i>
	</button>
	<ul class="dropdown-menu dropdown-menu-end position-absolute">
		<li>
			<h2 class="dropdown-header" title={displayName}>
				{readMoreString(displayName, 25)}
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
			<a class="dropdown-item" href={'#'} on:click={logout}>
				<i class="fa-solid fa-right-from-bracket"></i> Logout
			</a>
		</li>
	</ul>
</div>
