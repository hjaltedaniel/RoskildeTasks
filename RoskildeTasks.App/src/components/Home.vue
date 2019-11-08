<template>
  <div class="columns">
    <MainMenu />
    <ContentArea></ContentArea>
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

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
