import ResourceTabs from '../../components/ResourceTabs/index.vue';

export default {
	name: 'resources',
	components: {
		ResourceTabs,
	},
	props: [],
	data() {
		return {
			categoriesList: [],
		}
	},
	computed: {
		storeCategories() {
			return this.$store.state.categoriesList;
		},
	},
	watch: {
		storeCategories() {
			this.setCategories();
		}
	},
	mounted() {
		if (this.$store.state.categoriesList.length === 0) {
			this.$store.dispatch("getCategoryList");
		}
		if(this.$store.state.ressourcesList.length === 0) {
			this.$store.dispatch("getRessourceList");
		}
		this.setCategories();
	},
	methods: {
		setCategories() {
			let categories = this.storeCategories;
			let catList = [];

			if (categories != undefined) {
				categories.forEach(function (item, index) {
					if (item.isOnlyMessages == false) {
						catList.push(item);
					}
				})
				if (this.$route.params.resourcelist == undefined) {
					this.$router.push({
						path: `/resources/${catList[0].Id}`
					})
				}
				this.categoriesList = catList;
			}
		},
	}
}
