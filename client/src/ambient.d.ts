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
