import Cookies from 'js-cookie';
import axios from 'axios';
import commonConfig from '../config/common-service.config';

class MessageService {
	constructor() {
		this.httpClient = axios.create(commonConfig)
	}

	getMessagesForCategory = (id) => {
		this.httpClient.defaults.headers.common['Authorization'] = "Basic " + Cookies.get("Token");
		return this.httpClient.get("message/getmessagesforcategory?categoryid=" + id);
	}
	setMessageForCategory = (id, content) => {
		this.httpClient.defaults.headers.common['Authorization'] = "Basic " + Cookies.get("Token");
		let data = {
			"CategoryId": id,
			"Content": content
		}
		return this.httpClient.post("message/submitmessageforcategory", data)
	}
}

export default new MessageService()
