import EditorCell from './EditorCell';

export default {
	name: 'editor-row',
	components: {
		EditorCell
	},
	data() {
		return {
			isEditable: false
		}
	},
	props: {
		row: {
			type: Array,
			required: true
		},
		onRowEdit: {
			type: Function,
			required: true
		},
		onRowDelete: {
			type: Function,
			required: true
		}
	},
	methods: {
		enableEditing() {
			this.isEditable = true;
		},
		deleteRow() {
			// ...
			this.onRowDelete()
		},
		commitEdit() {
			this.isEditable = false;
			this.onRowEdit();
		},
		discardEdit() {
			this.isEditable = false;
			this.onRowDelete();
		}
	}
}


