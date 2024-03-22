/**
 * @param {string} errorMessage
 * @returns {Validator}
 */
function checkRequired(errorMessage = 'Field is required') {
	return {
		check: (value) => !!value,
		errorMessage
	};
}
/**
 * @param {string} errorMessage
 * @returns {Validator}
 */
function checkMaxLength(errorMessage = '', max_length = 256) {
	return {
		check: (value) => value.length <= max_length,
		errorMessage: errorMessage ? errorMessage : `Field should have maximum ${max_length} characters`
	};
}

/**
 * @param {string} errorMessage
 * @returns {Validator}
 */
function containsAlnumAndSpace(errorMessage = 'Field contains only numbers, letters and spaces') {
	return {
		check: (value) => /^[a-zA-Z\s\d]+$/.test(value),
		errorMessage
	};
}
/**
 * @param {string} errorMessage
 * @returns {Validator}
 */
function checkEmailFormat(errorMessage = 'Email has incorrect format') {
	return {
		check: (value) => /^.+@[^\.].*\.[a-z]{2,}$/.test(value),
		errorMessage
	};
}

/**
 * @param {import("./form.field").FormField} anotherField
 * @returns {Validator}
 */
function doesFieldEqualTo(anotherField, errorMessage = 'Fields does not equal') {
	return {
		check: (value) => anotherField.value === value,
		errorMessage
	};
}

/**
 * @returns {Validator}
 */
function isPasswordComplexEnough(
	errorMessage = 'String must at least 8 characters long. - at least 1 uppercase, AND at least 1 lowercase - At least 1 digit OR at least 1 alphanumeric'
) {
	return {
		check: (value) =>
			/(?=^.{8,}$)((?!.*\s)(?=.*[A-Z])(?=.*[a-z]))(?=(1)(?=.*\d)|.*[^A-Za-z0-9])^.*$/.test(value),
		errorMessage
	};
}

/**
 * @returns {Validator}
 */
function isOptional() {
	let errorMessage = '';
	return {
		check: (value) => true,
		errorMessage
	};
}

export const Validators = {
	checkRequired,
	checkMaxLength,
	containsAlnumAndSpace,
	checkEmailFormat,
	doesFieldEqualTo,
	isPasswordComplexEnough,
	isOptional
};
