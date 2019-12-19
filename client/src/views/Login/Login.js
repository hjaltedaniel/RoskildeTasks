import membersService from "../../services/MembersService";
import Cookies from 'js-cookie';

export default {
	name: "login",
	data() {
		return {
			auth: {
				username: null,
				password: null,
				remember: false
			},
			passwordFieldType: "password",
			errorMessage: "",
			loading: false
		};
	},
	computed: {
		logoutMessage() {
			return this.$store.state.logoutMessage;
		}
	},
	methods: {
		login() {
			this.loading = true;
			membersService.login(this.auth.username, this.auth.password)
				.then(response => {
					let token = response.config.headers.Authorization.replace(
						"Basic ",
						""
					);
					if (this.auth.remember) {
						this.$store.dispatch("setAuthorization24h", token);
					} else {
						this.$store.dispatch("setAuthorizationSession", token);
					}
					this.$store.dispatch("setUser", response.data)
					this.$router.push({ path: '/' });
				})
				.catch(error => {
					if (error.message == "Request failed with status code 401") {
						this.errorMessage = "There was something wrong with your username or password. Try again - or contact Roskilde Festival if the problem continues.";
					} else {
						this.errorMessage = error.message;
					}
				})
				.finally(() => {
					this.loading = false;
				});
		},
		changePasswordVisibility() {
			if (this.passwordFieldType == "password") {
				this.passwordFieldType = "text";
			} else if (this.passwordFieldType == "text") {
				this.passwordFieldType = "password";
			}
		}
	}
};