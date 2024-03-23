import { get, writable } from 'svelte/store';

export class FormGroup {
	#history = writable(this);

	/**
	 * @this {{[field: string]: import('./field').FormField | FormGroup}}
	 * @returns {boolean}
	 */
	get isValid() {
		return Object.keys(this).every((propName) => this[propName]?.isValid ?? true);
	}

	/**
	 * @this {{[field: string]: import('./field').FormField | FormGroup}}
	 * @type {boolean}
	 */
	get isTouched() {
		return Object.keys(this).every((propName) => this[propName]?.isTouched ?? true);
	}

	/**
	 * @this {{[field: string]: import('./field').FormField | FormGroup}}
	 * @type {boolean}
	 */
	get isDirty() {
		return Object.keys(this).every((propName) => this[propName]?.isDirty ?? true);
	}

	/** @type {import('svelte/action').Action}  */
	bind(node) {
		const update = (/** @type {Event} */ $event) => {
			this.#history.update(_ => this);
		};

		node.addEventListener('input', update);
		node.addEventListener('focusout', update);
		node.addEventListener('fieldHasChanged', update);

		return {
			destroy() {
				node.removeEventListener('input', update);
				node.removeEventListener('focusout', update);
				node.removeEventListener('fieldHasChanged', update);
			}
		};
	}

	/**
	 * @param {import("svelte/store").Subscriber<this>} subscriber
	 */
	subscribe(subscriber) {
		return this.#history.subscribe(subscriber);
	}
}