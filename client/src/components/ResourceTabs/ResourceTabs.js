
export default {
  name: 'resource-tabs',
  components: {},
  props: [],
  data () {
    return {
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
      ],
      iconColor: 'green'
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


