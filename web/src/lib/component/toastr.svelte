<script context="module">
	import { writable } from 'svelte/store';

	/**
	 * @type {import('svelte/store').Writable<{message:string, type: 'DANGER' | 'INFO' | 'SUCCESS' | 'WARNING'}[]>}
	 */
	const toastsStore = writable([]);

	/**
	 * @param {string} message
	 * @param {'DANGER' | 'INFO' | 'SUCCESS' | 'WARNING'} type
	 */
	export function show(message, type) {
		toastsStore.update((toasts) => [...toasts, { message, type }]);
	}

	/**
	 * @param {string} message
	 */
	export function showDanger(message) {
		show(message, 'DANGER');
	}

	/**
	 * @param {string} message
	 */
	export function showSuccess(message) {
		show(message, 'SUCCESS');
	}

	/**
	 * @param {string} message
	 */
	export function showWarning(message) {
		show(message, 'WARNING');
	}

	/**
	 * @param {string} message
	 */
	export function showInfo(message) {
		show(message, 'INFO');
	}
</script>

<script>
	import Toast from './toast.svelte';

	/**
	 * @type {typeof Toast | null}
	 *
	 * @default Toast
	 */
	export let toastComponent = Toast;

	/**
	 * If true, container at the top of the page, otherwise will be at the bottom.
	 *
	 * @default true
	 */
	export let top = true;

	/**
	 * If true, container on the right of the page, otherwise will be on the left.
	 *
	 * @default true
	 */
	export let right = true;

	/**
	 * component style class
	 */
	export { classNames as class };
	/**
	 * @type {string}
	 */
	let classNames = '';
</script>

<div
	class={`toast-container position-fixed ${classNames.trim()}`.trim()}
	class:top-0={top}
	class:bottom-0={!top}
	class:end-0={right}
	class:start-0={!right}>
	{#each $toastsStore as toast}
		<svelte:component this={toastComponent} message={toast.message} type={toast.type} />
	{/each}
</div>
