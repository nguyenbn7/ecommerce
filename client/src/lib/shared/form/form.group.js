
export class FormGroup {
    /**
     * @this {{[field: string]: import('./form.field').FormField | FormGroup}}
     * @returns {boolean}
     */
    get valid() {
        return Object.keys(this).every((propName) => this[propName]?.valid ?? true);
    }

    /**
     * @this {{[field: string]: import('./form.field').FormField | FormGroup}}
     * @type {boolean}
     */
    get touched() {
        return Object.keys(this).every((propName) => this[propName]?.touched ?? true);
    }

    /**
     * @this {{[field: string]: import('./form.field').FormField | FormGroup}}
     * @type {boolean}
     */
    get dirty() {
        return Object.keys(this).every((propName) => this[propName]?.dirty ?? true);
    }
}