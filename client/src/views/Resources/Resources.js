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
			isMobile: false
		}
	},
	computed: {
		storeCategories() {
			return this.$store.state.categoriesList;
		},
		isMobileOverlayActive() {
			if (this.$route.params.resourcelist != undefined && this.isMobile) {
				return true;
			} else {
				return false;
			}
		}
	},
	watch: {
		storeCategories: function () {
			this.setCategories();
		}
	},
	mounted() {
		this.setCategories();
	},
	created() {
		window.addEventListener('resize', this.handleResize)
		this.handleResize();
	},
	destroyed() {
		window.removeEventListener('resize', this.handleResize)
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
				if (this.$route.params.resourcelist == undefined && this.isMobile == false) {
					this.$router.push({
						path: `/resources/${catList[0].Id}`
					})
				}
				this.categoriesList = catList;
			}
		},
		handleResize() {
			if (window.innerWidth < 768) {
				this.isMobile = true;
			} else if (window.innerWidth > 768) {
				this.isMobile = false;
			}
		},
		closeOverlay() {
			this.$router.push({
				path: `/resources`
			})
		}
	}
}
