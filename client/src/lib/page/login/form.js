import { FormField, FormGroup, Validators } from "$lib/module/form";

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