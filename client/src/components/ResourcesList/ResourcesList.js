
export default {
  name: 'resources-list',
  components: {},
  props: [],
  data () {
    return {
      files: [
        {
          unread: false,
          title: 'Title of the document/file',
          type: 'Filetype',
          category: 'Category'
        },
        {
          unread: true,
          title: 'Title of the document/file',
          type: 'Filetype',
          category: 'Category'
        },
        {
          unread: false,
          title: 'Title of the document/file',
          type: 'Filetype',
          category: 'Category'
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


