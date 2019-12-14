import Vue from "vue";
import Vuex from "vuex";
import Cookies from 'js-cookie';
import TasksService from "./services/TasksService";
import CategoriesService from "./services/CategoriesService";
import RessourcesService from "./services/RessourcesService";

Vue.use(Vuex);

export default new Vuex.Store({
	state: {
		user: null,
		logoutMessage: "",
		tasksList: [],
		categoriesList: [],
		ressourcesList: [],
		showModal: false,
		modalOnCancel: () => { },
		modalOnContinue: () => { }
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
		SET_USER(state, payload) {
			state.user = payload
		},
		SET_NEW_MAIL(state, payload) {
			state.user.Email = payload
		},
		LOGOUT(state, payload) {
			state.logoutMessage = payload;
			state.user = null;
			state.tasksList = []
			state.ressourcesList = []
			state.categoriesList = []
			Cookies.remove('Token');
		},
		SET_TASKS_LIST(state, payload) {
			state.tasksList = payload;
		},
		SET_CATEGORIES_LIST(state, payload) {
			state.categoriesList = payload;
		},
		SET_RESSOURCES_LIST(state, payload) {
			state.ressourcesList = payload;
		},
		SHOW_MODAL(state, payload) {
			state.showModal = true;
			state.modalOnContinue = payload;
		},
		CLOSE_MODAL(state) {
			state.showModal = false;
		}
	},
	actions: {
		showModal(context, callback) {
			context.commit("SHOW_MODAL", callback);
		},
		closeModal(context) {
			context.commit("CLOSE_MODAL");
		},
		setAuthorizationSession(context, token) {
			Cookies.set('Token', token)
		},
		setAuthorization24h(context, token) {
			Cookies.set('Token', token, {
				expires: 1
			})
		},
		setUser(context, user) {
			context.commit("SET_USER", user);
		},
		setNewEmail(context, email) {
			context.commit("SET_NEW_MAIL", email);
		},
		logout(context, message) {
			context.commit("LOGOUT", message)
		},
		getTaskList(context) {
			TasksService.getAllTasks()
				.then((response) => {
					context.commit("SET_TASKS_LIST", response.data);
				})
				.catch((error) => {
					context.commit("SET_TASKS_LIST", []);
				});
		},
		getCategoryList(context) {
			CategoriesService.getAllCategories()
				.then((response) => {
					context.commit("SET_CATEGORIES_LIST", response.data);
				})
				.catch((error) => {
					context.commit("SET_CATEGORIES_LIST", []);
				});
		},
		getRessourceList(context) {
			RessourcesService.getAllRessources()
				.then((response) => {
					context.commit("SET_RESSOURCES_LIST", response.data);
				})
				.catch((error) => {
					context.commit("SET_RESSOURCES_LIST", []);
				});
		}
	}
});
