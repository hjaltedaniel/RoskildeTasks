import axios from "axios"

const apiClient = axios.create({
	baseURL: `https://localhost:44383/umbraco/api`,
	withCredentials: false,
	auth: {
		username: 'mikuna',
		password: '1234567890'
	},
	headers: {
		Accept: "application/json",
		"Content-Type": "application/json"
	}
})

export default apiClient;
