<script>
	/**
	 * @type {import('$lib/core/form/reactive.field').default}
	 */
	export let reactiveFormField;
</script>

<div
	class:invalid-feedback={reactiveFormField.invalidFeedback}
	class:valid-feedback={reactiveFormField.validFeedback}>
	{reactiveFormField.invalidFeedback || reactiveFormField.validFeedback || ''}
</div>
