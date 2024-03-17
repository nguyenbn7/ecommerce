import { FormField, FormGroup, Validators } from '$lib/shared/form';

export class LoginForm extends FormGroup {
	constructor() {
		super();
		this.emailField = new FormField(
			Validators.checkRequired('Email is required'),
			Validators.checkEmailFormat('Incorrect email. Example: bob@test.com')
		);
		this.passwordField = new FormField(Validators.checkRequired('Password is required'));
	}
}

export class RegisterForm extends FormGroup {
	constructor(fullNameMaxLen = 256, displayNameMaxLen = 55) {
		super();

		this.fullNameField = new FormField(
			Validators.checkRequired('Full name is required'),
			Validators.containsAlnumAndSpace('Full name contains only letters and spaces'),
			Validators.checkMaxLength(
				`Full name's max length is ${fullNameMaxLen} characters`,
				fullNameMaxLen
			)
		);

		this.displayNameField = new FormField(
			Validators.checkRequired('Display name is required'),
			Validators.containsAlnumAndSpace('Display name contains only letters and spaces'),
			Validators.checkMaxLength(
				`Display name's max length is ${displayNameMaxLen} characters`,
				displayNameMaxLen
			)
		);

		this.emailField = new FormField(
			Validators.checkRequired('Email is required'),
			Validators.checkEmailFormat('Incorrect email. Example: bob@test.com')
		);

		this.passwordField = new FormField(
			Validators.checkRequired('Password is required'),
			Validators.isPasswordComplexEnough(
				'Password must have At least 8 characters long. - At least 1 uppercase, AND at least 1 lowercase - At least 1 digit OR at least 1 alphanumeric'
			)
		);

		this.confirmPasswordField = new FormField(
			Validators.checkRequired('Confirm Password is required'),
			Validators.doesFieldEqualTo(
				this.passwordField,
				'Confirm Password does not match with Password'
			)
		);
	}
}
