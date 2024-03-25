import { createCustomerOrder } from './request';

/**
 * @param {import("../../component/checkout/form").OrderAddressForm} orderAddressForm
 */
function mapToOrderAddress(orderAddressForm) {
	return {
		fullName: orderAddressForm.fullName.value ?? '',
		email: orderAddressForm.email.value ?? '',
		address: orderAddressForm.address.value ?? '',
		address2: orderAddressForm.address2.value,
		country: orderAddressForm.country.value ?? '',
		state: orderAddressForm.state.value ?? '',
		city: orderAddressForm.city.value ?? '',
		zipCode: orderAddressForm.zipCode.value
	};
}

/**
 * @param {import("../../component/checkout/form").OrderForm} orderForm
 * @param {{ id: any; items?: BasketItem[]; }} basket
 */
export async function createOrder(orderForm, basket) {
	/**
	 * @type {CreateOrder}
	 */
	const order = {};

	order.basketId = basket?.id ?? '';
	order.billingAddress = mapToOrderAddress(orderForm.billingAddress);
	order.deliveryMethodId = Number(orderForm.deliveryMethodId.value);

	if (orderForm.shippingAddress) {
		order.shippingAddress = mapToOrderAddress(orderForm.shippingAddress);
	}

	return createCustomerOrder(order);
}
