<script context="module">
	import { writable } from 'svelte/store';

	/**
	 * @type {import('svelte/store').Writable<{message:string, type: ToastType}[]>}
	 */
	const toastsStore = writable([]);

	let showOnlyOneToast = false;

	/**
	 * @param {string} message
	 * @param {ToastType} type
	 */
	export function notify(message, type) {
		if (showOnlyOneToast) {
			toastsStore.set([{ message, type }]);
			return;
		}

		toastsStore.update((toasts) => {
			toasts.push({ message, type });
			return toasts;
		});
	}

	/**
	 * @param {string} message
	 */
	export function notifyDanger(message) {
		notify(message, 'DANGER');
	}

	/**
	 * @param {string} message
	 */
	export function notifySuccess(message) {
		notify(message, 'SUCCESS');
	}

	/**
	 * @param {string} message
	 */
	export function notifyWarning(message) {
		notify(message, 'WARNING');
	}

	/**
	 * @param {string} message
	 */
	export function notifyInfo(message) {
		notify(message, 'INFO');
	}
</script>

<script>
	import Toast, { hide } from './toast.svelte';

	export { classNames as class };
	/**
	 * @type {string}
	 */
	let classNames = 'toast-container position-fixed top-0 end-0 p-4 mt-5 me-5';

	export let singleToast = false;

	showOnlyOneToast = singleToast;
	$: toasts = $toastsStore;
</script>

<div class={classNames}>
	{#each toasts as toast}
		{#key toast}
			<Toast message={toast.message} type={toast.type} on:click={hide} />
		{/key}
	{/each}
</div>
