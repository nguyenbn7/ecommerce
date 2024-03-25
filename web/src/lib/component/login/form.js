import ReactiveFormBase from '$lib/core/form/reactive';
import ReactiveFormField from '$lib/core/form/reactive.field';
import { checkEmailFormat, checkRequired } from '$lib/core/form/validator';

export class LoginForm extends ReactiveFormBase {
	constructor() {
		super();
		this.email = ReactiveFormField.createField(
			checkRequired('Email is required'),
			checkEmailFormat('Incorrect email. Example: bob@test.com')
		);
		this.password = ReactiveFormField.createField(checkRequired('Password is required'));
	}
}
