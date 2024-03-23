import ReactiveFormBase from '$lib/core/form/reactive';
import ReactiveFormField from '$lib/core/form/reactive.field';
import {
	checkEmailFormat,
	checkMaxLength,
	checkRequired,
	containsAlnumAndSpace,
	doesFieldEqualTo,
	isPasswordComplexEnough
} from '$lib/core/form/validator';

export class LoginForm extends ReactiveFormBase {
	constructor() {
		super();
		this.emailField = new ReactiveFormField(
			checkRequired('Email is required'),
			checkEmailFormat('Incorrect email. Example: bob@test.com')
		);
		this.passwordField = new ReactiveFormField(checkRequired('Password is required'));
	}
}

export class RegisterForm extends ReactiveFormBase {
	constructor(fullNameMaxLen = 256, displayNameMaxLen = 55) {
		super();

		this.fullNameField = new ReactiveFormField(
			checkRequired('Full name is required'),
			containsAlnumAndSpace('Full name contains only letters and spaces'),
			checkMaxLength(`Full name's max length is ${fullNameMaxLen} characters`, fullNameMaxLen)
		);

		this.displayNameField = new ReactiveFormField(
			checkRequired('Display name is required'),
			containsAlnumAndSpace('Display name contains only letters and spaces'),
			checkMaxLength(
				`Display name's max length is ${displayNameMaxLen} characters`,
				displayNameMaxLen
			)
		);

		this.emailField = new ReactiveFormField(
			checkRequired('Email is required'),
			checkEmailFormat('Incorrect email. Example: bob@test.com')
		);

		this.passwordField = new ReactiveFormField(
			checkRequired('Password is required'),
			isPasswordComplexEnough(
				'Password must have At least 8 characters long. - At least 1 uppercase, AND at least 1 lowercase - At least 1 digit OR at least 1 alphanumeric'
			)
		);

		this.confirmPasswordField = new ReactiveFormField(
			checkRequired('Confirm Password is required'),
			doesFieldEqualTo(this.passwordField, 'Confirm Password does not match with Password')
		);
	}
}
