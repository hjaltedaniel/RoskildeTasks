import axios from "axios"

const apiClient = axios.create({
	baseURL: `https://localhost:44383/umbraco/api`,
	withCredentials: false,
	headers: {
		Accept: "*/*",
		"Content-Type": "application/json"
	}
})

export default apiClient;
