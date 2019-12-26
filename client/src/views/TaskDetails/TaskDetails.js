import UploadArea from './../../components/UploadArea'
import EditorTable from './../../components/EditorTable'
import TasksService from '../../services/TasksService'

export default {
	name: 'task-details',
	components: {
		UploadArea,
		EditorTable
	},
	computed: {
		task() {
			return this.$store.state.tasksList[this.$route.params.id]
		},
	},
	methods: {
		submitRows(data) {
			const answer = {
				"TaskId": this.task.id,
				"Content": data
			}
			TasksService.submitRows(answer);
		},
		submitFile(data) {
			TasksService.submitFile(this.task.id, data);
		}
	}
}