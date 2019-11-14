import ResourcesList from '../../components/ResourcesList/index.vue';

export default {
  name: 'resources',
  components: {
    ResourcesList
  },
  props: [],
  data () {
    return {
      iconColor: 'green',
      categories: [
        {title: 'Signs and Layout'},
        {title: 'Construction'},
        {title: 'Supply'}
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


