<script>
	import ValidationFeedback from './validation-feedback.svelte';

	export { classNames as class };
	let classNames = '';

	export let inputAbove = false;

	export let name = '';

	export let validationFeedback = false;

	export let disabled = false;

	/**
	 * @type {null | string}
	 */
	export let placeholder = null;

	/**
	 * @type {import('./reactive.field').default}
	 */
	export let reactiveFormField;

	$: validCondition = reactiveFormField.valid;

	$: invalidCondition = reactiveFormField.touched && !reactiveFormField.valid;
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
		on:focusout={(_) => (reactiveFormField.touched = true)}
		{placeholder}
		{disabled}
		{...$$restProps}
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
		on:focusout={(_) => (reactiveFormField.touched = true)}
		{placeholder}
		{disabled}
		{...$$restProps}
	/>
{/if}

{#if validationFeedback}
	<ValidationFeedback bind:reactiveFormField />
{/if}
