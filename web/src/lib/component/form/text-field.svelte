<script>
	import ValidationFeedback from './validation-feedback.svelte';

	export { classNames as class };
	let classNames = '';

	export let inputAbove = false;

	export let validationFeedback = false;

	export let name = '';

	export let disabled = false;

	/**
	 * @type {null | string}
	 */
	export let placeholder = null;

	/**
	 * @type {import('$lib/core/form/reactive.field').default}
	 */
	export let reactiveFormField;

	$: valid = $reactiveFormField.valid && $reactiveFormField.dirty;

	$: invalid = $reactiveFormField.touched && !$reactiveFormField.valid;
</script>

{#if inputAbove}
	<input
		class={classNames}
		{name}
		id={name}
		class:is-invalid={invalid}
		class:is-valid={valid}
		bind:this={reactiveFormField.instance}
		bind:value={reactiveFormField.value}
		{placeholder}
		{disabled}
		{...$$restProps}
	/>
	<slot name="label" />
{:else}
	<slot name="label" />
	<input
		class={classNames}
		{name}
		id={name}
		class:is-invalid={invalid}
		class:is-valid={valid}
		bind:this={reactiveFormField.instance}
		bind:value={reactiveFormField.value}
		{placeholder}
		{...$$restProps}
	/>
{/if}

{#if validationFeedback}
	<ValidationFeedback reactiveFormField={$reactiveFormField} />
{/if}
