<script>
	export { styleClasses as class };
	let styleClasses = '';
	styleClasses = 'breadcrumb ' + styleClasses.trim();
	styleClasses.trim();
	/**
	 * @type {{alias: string, href: string | null}[]}
	 */
	export let breadcrumbs;
</script>

<ol class={styleClasses} aria-label="breadcrumb" {...$$restProps}>
	{#each breadcrumbs as path, idx}
		{#if !path.href && !idx}
			<li class="breadcrumb-item active" aria-current="page">
				{path.alias}
			</li>
		{:else if !path.href}
			<li class="breadcrumb-item active" aria-current="page">
				{path.alias}
			</li>
		{:else if !idx}
			<li class="breadcrumb-item">
				<a class="link-primary fw-semibold text-decoration-none" href={path.href}>
					{path.alias}
				</a>
			</li>
		{:else}
			<li class="breadcrumb-item">
				<a class="link-primary fw-semibold text-decoration-none" href={path.href}>
					{path.alias}
				</a>
			</li>
		{/if}
	{/each}
</ol>
