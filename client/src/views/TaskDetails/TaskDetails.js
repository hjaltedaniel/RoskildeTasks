import UploadArea from './../../components/UploadArea'
import EditorTable from './../../components/EditorTable'

export default {
  name: 'task-details',
  components: {
	  UploadArea,
	  EditorTable
  },
  computed: {
	  task() {
		  return this.$store.state.tasksList[this.$route.params.id]
	  }
  },
}