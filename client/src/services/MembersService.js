import ApiService from "../services/ApiService"

class MembersService {

	login = (uname, pass) => {

		return ApiService.get("/member/init", {
			auth: {
				username: uname,
				password: pass
			}
		});
	}

	validate = (token) => {
		let auth = "Basic " + token;
		ApiService.defaults.headers.common['Authorization'] = auth;

		return ApiService.get("/member/init");
	}

	changePassword = (OldPassword, NewPassword) => {

		let data = {
			"OldPassword": OldPassword,
			"NewPassword": NewPassword
		}

		return ApiService.post("member/changepassword", data)
	}

	changeEmail = (email) => {

		let data = {
			"NewEmail": email
		}

		return ApiService.post("member/changeemail", data)
	}
}

export default new MembersService()
