<script context="module">
	import ReactiveFormBase from '$lib/core/form/reactive';
	import ReactiveFormField from '$lib/core/form/reactive.field';
	import { checkRequired } from '$lib/core/form/validator';

	class TestForm extends ReactiveFormBase {
		constructor() {
			super();
			this.fullName = new ReactiveFormField(false, checkRequired());
		}
	}
</script>

<script>
	import RadioField from '$lib/core/form/radio-field.svelte';
	import ValidationFeedback from '$lib/core/form/validation-feedback.svelte';

	let testForm = new TestForm();
	/**
	 * @param {SubmitEvent} $event
	 */
	function onSubmitForm($event) {
		console.log('submit form');
	}

	$: console.log(testForm.fullName.value);
	$: console.log(testForm.fullName.valid);
</script>

<form on:submit|preventDefault={onSubmitForm} bind:this={testForm.instance}>
	<div class="form-check">
		<RadioField
			class="form-check-input"
			name="flexRadioDefault"
			bind:reactiveFormField={testForm.fullName}
			inputAbove={true}
			value={1}
		>
			<svelte:fragment slot="label">
				<label class="form-check-label" for="flexRadioDefault"> Default radio </label>
			</svelte:fragment>
		</RadioField>
	</div>
	<div class="form-check">
		<RadioField
			class="form-check-input"
			name="flexRadioDefault2"
			bind:reactiveFormField={testForm.fullName}
			inputAbove={true}
			value={2}
		>
			<svelte:fragment slot="label">
				<label class="form-check-label" for="flexRadioDefault2"> Default checked radio </label>
			</svelte:fragment>
		</RadioField>
	</div>
	<ValidationFeedback bind:reactiveFormField={testForm.fullName} />
</form>
