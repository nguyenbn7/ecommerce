<script>
	import { onMount } from 'svelte';

	/**
	 * @type {string | null}
	 *
	 * @default null
	 */
	export let message;

	/**
	 * @type {'DANGER' | 'INFO' | 'SUCCESS' | 'WARNING'}
	 *
	 * @default SUCCESS
	 */
	export let type = 'SUCCESS';

	/**
	 * Apply a CSS fade transition to the toast
	 *
	 * @default true
	 */
	export let animation = true;

	/**
	 * Auto hide the toast
	 *
	 * @default true
	 */
	export let autohide = true;

	/**
	 * Delay hiding the toast (ms)
	 *
	 * @default 5000
	 */
	export let delay = 5000;
	/**
	 * Click toast to hide
	 *
	 * @default true
	 */
	export let hideOnClick = true;

	/**
	 * @type {HTMLElement}
	 */
	let instance;

	/**
	 * @type {import("bootstrap").Toast | null}
	 */
	let boostrapToast;

	/**
	 * @param {'DANGER' | 'INFO' | 'SUCCESS' | 'WARNING'} toastType
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

	function hide() {
		if (hideOnClick) boostrapToast?.hide();
	}

	onMount(async () => {
		boostrapToast = new (await import('bootstrap')).Toast(instance, { animation, delay, autohide });
		boostrapToast.show();

		const timeout = setTimeout(() => {
			clearTimeout(timeout);
			instance.parentNode?.removeChild(instance);
		}, delay + 500);
	});
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<div class="toast fade" tabindex="-1" role="button" on:click={hide} bind:this={instance}>
	<div
		class="row text-white"
		class:bg-danger={type === 'DANGER'}
		class:bg-success={type === 'SUCCESS'}
		class:bg-warning={type === 'WARNING'}
		class:bg-info={type === 'INFO'}
	>
		<div class="col-3 text-center ps-0">
			<i class={`${getToastIconClass(type)} toast-icon`}></i>
		</div>
		<div class="col-9 py-2 pe-2 fs-6">
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
