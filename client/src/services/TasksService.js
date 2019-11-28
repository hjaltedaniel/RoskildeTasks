import ApiService from "../services/ApiService"

class TasksService {
	getAllTasks = () => {
		return ApiService.get("task/getalltasks");
	}
}

export default new TasksService()
