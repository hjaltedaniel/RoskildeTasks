import ApiService from "../services/ApiService"

class MessageService {
	getMessagesForCategory = (id) => {
		return ApiService.get("message/getmessagesforcategory?categoryid=" + id);
	}
	setMessageForCategory = (id, content) => {

		let data = {
			"CategoryId": id,
			"Content": content
		}

		return ApiService.post("message/submitmessageforcategory", data)
	}
}

export default new MessageService()
