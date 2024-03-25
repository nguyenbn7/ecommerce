import ReactiveFormBase from '$lib/core/form/reactive';
import ReactiveFormField from '$lib/core/form/reactive.field';
import { checkRequired } from '$lib/core/form/validator';
import OrderAddressForm from './order.address.form';

export default class OrderForm extends ReactiveFormBase {
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
