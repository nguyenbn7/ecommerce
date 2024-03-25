import { writable } from 'svelte/store';

export default class ReactiveFormField {
	/**
	 * @type {HTMLElement | null}
	 */
	#instance = null;
	#history = writable(this);

	#touched = false;
	#dirty = false;
	#valid = false;
	/**
	 * @type {any}
	 */
	#value = '';
	#oldValue = '';

	/**
	 * @type {string | null}
	 */
	#invalidFeedback = null;
	/**
	 * @type {string | null}
	 */
	#validFeedback = null;

	/**
	 * @type {Validator[]}
	 */
	#validators;

	set instance(node) {
		const makeFieldTouched = () => {
			this.#touched = true;
			this.#validate();
			this.#notifyFieldHasChanged();
		};

		const validateFieldWhenFocusout = () => {
			this.#dirty = this.#oldValue != this.#value;

			if (this.#dirty) {
				this.#validate();
				this.#notifyFieldHasChanged();
			}
		};

		if (node) {
			this.#instance = node;
			this.#instance.addEventListener('focusin', makeFieldTouched);
			this.#instance.addEventListener('focusout', validateFieldWhenFocusout);
			return;
		}

		this.#instance?.removeEventListener('focusin', makeFieldTouched);
		this.#instance?.removeEventListener('focusout', validateFieldWhenFocusout);
		this.#instance = null;
	}

	get instance() {
		return this.#instance;
	}

	/**
	 * @param {boolean} value
	 */
	set touched(value) {
		this.#touched = value;
		if (this.#touched) {
			this.#validate();
			this.#notifyFieldHasChanged();
		}
	}

	get touched() {
		return this.#touched;
	}

	/**
	 * @param {boolean} value
	 */
	set dirty(value) {
		this.#dirty = value;
		this.#notifyFieldHasChanged();
	}

	get dirty() {
		return this.#dirty;
	}

	set value(value) {
		this.#oldValue = this.#value;
		this.#value = value;
		this.dirty = true;
		this.#validate();
		this.#notifyFieldHasChanged();
	}

	get value() {
		return this.#value;
	}

	get valid() {
		return this.#valid;
	}

	get invalidFeedback() {
		return this.#invalidFeedback;
	}

	get validFeedback() {
		return this.#validFeedback;
	}

	get validators() {
		return this.#validators;
	}

	#validate() {
		this.#invalidFeedback = null;
		this.#validFeedback = null;

		this.#valid = false;

		for (const validator of this.validators) {
			const { check, errorMessage } = validator;
			if (!check(this.#value)) {
				this.#invalidFeedback = errorMessage;
				return;
			}
		}

		this.#valid = true;
	}

	#notifyFieldHasChanged() {
		this.instance?.dispatchEvent(new CustomEvent('fieldHasChanged', { bubbles: true }));
		this.#history.update((_) => this);
	}

	#validateAndNotify() {
		this;
	}

	/**
	 * @param {import("svelte/store").Subscriber<this>} subscriber
	 */
	subscribe(subscriber) {
		return this.#history.subscribe(subscriber);
	}

	/**
	 * @param {Validator[]} validators
	 */
	static createOptionalField(...validators) {
		return new ReactiveFormField(true, ...validators);
	}

	/**
	 * @param {Validator[]} validators
	 */
	static createField(...validators) {
		return new ReactiveFormField(false, ...validators);
	}

	/**
	 * @param {Validator[]} validators
	 */
	constructor(optional = false, ...validators) {
		this.#valid = optional;
		this.#validators = validators ?? [];
	}
}
