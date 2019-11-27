import Vue from "vue";
import Vuex from "vuex";
import ApiService from "./services/ApiService";
import TasksService from "./services/TasksService";
import CategoriesService from "./services/CategoriesService";

Vue.use(Vuex);

export default new Vuex.Store({
	state: {
		token: undefined,
		tasksList: [],
		categoriesList: []
	},
	mutations: {
		SET_TOKEN(state, payload) {
			state.token = payload
		},
		SET_TASKS_LIST(state, payload) {
			state.tasksList = payload;
		},
		SET_CATEGORIES_LIST(state, payload) {
			state.categoriesList = payload;
		}
	},
	actions: {
		setAuthorization(context, token) {
			context.commit("SET_TOKEN", token)
		},
		getTaskList(context) {
			ApiService.defaults.headers.common['Authorization'] = this.state.token;
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
			ApiService.defaults.headers.common['Authorization'] = this.state.token;
			//loading...
			context.commit("SET_TASKS_LIST");
			CategoriesService.getAllCategories()
				.then((response) => {
					context.commit("SET_CATEGORIES_LIST", response.data);
				})
				.catch((error) => {
					context.commit("SET_CATEGORIES_LIST", error);
				});
		}
	}
});
