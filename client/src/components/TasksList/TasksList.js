export default {
	name: 'tasks-list',
	components: {},
	computed: {
		tasksList() {
			return this.$store.state.tasksList
		},
	},
	mounted() {
		this.$store.dispatch("getTaskList")
	},
	methods: {
		getTimeToDeadline(deadline) {
			const timeUntilDeadline = deadline - new Date();
			const day = 86400000;
			const hour = 3600000;
			const minute = 60000;

			if (timeUntilDeadline / day > 1) {
				return "+" + Math.ceil(timeUntilDeadline / day * 10) / 10 + " days";
			}
			else if (timeUntilDeadline / hour >= 1) {
				return "+" + Math.ceil(timeUntilDeadline / hour * 10) / 10 + " hours";
			}
			else if (timeUntilDeadline / hour < 1 && timeUntilDeadline / minute > 1) {
				return "+" + Math.ceil(timeUntilDeadline / minute * 10) / 10 + " minutes";
			}

			return "Overdue"
		}
	}
}


