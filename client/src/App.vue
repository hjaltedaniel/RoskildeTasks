<template>
	<div v-if="isLoggedIn">
		<MainMenu />
		<div class="main-content">
			<div class="container-fluid col-md-12">
				<div class="view-wrapper">
					<router-view/>
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
	import Cookies from 'js-cookie';

export default {
	components: {
		MainMenu,
		Login
	},
	computed: {
		isLoggedIn() {
			return true
			if (this.$store.state.token != undefined) {
				return true
			}
			else if (Cookies.get('Token') != undefined) {
				this.$store.dispatch("setAuthorization", Cookies.get('Token'));
				return true;
			}
			else {
				return false
			}
		},
		token() {
			return this.$store.state.token;
		}
	},
	// watch: {
	// 	token: function () {
	// 		if (this.token != undefined) {
	// 			this.$store.dispatch("getTaskList");
	// 			this.$store.dispatch("getCategoryList");
	// 			this.$store.dispatch("getRessourceList");
	// 		}
	// 	}
	// },
	methods: {
	}
};
</script>

<style lang="scss" scoped>
	.main-content {
        padding-left: 120px;
        .container-fluid {
            padding: 3%;
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
