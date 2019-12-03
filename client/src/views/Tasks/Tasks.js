import TasksList from '../../components/TasksList/index.vue';
import TasksService from '../../services/TasksService';

export default {
  name: 'tasks',
  components: {
	TasksList
  },
  provide () {
	  return {
		  'tasksService': TasksService
	  }
  }
}


