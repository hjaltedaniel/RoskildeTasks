<template>
  <div class="columns">
    <MainMenu />
    <ContentArea>
      <router-view />
    </ContentArea>
  </div>
</template>

<script>
import MainMenu from "@/components/MainMenu.vue";
import ContentArea from "@/components/ContentArea.vue";
export default {
  name: "Home",
  components: {
    MainMenu,
    ContentArea
  }
};
</script>

<style lang="scss">
@import "../src/assets/Styles/Site";
</style>
