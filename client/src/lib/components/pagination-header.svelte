<script>
	/**
	 * @type {number | undefined}
	 */
	export let pageNumber;
	/**
	 * @type {number | undefined}
	 */
	export let pageSize;
	/**
	 * @type {number | undefined}
	 */
	export let totalItems;
</script>

{#if totalItems !== undefined && pageNumber !== undefined && pageSize !== undefined}
	<header>
		{#if totalItems > 0}
			<span>
				Showing <strong>
					{(pageNumber - 1) * pageSize + 1} -
					{pageNumber * pageSize > totalItems ? totalItems : pageNumber * pageSize}
				</strong>
				of
				<strong>{totalItems}</strong> results
			</span>
		{:else}
			<span>There are <strong>0</strong> results for this filter</span>
		{/if}
	</header>
{/if}
