import { writable } from 'svelte/store';

export class FormGroup {
	#history = writable(this);

	/**
	 * @this {{[field: string]: import('./form.field').FormField | FormGroup}}
	 * @returns {boolean}
	 */
	get isValid() {
		return Object.keys(this).every((propName) => this[propName]?.isValid ?? true);
	}

	/**
	 * @this {{[field: string]: import('./form.field').FormField | FormGroup}}
	 * @type {boolean}
	 */
	get isTouched() {
		return Object.keys(this).every((propName) => this[propName]?.isTouched ?? true);
	}

	/**
	 * @this {{[field: string]: import('./form.field').FormField | FormGroup}}
	 * @type {boolean}
	 */
	get isDirty() {
		return Object.keys(this).every((propName) => this[propName]?.isDirty ?? true);
	}

	/** @type {import('svelte/action').Action}  */
	bind(node) {
		const update = (/** @type {Event} */ $event) => {
			$event.preventDefault();
			this.#history.set(this);
		};

		node.addEventListener('fieldHasChanged', update);

		return {
			destroy() {
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
