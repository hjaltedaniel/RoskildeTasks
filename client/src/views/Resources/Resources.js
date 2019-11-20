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
      categories: [
        {
          title: 'Signs and Layout',
          info: {
            name: 'Babel Burger Bod',
            type: 'pdf',
            filePath: ''
          }
        },
        {
          title: 'Construction',
          info: {
            name: 'Bob Construction pillar',
            type: 'pdf',
            filePath: ''
          }
        },
        {
          title: 'Supply',
          info: {
            name: 'Monster Energy for volunteers',
            type: 'pdf',
            filePath: ''
          }
        }
      ]
    }
  },
  computed: {

  },
  mounted () {

  },
  methods: {

  }
}


