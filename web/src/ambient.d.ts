// Auth module
type LoginDTO = {
	email: string;
	password: string;
};

type LoginSuccess = {
	displayName: string;
	accessToken: string;
};

type UserInfo = {
	displayName: string;
};

type RegisterDTO = {
	fullName: string;
	displayName: string;
	email: string;
	password: string;
	confirmPassword: string;
};

type ToastType = 'DANGER' | 'INFO' | 'SUCCESS' | 'WARNING';

type Validator = { check: (value: string) => boolean; errorMessage: string };

type Product = {
	price: number;
	pictureUrl: string;
	name: string;
	description: string;
	id: number;
	productType: string;
	productBrand: string;
};

type Page<T> = {
	pageNumber: number;
	pageSize: number;
	totalItems: number;
	data: T[];
};

type ShopParams = {
	pageNumber: number;
	pageSize: number;
	sort: string;
	search: string | undefined;
	brandId: number;
	typeId: number;
};

type ProductBrand = {
	id: number;
	name: string;
};

type ProductType = {
	id: number;
	name: string;
};

type Basket = {
	id: string;
	items: BasketItem[];
};

type BasketItem = {
	id: number;
	productName: string;
	price: number;
	quantity: number;
	pictureUrl: string;
	brand: string;
	type: string;
};

type BasketTotals = {
	shipping: number;
	subtotal: number;
	total: number;
};

type CreateOrder = {
	basketId: string;
	deliveryMethodId: number;
	billingAddress: OrderAddress;
	shippingAddress: OrderAddress | null;
};

type OrderAddress = {
	fullName: string;
	email: string;
	address: string;
	address2: string | null;
	country: string;
	city: string;
	state: string;
	zipCode: string | null;
};

type DeliveryMethod = {
	shortName: string;
	deliveryTime: string;
	price: number;
	id: number;
};

type UserProfile = {
	userId: number;
	email: string;
	fullName: string;
};
