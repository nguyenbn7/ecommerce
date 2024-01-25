import { FormField, FormGroup } from "../form";
import { Validators } from "../form/validation-feedback.svelte";

export class LoginForm extends FormGroup {
    constructor() {
        super();
        this.email = new FormField(
            Validators.checkRequired('Email is required'),
            Validators.checkEmailFormat('Incorrect email. Example: bob@test.com')
        );
        this.password = new FormField(Validators.checkRequired('Password is required'));
    }
}