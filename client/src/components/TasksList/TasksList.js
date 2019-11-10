import TasksService from './../../services/TasksService';
export default {
  name: 'tasks-list',
  components: {},
  props: [],
  data () {
    return {
		tasksList: []
    }
  },
  mounted () {
	TasksService.getAllTasks()
		.then((response) => {
			response.forEach(task => {
				task.timeToDeadline = this.getTimeToDeadline(task.deadline);
			});
			this.tasksList = response;
		});
  },
  methods: {
	getTimeToDeadline(deadline) {
		let timeUntilDeadline = deadline - new Date();

		if (timeUntilDeadline <= 0) {	
			return "Overdue"
		}

		let days = timeUntilDeadline / 86400000;
		if (days >= 1) {
			return "+" + Math.ceil(days * 10) / 10 + " days";			 
		}
		else if (days < 1) {
			return "+" + Math.ceil(days * 24 * 10) / 10 + " hours";
		}
		else if (days / 24 < 1) {
			return "+" + Math.ceil(days * 24 * 60 * 10) / 10 + " minutes";
		}
	}
  }
}


