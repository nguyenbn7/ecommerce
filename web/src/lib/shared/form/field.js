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
	/**
	 * @type {HTMLElement | null}
	 */
	#node = null;
	#history = writable(this);
	/**
	 * @type {HTMLElement | null} 
	 */
	#instance = null;

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
		this.#node?.dispatchEvent(new CustomEvent('fieldHasChanged', { bubbles: true }));
		this.#validateAndUpdateHistory();
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

	#validateAndUpdateHistory() {
		this.#validate();
		this.#history.update(_ => this);
	}

	/** @type {import('svelte/action').Action}  */
	bind(node) {
		this.#node = node;

		const onFocusOut = (/** @type {Event} */ $event) => {
			this.#touched = true;
			this.#dirty = false; // not sure about this field but just a dummy (not gonna to use it)
			this.#validateAndUpdateHistory();
		};

		const onInput = (/** @type {Event} */ $event) => {
			const target = $event.target;
			if (!target) return;

			this.#dirty = true; // not sure about this field but just a dummy (not gonna to use it)
			this.#value = /** @type {HTMLInputElement} */ (target).value;

			this.#validateAndUpdateHistory();
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
	 * @param {import("svelte/store").Subscriber<this>} subscriber
	 */
	subscribe(subscriber) {
		return this.#history.subscribe(subscriber);
	}

	/**
	 * @param {HTMLElement} node
	 */
	set instance(node) {
		const onFocusOut = (/** @type {Event} */ $event) => {
			this.#touched = true;
			this.#dirty = false; // not sure about this field but just a dummy (not gonna to use it)
			this.#validateAndUpdateHistory();
		};

		const onInput = (/** @type {Event} */ $event) => {
			const target = $event.target;
			if (!target) return;

			this.#dirty = true; // not sure about this field but just a dummy (not gonna to use it)
			this.#value = /** @type {HTMLInputElement} */ (target).value;

			this.#validateAndUpdateHistory();
		};

		if (node) {
			this.#instance = node;
			console.log(this.#instance);

			this.#instance.addEventListener('focusout', onFocusOut);
			this.#instance.addEventListener('input', onInput);
			return;
		}
		this.#instance?.removeEventListener('focusout', onFocusOut);
		this.#instance?.removeEventListener('input', onInput);
		this.#instance = null;
		console.log(this.#instance);
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
