<template>
	<div v-if="isLoggedIn">
		<MainMenu />
		<div class="main-content">
			<div class="container-fluid col-md-12">
				<div class="view-wrapper">
					<router-view />
				</div>
			</div>
		</div>
	</div>
	<div v-else>
		<Login></Login>
	</div>
</template>

<script>
	import MainMenu from "./components/MainMenu";
	import Login from "./views/Login";

export default {
	components: {
		MainMenu,
		Login
	},
	computed: {
		isLoggedIn() {
			let tokenCookie = this.getCookie("Token");
			if (this.$store.state.token != undefined) {
				this.$store.dispatch("getTaskList");
				this.$store.dispatch("getCategoryList");
				this.$store.dispatch("getRessourceList");

				return true
			}
			else if (tokenCookie != "") {
				this.$store.dispatch("setAuthorization", tokenCookie);
				this.$store.dispatch("getTaskList");
				this.$store.dispatch("getCategoryList");
				this.$store.dispatch("getRessourceList");

				return true
			}
			else {
				return false
			}
		}
	},
	methods: {
		getCookie(cname) {
			var name = cname + "=";
			var decodedCookie = decodeURIComponent(document.cookie);
			var ca = decodedCookie.split(';');
			for(var i = 0; i <ca.length; i++) {
			var c = ca[i];
			while (c.charAt(0) == ' ') {
				c = c.substring(1);
			}
			if (c.indexOf(name) == 0) {
				return c.substring(name.length, c.length);
			}
			}
			return "";
		}
	}
};
</script>

<style lang="scss" scoped>
	.main-content {
        padding-left: 120px;
        .container-fluid {
            padding: 60px;
            .view-wrapper {
				height: 87vh;
				overflow-y: auto;
                border-radius: 5px;
                background-color: $color-white;
                box-shadow: 0 0 7px #00000061;
            }
        }
    }
</style>
