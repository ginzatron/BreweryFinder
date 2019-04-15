<template>
  <div id="app">
    <the-header></the-header>
    <router-view />
    <the-footer></the-footer>
  </div>
</template>

<script>
import TheHeader from "@/components/TheHeader.vue"
import TheFooter from "@/components/TheFooter.vue"


export default {
    components: {
    TheHeader,
    TheFooter
  },
}
</script>

<style>
@import url("https://fonts.googleapis.com/css?family=Archivo|Merriweather");

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

#footer {
  position: absolute;
}

.card {
  border: 3px solid var(--burgundy);
  border-radius: 10px;
  background-color: rgba(167, 176, 173, 0.9);
  padding: 15px;
}
</style>
