<template>
  <div id="app">
    <the-header></the-header>
    <router-view />
    <the-map name="example"></the-map>
    <the-footer></the-footer>
  </div>
</template>

<script>
import TheHeader from "@/components/TheHeader.vue"
import TheFooter from "@/components/TheFooter.vue"
import TheMap       from "@/components/TheMap.vue"

export default {
    components: {
    TheHeader,
    TheFooter,
    TheMap
  },
}
</script>

<style>
@import url("https://fonts.googleapis.com/css?family=Archivo");

:root {
  --darkGrey: #2f3030;
  --goldFish: #a06500;
  --burgundy: #490000;
  --lightGrey: #a7b0ad;
  --ghostWhite: #ffffff;
}

#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}

body {
  background-image: url('assets/vats.jpg');
  min-height: 1325px;
}
</style>
