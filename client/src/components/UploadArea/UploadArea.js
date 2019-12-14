export default {
	name: "upload-area",
	props: {
		onSubmit: {
			required: true,
			type: Function
		}
	},
	data() {
		return {
			fileName: ''
		};
	},
	methods: {
		onFileInputChange(e) {
			this.fileName = e.target.files[0].name;
		},
		onClear(e) {
			this.fileName = '';
			this.$refs.uploadForm.reset();
		},
		uploadFile(e) {
			let formData = new FormData();
			formData.append('file', this.$refs['upload-control'].files[0]);
			
			this.onSubmit(formData);
		}
	}
};
