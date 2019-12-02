<template>
	<section class="login">
		<div class="login__logo">
			<img src="../assets/Icons/canopy.svg" alt="RF Tasks" class="header-logo__image d-none d-md-block" />
		</div>
		<div class="container h-100 d-flex align-items-center justify-content-center">
			<div class="login__content">
				<div class="row">
					<div class="col-12">
						<h1 class="login__headline">RF Tasks</h1>
						<p class="login__misc-text">Please login using the credentials provided by Roskilde Festival</p>
					</div>
					<div class="form-group col-12">
						<label for="username" class="login__label">Username</label>
						<input class="form-control login__text" v-model="auth.username"
							placeholder="Your username is usually your email" />
					</div>
					<div class="form-group col-12">
						<label for="password" class="login__label">Password</label>
						<input class="form-control login__text" v-model="auth.password"
							placeholder="Enter your password" :type="passwordFieldType" />
					</div>
					<div class="col-12 d-flex align-items-center justify-content-between">
						<button v-on:click="login" type="button" class="btn btn-success login__button mr-3">Login</button>
						<div class="form-check mr-auto d-flex align-items-center">
							<input class="form-check-input" type="checkbox" v-model="auth.remember" id="remember">
							<label class="form-check-label" for="remember">
								Remember me
							</label>
						</div>
						<span v-show="passwordFieldType == 'password'" class="login__visibility-toggle"
							  v-on:click="changePasswordVisibility">
							<font-awesome-icon icon="eye" /> Show password
						</span>
						<span v-show="passwordFieldType == 'text'" class="login__visibility-toggle"
							  v-on:click="changePasswordVisibility">
							<font-awesome-icon icon="eye-slash" /> Hide password
						</span>
					</div>
					<div class="col-12 login__messages" v-show="errorMessage != '' || logoutMessage != ''">
						<div class="alert alert-danger" v-show="errorMessage != ''" role="alert">{{errorMessage}}</div>
						<div v-show="logoutMessage != ''" class="alert alert-info" role="alert">{{logoutMessage}}</div>
					</div>
				</div>
			</div>
		</div>
	</section>
</template>

<script>
	import membersService from "../services/MembersService";

	export default {
		name: "login",
		components: {},
		props: [],
		data() {
			return {
				auth: {
					username: undefined,
					password: undefined,
					remember: false
				},
				passwordFieldType: "password",
				errorMessage: ""
			};
		},
		computed: {
			logoutMessage() {
				return this.$store.state.logoutMessage;
			}
		},
		mounted() { },
		methods: {
			login() {
				membersService
					.login(this.auth.username, this.auth.password)
					.then(response => {
						let token = response.config.headers.Authorization.replace(
							"Basic ",
							""
						);
						this.auth.username = undefined;
						this.auth.password = undefined;
						if (this.auth.remember) {
							this.$store.dispatch("setAuthorization24h", token);
						}
						else {
							this.$store.dispatch("setAuthorizationSession", token);
						}
						this.auth.remember = false;
					})
					.catch(error => {
						if (error.message == "Request failed with status code 401") {
							this.errorMessage = "There was something wrong with your username or password. Try again - or contact Roskilde Festival if the problem continues.";
							this.username = undefined;
							this.password = undefined;
						}
						else {
							console.log(error.message)
							this.errorMessage = error.message;
							this.username = undefined;
							this.password = undefined;
						}

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
</script>

<style lang="scss" scoped>
	.login {
		height: 100vh;
		background-image: url(../assets/rf19_people_wallpaper.png);
		background-size: cover;

		&__logo {
			position: absolute;
			top: 15px;
			left: 15px;
		}

		&__headline {
			font-family: $font-heavy;
			text-transform: uppercase;
			font-size: 65px;
			text-align: center;
			color: $color-orange-chilean-fire;
		}

		&__misc-text {
			font-size: 20px;
			text-align: center;
		}

		&__content {
			background-color: $color-white;
			width: 60%;
			padding: 40px;

			@media screen and (max-width: $viewport-medium) {
				width: 100%;
			}
		}

		&__visibility-toggle {
			cursor: pointer;
		}

		&__messages {
			padding-top: 20px;
		}

		&__text {
			width: 100%;
		}

		&__label {
			font-family: $font-bold;
		}
	}
</style>
