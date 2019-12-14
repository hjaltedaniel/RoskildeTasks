<template>
  <div v-if="$store.state.user">
    <Modal />
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
    <div class="d-flex h-100 justify-content-center align-items-center" v-if="isLoading">
      <Loader></Loader>
    </div>
    <router-view v-else />
  </div>
</template>

<script>
import MainMenu from "./components/MainMenu";
import Cookies from "js-cookie";
import Loader from "./components/Loader";
import MembersService from "./services/MembersService";
import Modal from "./components/Modal";

export default {
  components: {
    MainMenu,
    Loader,
    Modal
  },
  data() {
    return {
      isLoading: false
    };
  },
  mounted() {
    if (Cookies.get("Token")) {
      this.isLoading = true;
      MembersService.validate()
        .then(response => {
          this.$store.dispatch("setUser", response.data);
          this.$router.push({ path: "/" });
        })
        .catch(error => {
          this.$store.dispatch("logout", error.message);
        })
        .finally(() => {
          this.isLoading = false;
        });
    }
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
