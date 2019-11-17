<template>
	<div>
		<MainMenu />
		<div class="main-content">
			<div class="container-fluid col-md-12">
				<div class="view-wrapper">
					<router-view />
				</div>
			</div>
		</div>
	</div>
</template>

<script>
import MainMenu from "./components/MainMenu";

export default {
  components: {
    MainMenu
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