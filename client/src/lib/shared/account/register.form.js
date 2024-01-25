import { FormField, FormGroup } from "../form";
import { Validators } from "../form/validation-feedback.svelte";

export class RegisterForm extends FormGroup {
    /**
     * @param {number | undefined} displayNameMaxLen
     */
    constructor(displayNameMaxLen) {
        super();
        this.displayName = new FormField(
            Validators.checkRequired('Name is required'),
            Validators.containsAlnumAndSpace('Name contains only letters and spaces'),
            Validators.checkMaxLength(
                `Name's max length is ${displayNameMaxLen} characters`,
                displayNameMaxLen
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