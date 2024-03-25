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

export default class RegisterForm extends ReactiveFormBase {
	constructor(fullNameMaxLen = 256, displayNameMaxLen = 55) {
		super();

		this.fullName = ReactiveFormField.createField(
			checkRequired('Full name is required'),
			containsAlnumAndSpace('Full name contains only letters and spaces'),
			checkMaxLength(`Full name's max length is ${fullNameMaxLen} characters`, fullNameMaxLen)
		);

		this.displayName = ReactiveFormField.createField(
			checkRequired('Display name is required'),
			containsAlnumAndSpace('Display name contains only letters and spaces'),
			checkMaxLength(
				`Display name's max length is ${displayNameMaxLen} characters`,
				displayNameMaxLen
			)
		);

		this.email = ReactiveFormField.createField(
			checkRequired('Email is required'),
			checkEmailFormat('Incorrect email. Example: bob@test.com')
		);

		this.password = ReactiveFormField.createField(
			checkRequired('Password is required'),
			isPasswordComplexEnough(
				'Password must have At least 8 characters long. - At least 1 uppercase, AND at least 1 lowercase - At least 1 digit OR at least 1 alphanumeric'
			)
		);

		this.confirmPassword = ReactiveFormField.createField(
			checkRequired('Confirm Password is required'),
			doesFieldEqualTo(this.password, 'Confirm Password does not match with Password')
		);

		this.agree = ReactiveFormField.createField(checkRequired(''));
	}
}
