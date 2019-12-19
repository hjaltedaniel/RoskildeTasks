export default {
	name: 'modal',
	computed: {
		showModal() {
			return this.$store.state.showModal
		},
		onCancel() { 
			return this.$store.state.modalOnCancel
		},
		onContinue() {
			return this.$store.state.modalOnContinue
		}
	},
	methods: {
		cancelAction() {
			this.$store.dispatch("closeModal")
			this.onCancel();
		},
		continueAction() {
			this.$store.dispatch("closeModal")
			this.onContinue();
		}
	}
}
