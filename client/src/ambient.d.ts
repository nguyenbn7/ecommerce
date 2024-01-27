type ToastType = 'DANGER' | 'INFO' | 'SUCCESS' | 'WARNING';

type Validator = { check: (value: string) => boolean; errorMessage: string };

declare namespace Account {
	type UserLoginForm = {
		email: string;
		password: string;
	};

	type UserRegisterForm = {
		email: string;
		firstName: string;
		lastName: string;
		password: string;
		confirmPassword: string;
	};

	type LoginSuccessResponse = {
		email: string;
		firstName: string;
		lastName: string;
		accessToken: string;
	};

	type UserDisplayInfoResponse = {
		email: string;
		firstName: string;
		lastName: string;
	};

	type UserInfo = {
		email: string;
		displayName: string;
	};
}

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
