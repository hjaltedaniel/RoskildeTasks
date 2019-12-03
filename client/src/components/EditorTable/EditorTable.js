import EditorRow from './EditorRow'

export default {
	name: 'editor-table',
	components: {
		EditorRow
	},
	props: [],
	data() {
		return {
			columns: [
				"Machine",
				"Usage",
				"Force (in Volt)",
				"Power (in Watt)",
			],
			rows: [
				[
					"Deep fryer",
					"Deep Frying...",
					230,
					1800,
				], [
					"Deep fryer",
					"Deep Frying...",
					230,
					1800,
				], [
					"Deep fryer",
					"Deep Frying...",
					230,
					1800,
				], [
					"Deep fryer",
					"Deep Frying...",
					230,
					1800,
				], [
					"Deep fryer",
					"Deep Frying...",
					230,
					1800,
				], [
					"Deep fryer",
					"Deep Frying...",
					230,
					1800,
				]
			]
		}
	},
	computed: {

	},
	mounted() {

	},
	methods: {
		onRowEdit() {
			console.log('edit')
		},
		onRowDelete() {
			console.log('Delete')
		}
	}
}


