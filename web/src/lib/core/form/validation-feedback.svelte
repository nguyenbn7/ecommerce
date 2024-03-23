<script>
	/**
	 * @type {import('./reactive.field').default}
	 */
	export let reactiveFormField;
</script>

{#if reactiveFormField.invalidFeedback || reactiveFormField.validFeedback}
	<div
		class:invalid-feedback={reactiveFormField.invalidFeedback}
		class:valid-feedback={reactiveFormField.validFeedback}
	>
		{reactiveFormField.invalidFeedback || reactiveFormField.validFeedback}
	</div>
{/if}
