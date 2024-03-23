import { writable } from 'svelte/store';

export default class ReactiveFormBase {
    /**
     * @type {HTMLElement | null}
     */
    #instance = null;
    #history = writable(this);

    /**
     * @this {{ [field: string]: import('./reactive.field.js').default | ReactiveFormBase }}
     * @returns {boolean}
     */
    get valid() {
        return Object.keys(this).every((propName) => this[propName]?.valid ?? true);
    }

    /**
     * @this {{ [field: string]: import('./reactive.field.js').default | ReactiveFormBase }}
     * @type {boolean}
     */
    get touched() {
        return Object.keys(this).every((propName) => this[propName]?.touched ?? true);
    }

    /**
     * @this {{ [field: string]: import('./reactive.field.js').default | ReactiveFormBase }}
     * @type {boolean}
     */
    get dirty() {
        return Object.keys(this).every((propName) => this[propName]?.dirty ?? true);
    }

    set instance(node) {
        const updateHistory = () => {
            this.#history.update(_ => this);
        };

        if (node) {
            this.#instance = node;

            this.#instance.addEventListener('fieldHasChanged', updateHistory);
            return;
        }

        this.#instance?.removeEventListener('fieldHasChanged', updateHistory);
        this.#instance = null;
    }

    get instance() {
        return this.#instance;
    }

    /**
     * @param {import("svelte/store").Subscriber<this>} subscriber
     */
    subscribe(subscriber) {
        return this.#history.subscribe(subscriber);
    }
}