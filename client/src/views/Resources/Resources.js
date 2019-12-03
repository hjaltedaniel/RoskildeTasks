import ResourceTabs from '../../components/ResourceTabs/index.vue';

export default {
  name: 'resources',
  components: {
    ResourceTabs,
  },
  props: [],
  data () {
    return {
      
    }
  },
	computed: {
		categoriesList() {
			return this.$store.state.categoriesList;
		},
	  ressourcesList() {
		  return this.$store.getters.filterRessourcesByCategory(this.categoriesList);
	  },
  },
  mounted () {

  },
  methods: {
    
  }
}


