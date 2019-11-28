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
}

export default new MembersService()
