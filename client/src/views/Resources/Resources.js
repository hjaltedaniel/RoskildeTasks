import ResourcesList from '../../components/ResourcesList/index.vue';
import Tabs from '../../components/ResourceTabs/index.vue';

export default {
  name: 'resources',
  components: {
    ResourcesList,
    Tabs,
  },
  props: [],
  data () {
    return {
      iconColor: 'green',
      isRead: false,
      categories: [
        {
          title: 'Signs and Layout',
          info: [
            {
              name: 'Babel Burger Bod',
              type: 'pdf',
              filePath: ''
            },
            {
              name: 'Signs for stalls',
              type: 'pdf',
              filePath: ''
            },
            {
              name: 'test',
              type: 'test type',
              filePath: 'test path'
            }
          ],
        },
        {
          title: 'Construction',
          info: [
            {
              name: 'Cement Pillar',
              type: 'Image',
              filePath: ''
            },
            {
              name: 'Wooden planks',
              type: 'pdf',
              filePath: ''
            }
          ]
        },
        {
          title: 'Supply',
          info: [
            {
              name: 'Monster Energy for volunteers',
              type: 'pdf',
              filePath: ''
            },
            {
              name: 'Beer for all foodstalls',
              type: 'pdf',
              filePath: ''
            },
          ]
        },
      ]
    }
  },
	computed: {
		categoriesList() {
			return this.$store.state.categoriesList;
		},
	  ressourcesList() {
		  return this.$store.getters.filterRessourcesByCategory(1075);
	  },
  },
  mounted () {

  },
  methods: {

  }
}


