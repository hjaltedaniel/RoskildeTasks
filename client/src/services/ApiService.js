import axios from "axios"

const apiClient = axios.create({
	baseURL: `https://roskildetasks.azurewebsites.net/umbraco/api`,
	withCredentials: false,
	headers: {
		Accept: "*/*",
		"Content-Type": "application/json"
	}
})

export default apiClient;
