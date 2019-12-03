
export default {
  name: 'resource-file-list',
  components: {},
  props: {
    title: String,
    type: String,
    filePath: String
  },
  data () {
    return {

    }
  },
  computed: {

  },
  mounted () {
    console.log(this.$route.params.resourcelist);
  },
  methods: {

  }
}


