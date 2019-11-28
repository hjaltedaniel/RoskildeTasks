import Vue from "vue";
import Vuex from "vuex";
import Cookies from 'js-cookie';
import ApiService from "./services/ApiService";
import TasksService from "./services/TasksService";
import CategoriesService from "./services/CategoriesService";
import RessourcesService from "./services/RessourcesService";

Vue.use(Vuex);

export default new Vuex.Store({
	state: {
		token: undefined,
		logoutMessage: "",
		tasksList: [],
		categoriesList: [],
		ressourcesList: []
	},
	getters: {
		filterRessourcesByCategory: (state) => (category) => {
			return state.ressourcesList.filter(ressource => ressource.Category.Id === category)
		},
		getTask: (state) => (id) => {
			return state.tasksList.find(task => task.Id === id)
		}
	},
	mutations: {
		SET_TOKEN(state, payload) {
			state.token = payload
		},
		DELETE_TOKEN(state, payload) {
			state.token = undefined;
			state.logoutMessage = payload;
		},
		SET_TASKS_LIST(state, payload) {
			state.tasksList = payload;
		},
		SET_CATEGORIES_LIST(state, payload) {
			state.categoriesList = payload;
		},
		SET_RESSOURCES_LIST(state, payload) {
			state.ressourcesList = payload;
		}
	},
	actions: {
		setAuthorization(context, token) {
			context.commit("SET_TOKEN", token)
			Cookies.set('Token', token, { expires: 1 })
		},
		logout(context, message) {
			context.commit("DELETE_TOKEN", message)
			Cookies.remove('Token')
		},
		getTaskList(context) {
			let auth = "Basic " + this.state.token;
			ApiService.defaults.headers.common['Authorization'] = auth;
			//loading...
			context.commit("SET_TASKS_LIST");
			TasksService.getAllTasks()
			.then((response) => {
				context.commit("SET_TASKS_LIST", response.data);
			})
			.catch((error) => {
				context.commit("SET_TASKS_LIST", error);
			});
		},
		getCategoryList(context) {
			let auth = "Basic " + this.state.token;
			ApiService.defaults.headers.common['Authorization'] = auth;
			//loading...
			context.commit("SET_TASKS_LIST");
			CategoriesService.getAllCategories()
				.then((response) => {
					context.commit("SET_CATEGORIES_LIST", response.data);
				})
				.catch((error) => {
					context.commit("SET_CATEGORIES_LIST", error);
				});
		},
		getRessourceList(context) {
			let auth = "Basic " + this.state.token;
			ApiService.defaults.headers.common['Authorization'] = auth;
			//loading...
			context.commit("SET_RESSOURCES_LIST");
			RessourcesService.getAllRessources()
				.then((response) => {
					context.commit("SET_RESSOURCES_LIST", response.data);
				})
				.catch((error) => {
					context.commit("SET_RESSOURCES_LIST", error);
				});
		}
	}
});
