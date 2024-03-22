import { writable } from 'svelte/store';

export class FormField {
	/*** @type {boolean} */
	#dirty;
	/*** @type {boolean} */
	#touched;
	/*** @type {boolean} */
	#valid;
	/*** @type {string} */
	#value;
	/*** @type {string | undefined} */
	#error;
	/*** @type {string | undefined} */
	#success;
	/*** @type {Validator[]} */
	#validators;
	#history = writable(this);

	get isDirty() {
		return this.#dirty;
	}

	get isTouched() {
		return this.#touched;
	}

	get isValid() {
		return this.#valid;
	}

	set value(newValue) {
		this.#value = newValue;
		this.#dirty = true;
		this.#touched = true;
		this.#validate();
	}

	get value() {
		return this.#value;
	}

	get error() {
		return this.#error;
	}

	get success() {
		return this.#success;
	}

	get validators() {
		return this.#validators;
	}

	#validate() {
		this.#valid = false;

		for (const validator of this.validators) {
			const { check, errorMessage } = validator;
			if (!check(this.#value)) {
				this.#error = errorMessage;
				return;
			}
		}

		this.#valid = true;
	}

	/**
	 * @param {HTMLElement} node
	 */
	#validateAndNotifyParent(node) {
		this.#validate();
		this.#history.set(this);
		node.dispatchEvent(new CustomEvent('fieldHasChanged', { bubbles: true }));
	}

	/** @type {import('svelte/action').Action}  */
	bind(node) {
		const onFocusOut = (/** @type {Event} */ $event) => {
			$event.stopPropagation();
			this.#touched = true;
			this.#dirty = false; // not sure about this field but just a dummy (not gonna to use it)
			this.#validateAndNotifyParent(node);
		};

		const onInput = (/** @type {Event} */ $event) => {
			$event.stopPropagation();
			const target = $event.target;
			if (!target) return;

			this.#dirty = true; // not sure about this field but just a dummy (not gonna to use it)
			this.#value = /** @type {HTMLInputElement} */ (target).value;

			this.#validateAndNotifyParent(node);
		};

		node.addEventListener('focusout', onFocusOut);
		node.addEventListener('input', onInput);

		return {
			destroy() {
				node.removeEventListener('focusout', onFocusOut);
				node.removeEventListener('input', onInput);
			}
		};
	}

	/**
	 * @param {Validator[]} validators
	 */
	constructor(...validators) {
		this.#dirty = false;
		this.#touched = false;
		this.#valid = false;
		this.#value = '';
		this.#error = undefined;
		this.#success = undefined;
		this.#validators = validators ?? [];
	}
}
