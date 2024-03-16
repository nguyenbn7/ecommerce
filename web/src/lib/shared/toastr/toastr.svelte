<script>
	import AppToast from '../toast/app-toast.svelte';
	import { saveShowSingleToastOption, toastService, toastsService } from './service';

	export let showSingleToast = true;
	export { classNames as class };
	/**
	 * @type {string}
	 */
	let classNames = 'toast-container position-fixed top-0 end-0 p-4 mt-5 me-5';

	saveShowSingleToastOption(showSingleToast);

	$: singleToast = $toastService;
</script>

<div class={classNames}>
	{#if showSingleToast}
		{#if singleToast}
			{#key singleToast}
				<AppToast bind:message={singleToast.message} type={singleToast.type} />
			{/key}
		{/if}
	{:else}
		{#each $toastsService as toast}
			<AppToast message={toast.message} type={toast.type} />
		{/each}
	{/if}
</div>
