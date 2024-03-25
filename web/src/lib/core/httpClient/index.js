import { PUBLIC_BASE_API_URL } from '$env/static/public';
import axios from 'axios';

export function createHttpClient(prefix = '', timeoutMilliSecs = 5000) {
	let baseURL = PUBLIC_BASE_API_URL.endsWith('/')
		? PUBLIC_BASE_API_URL.slice(PUBLIC_BASE_API_URL.length - 1)
		: PUBLIC_BASE_API_URL;

	if (prefix) {
		if (prefix[0] === '/')
			return axios.create({
				baseURL: `${baseURL}${prefix}`
			});
		return axios.create({
			baseURL: `${baseURL}/${prefix}`
		});
	}
	return axios.create({
		baseURL,
		timeout: timeoutMilliSecs
	});
}
