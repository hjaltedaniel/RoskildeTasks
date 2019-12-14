import MessageService from "../../services/MessageService"
import MessageView from "@/components/MessageView"
import Loader from "@/components/Loader"
import _ from "lodash";

export default {
	name: 'messages',
	components: {
		MessageView,
		Loader
	},
	props: [],
	data() {
		return {
			loaded: false,
			mobileOverlayActive: false,
			activeCategory: {},
			messageContent: "",
			errorMessage: "",
			categories: [],
			isMobile: false
		}
	},
	computed: {
		categoriesList() {
			return this.$store.state.categoriesList;
		}
	},
	mounted() {
		this.populateMessages();
		if (this.$store.state.categoriesList.length === 0) {
			this.$store.dispatch("getCategoryList");
		}
	},
	watch: {
		categoriesList: function () {
			if (this.categoriesList != undefined) {
				this.populateMessages();
			}
		},
		activeCategory: function () {
			if (this.activeCategory != undefined) {
				this.$nextTick(() => {
					let elem = this.$el.querySelector("[data-content]");
					elem.scrollTop = elem.scrollHeight;
				})
			}
		}
	},
	methods: {
		setActive(category) {
			if (this.isMobile) {
				this.mobileOverlayActive = true;
			}
			this.activeCategory = category;
		},
		handleResize() {
			if (window.innerWidth < 768) {
				this.isMobile = true;
			} else if (window.innerWidth > 768) {
				this.isMobile = false;
				this.activeCategory = this.categories[0];
			}
		},
		populateMessages() {
			let categories = this.categoriesList;
			let returnArr = [];
			let itemsProcessed = 0;
			categories.forEach(async function (item, index) {
				let categoryMessages;
				await MessageService.getMessagesForCategory(item.Id)
					.then((response) => {
						categoryMessages = response.data;
						itemsProcessed++
					})
					.catch((error) => {
						console.log(error)
					});
				item.Messages = categoryMessages;
				returnArr.push(item);
				if (categories.length == itemsProcessed) {
					callback();
				}
			})
			let callback = () => {
				let sortedArray = _.sortBy(returnArr, function (dateObj) {
					if (dateObj.Messages[0] == undefined) {
						return dateObj.Id;
					} else {
						return new Date(dateObj.Messages[0].Date);
					}
				});
				let sortedCategories = _.reverse(sortedArray)
				this.categories = sortedCategories;
				this.loaded = true;
				if (!this.isMobile) {
					this.activeCategory = sortedCategories[0];
				}
			}
		},
		getSender(bool) {
			if (bool) {
				return "Festival:"
			} else {
				return "You:"
			}
		},
		textTruncate(str, length, ending) {
			if (str.length > length) {
				return str.substring(0, length - ending.length) + ending;
			} else {
				return str;
			}
		},
		isActive(category) {
			if (this.activeCategory == category) {
				return true;
			} else {
				return false;
			}
		},
		getTimeFromDate(date) {
			const d = new Date(date);
			var today = new Date();

			const diffTime = Math.abs(today - d);
			const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

			if (returnDate(d) == returnDate(today)) {
				const min = d.getMinutes();
				const hr = d.getHours();
				let minutes;
				if (min <= 9) {
					minutes = "0" + d.getMinutes();
				} else {
					minutes = d.getMinutes();
				}

				let hours;
				if (hr <= 9) {
					hours = "0" + d.getHours();
				} else {
					hours = d.getHours();
				}
				return hours + ":" + minutes;
			} else if (diffDays == 1 && returnDate(d) != returnDate(today)) {
				return "Yesterday";
			} else if (diffDays > 1 && diffDays < 7) {
				let weekday = new Array(7);
				weekday[0] = "Sun";
				weekday[1] = "Mon";
				weekday[2] = "Tue";
				weekday[3] = "Wed";
				weekday[4] = "Thu";
				weekday[5] = "Fri";
				weekday[6] = "Sat";

				return weekday[d.getDay()]
			} else if (diffDays <= 7 && diffDays < 365) {
				let month = new Array();
				month[0] = "jan";
				month[1] = "feb";
				month[2] = "mar";
				month[3] = "apr";
				month[4] = "may";
				month[5] = "jun";
				month[6] = "jul";
				month[7] = "aug";
				month[8] = "sep";
				month[9] = "oct";
				month[10] = "nov";
				month[11] = "dec";

				return d.getDate() + ". " + month[d.getMonth()]
			} else {
				return returnDate(d);
			}

			function returnDate(date) {
				var dd = String(date.getDate()).padStart(2, '0');
				var mm = String(date.getMonth() + 1).padStart(2, '0'); //January is 0!
				var yyyy = date.getFullYear();
				return dd + '/' + mm + '/' + yyyy;
			}
		},
		async submitMessage() {
			if (this.messageContent != "") {
				this.errorMessage = "";
				this.loaded = false;
				await MessageService.setMessageForCategory(this.activeCategory.Id, this
					.messageContent);
				this.messageContent = "";
				this.populateMessages();
			} else {
				this.errorMessage = "No content set for message"
			}
		},
		sortMessages(arr) {
			let sortedArray = _.sortBy(arr, function (dateObj) {
				return new Date(dateObj.Date);
			});
			return sortedArray;
		}
	}
}
