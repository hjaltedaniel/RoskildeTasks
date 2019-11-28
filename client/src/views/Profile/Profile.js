
export default {
  name: 'profile',
  components: {},
  props: [],
  data () {
    return {
		logoutMessages: [
			"Catch you later.",
			"Tea time? See you later.",
			"See you later alligator",
			"In a while - crocodile",
			"Hate to see you go - but see you again soon",
			"Many kisses from us at Roskilde Festival"
		]
    }
  },
  computed: {

  },
  mounted () {

  },
  methods: {
	  logout() {
		  this.$store.dispatch("logout", this.logoutMessages[Math.floor(Math.random() * this.logoutMessages.length)]);
	  }
  }
}


