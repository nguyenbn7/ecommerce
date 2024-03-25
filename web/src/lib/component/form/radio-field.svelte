<script>
	export { classNames as class };
	let classNames = '';

	export let inputAbove = false;
	export let name = '';

	/**
	 * @type {any}
	 */
	export let value;
	/**
	 * @type {import('$lib/core/form/reactive.field').default}
	 */
	export let reactiveFormField;
	export let disabled = false;
</script>

{#if inputAbove}
	<input
		class={classNames}
		type="radio"
		{name}
		id={name}
		bind:this={reactiveFormField.instance}
		bind:group={reactiveFormField.value}
		{value}
		{disabled}
	/>
	<slot name="label" />
{:else}
	<slot name="label" />
	<input
		class={classNames}
		type="radio"
		{name}
		id={name}
		bind:this={reactiveFormField.instance}
		bind:group={reactiveFormField.value}
		{value}
		{disabled}
	/>
{/if}
