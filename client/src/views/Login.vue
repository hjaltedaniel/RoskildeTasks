<template>
	<div>
		<input v-model="auth.username" placeholder="username">
		<input v-model="auth.password" placeholder="password" type="password">
		<input type="submit" value="Login" v-on:click="login">
		<span v-show="errorMessage != null">{{errorMessage}}</span>
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

	  },
	  mounted () {

	  },
	  methods: {
		login() {
			membersService.login(this.auth.username, this.auth.password)
				.then((response) => {
					let token = response.config.headers.Authorization;
					this.auth.username = undefined;
					this.auth.password = undefined;
					this.isLoggedIn = true;
					this.$store.dispatch("setAuthorization", token);
					let d = new Date();
					d.setTime(d.getTime() + (1 * 2 * 60 * 60 * 1000));
					let expires = "expires="+ d.toUTCString();
					document.cookie = "Token=" + token + ";" + expires + ";path=/";
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
