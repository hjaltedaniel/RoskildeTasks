import ApiService from "../services/ApiService"

class CategoriesService {
	getAllCategories = () => {
		return ApiService.get("category/getallcategories");
	}
}

export default new CategoriesService()
