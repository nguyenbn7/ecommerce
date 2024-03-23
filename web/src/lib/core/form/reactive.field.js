import { writable } from 'svelte/store';

export default class ReactiveFormField {
    /**
     * @type {HTMLElement | null}
     */
    instance = null;
    #history = writable(this);

    #touched = false;
    #dirty = false;
    #valid = false;
    #value = '';

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

    /**
     * @param {boolean} value
     */
    set touched(value) {
        this.#touched = value;
        this.#notifyFieldHasChanged();
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
        this.#value = value;
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

    /**
     * @param {import("svelte/store").Subscriber<this>} subscriber
     */
    subscribe(subscriber) {
        return this.#history.subscribe(subscriber);
    }

    /**
     * @param {Validator[]} validators
     */
    constructor(optionalField = false, ...validators) {
        this.#validators = validators ?? [];
    }
}