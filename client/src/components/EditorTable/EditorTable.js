export default {
	name: 'editor-table',
	props: {
		columns: {
			required: true,
			type: Object
		},
		data: {
			default: () => [],
			type: Array
		}
	},
	data() {
		return {
			rows: [...this.data],
			showModal: false,
		}
	},
	methods: {
		toggleModal() {
			this.showModal = !this.showModal
		},
		addRow() {
			this.rows.push({
				machine: "",
				usage: "",
				force: "",
				power: "",
			})
		},
		deleteRow(rowIndex) {
			let newRows = [...this.rows];
			newRows.splice(rowIndex, 1);
			this.rows = newRows;
		},
		inputChange(rowIndex, column, value) {
			this.rows[rowIndex][column] = value;
		},
		saveRows() {
			this.$store.dispatch("showModal", () => {
				alert("Saving");
			});
		},
		clearRows() {
			this.$store.dispatch("showModal", () => {
				this.rows = [];
			});
		}
	}
}


