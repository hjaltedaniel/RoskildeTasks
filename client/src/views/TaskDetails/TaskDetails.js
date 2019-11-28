
export default {
  name: 'task-details',
  data() {
    return {
		filesLength: 0
    }
  },
  methods: {
	onFileInputChange(e) {
		this.filesLength = e.target.files.length;
	},
	onClear(e) {
		this.filesLength = 0;
		this.$refs.uploadForm.reset()
	},
	onSubmit(e) {
		console.log("Upload submit");
	}
  }
}


