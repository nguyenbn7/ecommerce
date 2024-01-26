<script>
	/**
	 * @type {import("$lib/module/form").FormField}
	 */
	export let field;
	/**
	 * @type {string}
	 */
	export let invalidStyle = 'invalid-feedback';
	/**
	 * @type {string}
	 */
	export let validStyle = 'valid-feedback';
</script>

{#if field.touched && !field.valid && field.error}
	<div class={invalidStyle}>
		{field.error}
	</div>
{:else if field.touched && field.valid && field.success}
	<div class={validStyle}>
		{field.success}
	</div>
{/if}
