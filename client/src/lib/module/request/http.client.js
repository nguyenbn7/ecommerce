import { PUBLIC_BASE_API_URL } from "$env/static/public";
import axios from "axios";

export function createHttpClient() {
    return axios.create({
        baseURL: PUBLIC_BASE_API_URL,
        timeout: 5000
    });
}