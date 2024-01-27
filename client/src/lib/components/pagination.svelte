<script>
	import { createEventDispatcher } from 'svelte';

	export let nextText = 'Next';
	export let previousText = 'Previous';
	export let lastText = '';
	export let firstText = '';

	/**
	 * @type {Number}
	 */
	export let totalItems;
	/**
	 * @type {Number}
	 */
	export let itemsPerPage;
	/**
	 * @type {Number}
	 */
	export let maxSize = 5;
	/**
	 * @type {Number}
	 */
	export let pageNumber;

	const dispatch = createEventDispatcher();
	const eventName = 'pageChanged';

	/**
	 * @param {number} page
	 */
	function onClickPage(page) {
		if (page < 1 || page > lastPage) return;
		pageNumber = page;
		dispatch(eventName, { pageNumber });
	}

	$: lastPage = itemsPerPage < 1 || totalItems < 1 ? 0 : Math.ceil(totalItems / itemsPerPage);

	/**
	 * @param {Number} currentPage
	 * @param {Number} lastPage
	 * @param {Number} maxSize
	 */
	function generateSize(currentPage, lastPage, maxSize) {
		const length = lastPage < maxSize ? lastPage : maxSize;
		const startLastPage = lastPage - length;
		let startPage = 1;

		if (currentPage >= length && length < lastPage) startPage = currentPage - 1;

		if (currentPage > startLastPage && currentPage <= lastPage) {
			startPage = startLastPage + 1;
		}
		return Array.from({ length }, (_, idx) => startPage + idx);
	}

	$: pages = [...generateSize(pageNumber, lastPage, maxSize)];
</script>

{#if lastPage && itemsPerPage}
	<ul class="pagination">
		{#if firstText}
			<li class="page-item" class:disabled={pageNumber <= 1} title="First">
				<a class="page-link" href={'#'} aria-label="First" on:click={() => onClickPage(1)}>
					<span aria-hidden="true">{firstText}</span>
				</a>
			</li>
		{/if}

		<li class="page-item" class:disabled={pageNumber <= 1}>
			<a
				class="page-link"
				href={'#'}
				aria-label="Previous"
				on:click={() => onClickPage(pageNumber - 1)}
				title="Previous"
			>
				<span aria-hidden="true">{previousText}</span>
			</a>
		</li>

		{#each pages as page}
			<li class="page-item" class:active={page === pageNumber}>
				<a class="page-link" href={'#'} on:click={() => onClickPage(page)}>
					<span aria-hidden="true">{page}</span>
				</a>
			</li>
		{/each}

		<li class="page-item" class:disabled={pageNumber === lastPage} title="Next">
			<a
				class="page-link"
				href={'#'}
				aria-label="Next"
				on:click={() => onClickPage(pageNumber + 1)}
			>
				<span aria-hidden="true">{nextText}</span>
			</a>
		</li>

		{#if lastText}
			<li class="page-item" class:disabled={pageNumber === lastPage} title="Last">
				<a class="page-link" href={'#'} aria-label="Last" on:click={() => onClickPage(lastPage)}>
					<span aria-hidden="true">{lastText}</span>
				</a>
			</li>
		{/if}
	</ul>
{/if}
