import { FormField, FormGroup, Validators } from "$lib/shared/form";

export class AddressFormGroup extends FormGroup {
    constructor() {
        super();
        this.fullName = new FormField(Validators.checkRequired('Full name is required'));
        this.email = new FormField(
            Validators.checkRequired('Email is required'),
            Validators.checkEmailFormat('Incorrect email. Example: bob@test.com')
        );
        this.address = new FormField(Validators.checkRequired('Address is required'));
        this.address2 = new FormField(Validators.isOptional());
    }
}

export class OrderFormGroup extends FormGroup {
    constructor() {
        super();
        this.billingAddress = new AddressFormGroup();
        this.shippingAddress = null;
    }
}