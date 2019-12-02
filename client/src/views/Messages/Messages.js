import MessageService from "../../services/MessageService"
import MessageView from "@/components/MessageView"
import _ from "lodash";

export default {
	name: 'messages',
	components: {
		MessageView
	},
	props: [],
	data () {
		return {
			loaded: false,
			activeCategory: {},
			messageContent: "",
			errorMessage: "",
			messages: []
	}
	},
	computed: {
		categoriesList() {
			return this.$store.state.categoriesList;
		},
		sortedMessages() {
			let sortedArray = _.sortBy(this.messages, function (dateObj) {
				return new Date(dateObj.Date);
			});
			return sortedArray;
		}
	},
	mounted() {
	},
	watch: {
		categoriesList: function () {
			if (this.categoriesList != undefined) {
				this.activeCategory = this.categoriesList[0];
				this.getMessages();
				this.loaded = true;
			}
		}
	},
	methods: {
		setActive(category) {
			this.activeCategory = category;
			this.getMessages();
		},
		getMessages() {
			MessageService.getMessagesForCategory(this.activeCategory.Id)
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
				MessageService.setMessageForCategory(this.activeCategory.Id, this.messageContent);
				this.messageContent = "";
			}
			else {
				this.errorMessage = "No content set for message"
			}
			
		}
	}
}


