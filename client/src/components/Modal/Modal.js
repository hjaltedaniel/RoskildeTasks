
export default {
  name: 'modal',
  data () {
    return {
		visible: false,
    }
  },
  methods: {
	  toggleModal() {
		  this.visible = !this.visible;
	  }
  }
}
