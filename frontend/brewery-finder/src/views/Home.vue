<template>
  <section class="home">
    <the-map v-bind:breweries="appData.breweries"></the-map>
  </section>
</template>

<script>
// @ is an alias to /src
import TheMap       from "@/components/TheMap.vue"

export default {
  name: "home",
  components: {
    TheMap
  },
  props: {
    appData: Object
  },
};
</script>

<style >
section {
    background-image: url('~@/assets/vats.jpg');
    background-size: cover;
    background-repeat: no-repeat;
    background-attachment: fixed;
    background-color: rgba(68, 67, 29, 0.9);
    background-blend-mode: screen;
}
</style>
