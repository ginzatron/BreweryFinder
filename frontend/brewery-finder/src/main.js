import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import * as VueGoogleMaps from "vue2-google-maps";
import GmapCluster from 'vue2-google-maps/src/components/cluster';

Vue.component('GmapCluster', GmapCluster) 

Vue.config.productionTip = false;

Vue.use(VueGoogleMaps, {
  load: {
    key:"AIzaSyAIIPRnAcozwfSqoM40VCnOwE0ZiXhiIY8",
    libraries: "places"
  }
});

new Vue({
  router,
  render: h => h(App)
}).$mount("#app");
