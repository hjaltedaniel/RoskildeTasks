import Cookies from 'js-cookie';
import axios from 'axios';
import commonConfig from '../config/common-service.config'

class RessourcesService {
	constructor() {
		this.httpClient = axios.create(commonConfig)
	}

	getAllRessources = () => {
		this.httpClient.defaults.headers.common['Authorization'] = "Basic " + Cookies.get("Token");
		return this.httpClient.get("ressource/getallressources");
	}
}

export default new RessourcesService()
