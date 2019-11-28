import ApiService from "../services/ApiService"

class RessourcesService {
	getAllRessources = () => {
		return ApiService.get("ressource/getallressources");
	}
}

export default new RessourcesService()
