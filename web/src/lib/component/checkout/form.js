import ReactiveFormBase from '$lib/core/form/reactive';
import ReactiveFormField from '$lib/core/form/reactive.field';
import { checkEmailFormat, checkRequired } from '$lib/core/form/validator';

export class OrderAddressForm extends ReactiveFormBase {
	constructor() {
		super();
		this.fullName = ReactiveFormField.createField(checkRequired('Full name is required'));
		this.email = ReactiveFormField.createField(
			checkRequired('Email is required'),
			checkEmailFormat('Incorrect email. Example: bob@test.com')
		);
		this.address = ReactiveFormField.createField(checkRequired('Address is required'));
		this.address2 = ReactiveFormField.createOptionalField();
		this.country = ReactiveFormField.createField(checkRequired());
		this.state = ReactiveFormField.createField(checkRequired());
		this.city = ReactiveFormField.createField(checkRequired());
		this.zipCode = ReactiveFormField.createOptionalField();
	}
}

export class OrderForm extends ReactiveFormBase {
	constructor() {
		super();
		this.billingAddress = new OrderAddressForm();
		/**
		 * @type {OrderAddressForm | null}
		 */
		this.shippingAddress = null;
		this.deliveryMethodId = ReactiveFormField.createField(
			checkRequired('Delivery method not selected')
		);
	}
}
