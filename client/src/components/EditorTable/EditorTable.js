import EditorRow from './EditorRow'

export default {
	name: 'editor-table',
	components: {
		EditorRow
	},
	data() {
		return {
			columns: {
				machine: {
					displayName: "Machine",
					type: "text"
				},
				usage: {
					displayName: "Usage",
					type: "text"
				},
				force: {
					displayName: "Force (in Volt)",
					type: "number"
				},
				power: {
					displayName: "Power (in Watt)",
					type: "number"
				}
			},
			rows: [
				{
					machine: "Deep fryer",
					usage: "Deep Frying...",
					force: 230,
					power: 1800,
				},
				{
					machine: "Stove",
					usage: "Stoving",
					force: 230,
					power: 1800,
				},
				{
					machine: "Deep fryer",
					usage: "Deep Frying...",
					force: 230,
					power: 1800,
				},
				{
					machine: "Deep fryer",
					usage: "Deep Frying...",
					force: 230,
					power: 1800,
				},
				{
					machine: "Deep fryer",
					usage: "Deep Frying...",
					force: 230,
					power: 1800,
				},
				{
					machine: "Deep fryer",
					usage: "Deep Frying...",
					force: 230,
					power: 1800,
				}
			],
		}
	},
	methods: {
		onRowEdit(rowIndex, newRow) {
			console.log(newRow);
			let newRows = [...this.rows];
			newRows[rowIndex] = newRow;
			this.rows = newRows;
		},
		onRowDelete() {
			console.log("Delete");
		},
		addRow() {
			console.log("add row");
		},
		discardNewRow() {
			console.log("discard new row");
		}
	}
}


