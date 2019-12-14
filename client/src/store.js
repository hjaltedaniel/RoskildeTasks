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
		user: {
			name: "",
			company: "",
			email: "",
			status: "",
			accessGroups: []
		},
		token: undefined,
		logoutMessage: "",
		tasksList: [],
		categoriesList: [],
		ressourcesList: [],
		showModal: false,
		modalOnCancel: () => {},
		modalOnContinue: () => {}
	},
	getters: {
		filterRessourcesByCategory: (state) => (category) => {
			return state.ressourcesList.filter(ressource => ressource.Category.Id ===
				category)
		},
		getTask: (state) => (id) => {
			return state.tasksList.find(task => task.Id === id)
		}
	},
	mutations: {
		SET_TOKEN(state, payload) {
			state.token = payload
		},
		SET_USER(state, payload) {
			state.user = payload
		},
		SET_NEW_MAIL(state, payload) {
			state.user.Email = payload
		},
		DELETE_TOKEN(state, payload) {
			state.token = undefined;
			state.logoutMessage = payload;
			Cookies.remove('Token')
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
			context.commit("SET_TOKEN", token)
			Cookies.set('Token', token)
		},
		setAuthorizationState(context, token) {
			context.commit("SET_TOKEN", token)
		},
		setAuthorization24h(context, token) {
			context.commit("SET_TOKEN", token)
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
			context.commit("DELETE_TOKEN", message)
			Cookies.remove('Token')
		},
		getTaskList(context) {
			// let auth = "Basic " + this.state.token;
			// ApiService.defaults.headers.common['Authorization'] = auth;
			// //loading...
			// context.commit("SET_TASKS_LIST");
			// TasksService.getAllTasks()
			// 	.then((response) => {
			// 		context.commit("SET_TASKS_LIST", response.data);
			// 	})
			// 	.catch((error) => {
			// 		context.commit("SET_TASKS_LIST", error);
			// 	});
			context.commit("SET_TASKS_LIST", [
				{
					id: 1,
					name: 'Test Task',
					deadline: new Date(new Date().getTime() + 86400000 * 3.7),
					category: {
						id: 1,
						name: "Supply",
						color: "#C82333",
					},
					editor: {
						type: "file",
						columns: null
					},
					description: `<p>We want to be sure that we can provide sufficient power supply for you and your kitchen. 
					We need to know how much power your machinery will require. Please provide the information in the table bellow</p>`
				},
				{
					id: 2,
					name: 'Provide Power Usage',
					deadline: new Date(new Date().getTime() + 86400000 * 3.7),
					category: {
						id: 1,
						name: "Supply",
						color: "#C82333",
					},
					editor: {
						type: "list",
						columns: {
							machine: {
								displayName: "Machine",
								type: "text"
							},
							usage: {
								displayName: "Usage",
								type: "text"
							},
							force: {
								displayName: "Force (in Volt)",
								type: "number"
							},
							power: {
								displayName: "Power (in Watt)",
								type: "number"
							}
						}
					},
					description: `<p>We want to be sure that we can provide sufficient power supply for you and your kitchen. 
					We need to know how much power your machinery will require. Please provide the information in the table bellow</p>`
				},
			])
		},
		getCategoryList(context) {
			let auth = "Basic " + this.state.token;
			ApiService.defaults.headers.common['Authorization'] = auth;
			//loading...
			context.commit("SET_CATEGORIES_LIST");
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
