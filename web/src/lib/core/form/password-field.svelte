<script>
	import ValidationFeedback from './validation-feedback.svelte';
	
	export { classNames as class };
	let classNames = '';

	export let inputAbove = false;
	export let name = '';
	export let validationFeedback = false;
	/**
	 * @type {null | string}
	 */
	export let placeholder = null;

	/**
	 * @type {import('./reactive.field').default}
	 */
	export let reactiveFormField;
	/**
	 * @type {any}
	 */
	export let validCondition;
	/**
	 * @type {any}
	 */
	export let invalidCondition;
</script>

{#if inputAbove}
	<input
		class={classNames}
		type="password"
		{name}
		id={name}
		class:is-invalid={invalidCondition}
		class:is-valid={validCondition}
		bind:this={reactiveFormField.instance}
		bind:value={reactiveFormField.value}
		{placeholder}
	/>
	<slot name="label" />
{:else}
	<slot name="label" />
	<input
		class={classNames}
		type="password"
		{name}
		id={name}
		class:is-invalid={invalidCondition}
		class:is-valid={validCondition}
		bind:this={reactiveFormField.instance}
		bind:value={reactiveFormField.value}
		{placeholder}
	/>
{/if}

{#if validationFeedback}
	<ValidationFeedback bind:reactiveFormField />
{/if}
