
export default {
	name: 'editor-cell',
	components: {},
	props: {
		cell: {
			required: true
		},
		isEditable: {
			type: Boolean,
			required: true
		},
		onInputChange: {
			type: Function,
			default: () => {}
		},
		columnName: {
			type: String,
			required: true
		}
	}
}
