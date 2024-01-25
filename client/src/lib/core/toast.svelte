<script context="module">
	import { Toast } from 'bootstrap';

	/**
	 * @type {HTMLDivElement}
	 */
	let ele;

	export function hide() {
		/**
		 * @type {import("bootstrap").Toast | null}
		 */
		let toast = Toast.getInstance(ele);

		if (!toast) return;

		if (toast.isShown()) toast.hide();
	}
</script>

<script>
	import { onMount } from 'svelte';

	/**
	 * @type {string}
	 */
	export let message;
	/**
	 * @type {ToastType}
	 */
	export let type;

	/**
	 * @param {ToastType} toastType
	 */
	function getToastIconClass(toastType) {
		switch (toastType) {
			case 'DANGER':
				return 'fa-solid fa-circle-xmark';
			case 'INFO':
				return 'fa-solid fa-circle-info';
			case 'WARNING':
				return 'fa-solid fa-circle-exclamation';
			default:
				return 'fa-solid fa-circle-check';
		}
	}

	const classIcon = getToastIconClass(type);

	onMount(() => {
		let toast = new Toast(ele);

		toast.show();
		// @ts-ignore
		setTimeout(() => ele.parentNode?.removeChild(ele), toast._config.delay + 1000);
	});
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<div
	class="toast"
	aria-live="assertive"
	aria-atomic="true"
	tabindex="-1"
	bind:this={ele}
	role="button"
	on:click
>
	<div
		class="row text-white"
		class:bg-danger={type === 'DANGER'}
		class:bg-success={type === 'SUCCESS'}
		class:bg-warning={type === 'WARNING'}
		class:bg-info={type === 'INFO'}
	>
		{#if classIcon}
			<div class="col-3 text-center ps-0">
				<i class={`${classIcon} toast-icon`}></i>
			</div>
		{/if}
		<div class="col-9 py-2 pe-1 fs-6">
			<div class="text-wrap text-break">{message}</div>
		</div>
	</div>
</div>

<style lang="scss">
	.toast-icon {
		font-size: 2.5em;
		margin: 0.25em 0;
	}
</style>
