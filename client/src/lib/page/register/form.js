import { FormField, FormGroup, Validators } from "$lib/module/form";

export class RegisterForm extends FormGroup {
    constructor(firstNameMaxLen = 255, lastNameMaxLen = 255) {
        super();

        this.firstName = new FormField(
            Validators.checkRequired('First Name is required'),
            Validators.containsAlnumAndSpace('First Name contains only letters and spaces'),
            Validators.checkMaxLength(
                `First Name's max length is ${firstNameMaxLen} characters`,
                firstNameMaxLen
            )
        );

        this.lastName = new FormField(
            Validators.checkRequired('Last Name is required'),
            Validators.containsAlnumAndSpace('Last Name contains only letters and spaces'),
            Validators.checkMaxLength(
                `Last Name's max length is ${lastNameMaxLen} characters`,
                lastNameMaxLen
            )
        );

        this.email = new FormField(
            Validators.checkRequired('Email is required'),
            Validators.checkEmailFormat('Incorrect email. Example: bob@test.com')
        );

        this.password = new FormField(
            Validators.checkRequired('Password is required'),
            Validators.isPasswordComplexEnough(
                'Password must have At least 8 characters long. - At least 1 uppercase, AND at least 1 lowercase - At least 1 digit OR at least 1 alphanumeric'
            )
        );

        this.confirmPassword = new FormField(
            Validators.checkRequired('Confirm Password is required'),
            Validators.doesFieldEqualTo(this.password, 'Confirm Password does not match with Password')
        );
    }
}