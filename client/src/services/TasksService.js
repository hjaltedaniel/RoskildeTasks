import Cookies from 'js-cookie';
import axios from 'axios';
import commonConfig from '../config/common-service.config'

class TasksService {
	constructor() {
		this.httpClient = axios.create(commonConfig)
	}

	getAllTasks = () => {
		this.httpClient.defaults.headers.common['Authorization'] = "Basic " + Cookies.get("Token");
		return this.httpClient.get("tasks/");
	}
	submitRows = (data) => {
		this.httpClient.defaults.headers.common['Authorization'] = "Basic " + Cookies.get("Token");
		return this.httpClient.post(`answer/submitanswer`, data);
	}
	submitFile = (data) => {
		this.httpClient.defaults.headers.common['Authorization'] = "Basic " + Cookies.get("Token");
		return this.httpClient.post(`answer/submitanswer`, data);
	}
}

export default new TasksService()