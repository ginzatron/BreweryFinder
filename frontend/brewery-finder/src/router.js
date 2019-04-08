import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import Register from "./views/Register.vue";
import Login from "./views/Login.vue";
import ViewBeer from "./views/ViewBeer.vue";
import ViewBrewery from "./views/ViewBrewery.vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/register",
      name: "register",
      component: Register
    },
    {
      path: "/beer/:id",
      name: "view-beer",
      component: ViewBeer
    },
    {
      path: "/brewery/:id",
      name: "view-brewery",
      component: ViewBrewery
    },
    {
      path: "/login",
      name: "login",
      component: Login
    }

  ]
});
