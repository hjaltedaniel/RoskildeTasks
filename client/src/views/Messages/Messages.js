import MessageService from "../../services/MessageService"

export default {
	name: 'messages',
	components: {},
	props: [],
	data () {
		return {
			activeCategory: 0,
			messageContent: "",
			errorMessage: "",
			messages: []
	}
	},
	computed: {
		categoriesList() {
			return this.$store.state.categoriesList;
		},
	},
	mounted() {
	},
	methods: {
		setActive(id) {
			this.activeCategory = id;
			this.getMessages();
		},
		getMessages() {
			MessageService.getMessagesForCategory(this.activeCategory)
				.then((response) => {
					this.messages = response.data;
				})
				.catch((error) => {
					console.log(error)
				});
		},
		submitMessage() {
			if (this.messageContent != "") {
				this.errorMessage = "";
				MessageService.setMessageForCategory(this.activeCategory, this.messageContent);
				this.messageContent = "";
				this.getMessages();
			}
			else {
				this.errorMessage = "No content set for message"
			}
			
		}
	}
}


