import Loader from "@/components/Loader"

export default {
	name: 'resource-file-list',
	components: {
		Loader
	},
	props: [],
	data() {
		return {
			ressourcesList: [],
			isLoading: true
		}
	},
	computed: {
		categoryId() {
			return Number.parseInt(this.$route.params.resourcelist);
		},
		storeRessources() {
			return this.$store.state.ressourcesList;
		}
	},
	watch: {
		categoryId: function () {
			this.ressourcesList = this.$store.getters.filterRessourcesByCategory(this
				.categoryId);
		},
		storeRessources: function () {
			if (this.storeRessources != undefined) {
				this.ressourcesList = this.$store.getters.filterRessourcesByCategory(this
					.categoryId);
				this.isLoading = false;
			}
		}
	},
	mounted() {
		if (this.storeRessources != undefined) {
			this.ressourcesList = this.$store.getters.filterRessourcesByCategory(this
				.categoryId)
			this.isLoading = false;
		}
	},
	methods: {}
}
