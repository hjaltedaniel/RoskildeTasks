
export default {
  name: 'resource-tabs',
  components: {},
  props: {
    id: Number,
    title: String,
    fileType: String,
    filePath: String,
    categoriesList: Array
  },
  data () {
    return {
      iconColor: 'green',
      tabs: []
    }
  },
  computed: {

  },
  created () {
    //when created, populate 'tabs' with children of ResourceTabs. In this case it's the content inserted in the <slot></slot> by the parent ' Resources'
    this.tabs = this.$children;
  },
  methods: {
    selectTab(selectedTab) {
      //filter through each children (the tabs) and update their 'isActive' prop to either false or true depending on the current tab that is clicked.
      this.tabs.forEach(tab => {
        tab.isActive = (tab.name == selectedTab.name);
      });
    }
  }
}


