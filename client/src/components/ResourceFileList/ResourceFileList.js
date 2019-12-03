
export default {
  name: 'resource-file-list',
  components: {},
  props: [],
  data () {
    return {
      ressourcesList: []
    }
  },
  computed: {
    categoryId() {
      return Number.parseInt(this.$route.params.resourcelist);
    }
  },
  watch: {
    categoryId: function() {
      this.ressourcesList = this.$store.getters.filterRessourcesByCategory(this.categoryId);
    }
  },
  mounted () {
    this.ressourcesList = this.$store.getters.filterRessourcesByCategory(this.categoryId);
  },
  methods: {

  }
}


