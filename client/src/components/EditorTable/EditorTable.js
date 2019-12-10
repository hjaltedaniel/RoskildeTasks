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
			console.log("save");
		},
		clearRows() {
			this.rows = [];
		}
	}
}


