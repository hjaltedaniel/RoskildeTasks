import Cookies from 'js-cookie';
import axios from 'axios';
import commonConfig from '../config/common-service.config';

class CategoriesService {
	constructor() {
		this.httpClient = axios.create(commonConfig)
	}

	getAllCategories = () => {
		this.httpClient.defaults.headers.common['Authorization'] = "Basic " + Cookies.get("Token");
		return this.httpClient.get("category/getallcategories");
	}
}

export default new CategoriesService()
