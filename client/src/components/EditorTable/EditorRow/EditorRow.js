import EditorCell from './EditorCell';

export default {
	name: 'editor-row',
	components: {
		EditorCell
	},
	props: {
		row: {
			type: Object,
			required: true
		},
		onRowEdit: {
			type: Function,
			required: true
		},
		onRowDelete: {
			type: Function,
			required: true
		},
		initialIsEditable: {
			type: Boolean,
			default: false
		}
	},
	data() {
		return {
			isEditable: this.initialIsEditable,
			tempRow: {}
		}
	},
	methods: {
		enableEditing() {
			this.isEditable = true;
			this.tempRow = {...this.row}
		},
		deleteRow() {
			this.onRowDelete()
		},
		commitEdit() {
			this.onRowEdit(this.tempRow);
			this.isEditable = false;
			this.tempRow = {}
		},
		discardEdit() {
			this.isEditable = false;
			this.tempRow = {}
		},
		onInputChange(key, value) {
			this.tempRow[key] = value;
		}
	}
}
