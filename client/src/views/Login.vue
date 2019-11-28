<template>
	<div>
		<input v-model="auth.username" placeholder="username">
		<input v-model="auth.password" placeholder="password" type="password">
		<input type="submit" value="Login" v-on:click="login">
		<span v-show="errorMessage != ''">{{errorMessage}}</span>
		<span v-show="logoutMessage != ''">{{logoutMessage}}</span>
	</div>
</template>

<script>
	import membersService from "../services/MembersService";

	export default {
	  name: 'login',
	  components: {},
	  props: [],
	  data () {
		return {
			auth: {
				username: undefined,
				password: undefined
			},
			errorMessage: ""
		}
	  },
	  computed: {
		  logoutMessage() {
			  return this.$store.state.logoutMessage;
		  }
	  },
	  mounted () {

	  },
	  methods: {
		login() {
			membersService.login(this.auth.username, this.auth.password)
				.then((response) => {
					let token = response.config.headers.Authorization.replace("Basic ", "");
					this.auth.username = undefined;
					this.auth.password = undefined;
					this.$store.dispatch("setAuthorization", token);
				})
				.catch((error) => {
					this.errorMessage = error.message;
					this.username = undefined;
					this.password = undefined;
				});
		}
	  }
	}
</script>

<style lang="scss" scoped>
</style>
