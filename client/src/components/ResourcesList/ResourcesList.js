
export default {
  name: 'resources-list',
  components: {},
  props: {
    name: {required: true},
    selected: {default: false}
  },
  data () {
    return {
      isActive: false
      /* files: [
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
      ] */
    }
  },
  computed: {
    href() {
      //replace removes the spaces and replaces it with -
      //ex: www.about us.dk is changed to www.about-us.dk
      return '#' + this.name.toLowerCase().replace(/ /g, '-');
    }
  },
  mounted () {
    this.isActive = this.selected;
  },
  methods: {

  }
}


