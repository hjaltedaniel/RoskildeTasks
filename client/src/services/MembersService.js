import Cookies from 'js-cookie';
import axios from 'axios';
import commonConfig from '../config/common-service.config';

class MembersService {
	constructor() {
		this.httpClient = axios.create(commonConfig);
	}

	login = (uname, pass) => {
		return this.httpClient.get("/member/init", {
			auth: {
				username: uname,
				password: pass
			}
		});
	}

	validate = () => {
		this.httpClient.defaults.headers.common['Authorization'] = "Basic " + Cookies.get("Token");
		return this.httpClient.get("/member/init");
	}

	changePassword = (OldPassword, NewPassword) => {
		this.httpClient.defaults.headers.common['Authorization'] = "Basic " + Cookies.get("Token");
		let data = {
			"OldPassword": OldPassword,
			"NewPassword": NewPassword
		}
		return this.httpClient.post("member/changepassword", data)
	}

	changeEmail = (email) => {
		this.httpClient.defaults.headers.common['Authorization'] = "Basic " + Cookies.get("Token");
		let data = {
			"NewEmail": email
		}
		return this.httpClient.post("member/changeemail", data)
	}
}

export default new MembersService()
