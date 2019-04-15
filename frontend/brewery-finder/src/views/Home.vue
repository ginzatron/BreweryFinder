<template>
  <section class="home">
    <the-map></the-map>
  </section>
</template>

<script>
// @ is an alias to /src
import TheMap       from "@/components/TheMap.vue"

export default {
  name: "home",
  components: {
    TheMap
  }
};
</script>

<style scoped>
section {
    background-image: url('~@/assets/vats.jpg');
    background-size: cover;
    background-repeat: no-repeat;
    background-attachment: fixed;
}
</style>
