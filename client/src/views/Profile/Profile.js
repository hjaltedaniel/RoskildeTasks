import MembersService from "../../services/MembersService"

export default {
	name: 'profile',
	data() {
		return {
			logoutMessages: [
				"Catch you later.",
				"Tea time? See you later.",
				"See you later alligator",
				"In a while - crocodile",
				"Hate to see you go - but see you again soon",
				"Many kisses from us at Roskilde Festival"
			],
			modal: {
				active: false,
				content: "",
				data: {
					OldPassword: '',
					NewPassword: '',
					NewEmail: '',
					ErrorMessage: ''
				},
				loading: false
			},
			value: "",
		}
	},
	computed: {
		user() {
			return this.$store.state.user;
		}
	},
	methods: {
		logout() {
			this.$store.dispatch("logout", this.logoutMessages[Math.floor(Math.random() * this.logoutMessages.length)]);
			this.$router.push({ path: 'login' });
		},
		setModal(content) {
			this.modal.active = true;
			this.modal.content = content;
		},
		clearModalData() {
			this.modal.data.OldPassword = '';
			this.modal.data.NewEmail = '';
			this.modal.data.NewPassword = '';
			this.value = '';
		},
		submitModal(content) {
			if (content == 'password') {
				this.$refs.newPass.validate()
					.then(success => {
						if (success) {
							this.modal.loading = true;
							MembersService.changePassword(this.modal.data.OldPassword, this.modal.data.NewPassword)
								.then(response => {
									let token = response.data;
									this.$store.dispatch("setAuthorizationSession", token);
									this.clearModalData();
									this.modal.active = false;
									this.modal.loading = false;
								})
								.catch(error => {
									if (error.message = 'Request failed with status code 400') {
										this.modal.data.ErrorMessage = "The old password didn't match."
										this.modal.data.OldPassword = '';
									}
									else {
										this.modal.data.ErrorMessage = error.message;
									}
									this.modal.loading = false;
								})
						}
					})
			}
			else if (content == 'email') {
				this.$refs.email.validate()
					.then(success => {
						if (success) {
							this.modal.loading = true;
							MembersService.changeEmail(this.modal.data.NewEmail)
								.then(response => {
									let token = response.data;
									this.$store.dispatch("setAuthorizationSession", token);
									this.$store.dispatch("setNewEmail", this.modal.data.NewEmail);
									this.clearModalData();
									this.modal.active = false;
									this.modal.loading = false;
								})
								.catch(error => {
									this.modal.data.ErrorMessage = error.message;
									this.modal.loading = false;
								});
						}
					})
			}
		}
	}
}


