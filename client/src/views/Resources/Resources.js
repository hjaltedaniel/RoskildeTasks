import ResourceTabs from '../../components/ResourceTabs/index.vue';

export default {
  name: 'resources',
  components: {
    ResourceTabs,
  },
  props: [],
  data () {
    return {
      iconColor: 'green',
      isRead: false,
      categories: [
        {
          id: 1,
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
          id: 2,
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
          id: 3,
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

  },
  mounted () {
    console.log(this.$route.query);
  },
  methods: {
    
  }
}


