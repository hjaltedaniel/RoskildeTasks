export default {
	name: 'editor-table',
	props: {
		columns: {
			required: true,
			type: Object
		},
		onSave: {
			default: () => {}
		}
	},
	data() {
		return {
			rows: []
		}
	},
	methods: {
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
				this.onSave(this.rows);
			});
		},
		clearRows() {
			this.$store.dispatch("showModal", () => {
				this.rows = [];
			});
		}
	}
}


