<script>
	/**
	 * @type {string | undefined}
	 */
	export let message;
	/**
	 * @type {ToastType}
	 */
	export let type;
	export let hideOnClick = true;
	export let delayHidingMS = 5000;

	$: msg = message;

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

	$: classIcon = getToastIconClass(type);

	function hide() {
		if (hideOnClick) msg = undefined;
	}

	const timeout = setTimeout(() => {
		msg = undefined;
		clearTimeout(timeout);
	}, delayHidingMS);
</script>

{#if msg}
	<!-- svelte-ignore a11y-click-events-have-key-events -->
	<div class="toast fade show" tabindex="-1" role="button" on:click={hide}>
		<div
			class="row text-white"
			class:bg-danger={type === 'DANGER'}
			class:bg-success={type === 'SUCCESS'}
			class:bg-warning={type === 'WARNING'}
			class:bg-info={type === 'INFO'}
		>
			<div class="col-3 text-center ps-0">
				<i class={`${classIcon} toast-icon`}></i>
			</div>
			<div class="col-9 py-2 pe-1 fs-6">
				<div class="text-wrap text-break">{message}</div>
			</div>
		</div>
	</div>
{/if}

<style lang="scss">
	.toast-icon {
		font-size: 2.5em;
		margin: 0.25em 0;
	}
</style>
