import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import Login from "./views/Login.vue";
import ViewBeer from "./views/ViewBeer.vue";
import ViewBrewery from "./views/ViewBrewery.vue";
import BeerResults from "./views/BeerResults.vue";
import SearchBrewery from "./views/SearchBrewery.vue";
import auth from "./shared/auth";

Vue.use(Router);

const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "home",
      component: Home,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/beer/:id",
      name: "view-beer",
      component: ViewBeer,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/brewery/:name",
      name: "view-brewery",
      component: ViewBrewery,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/beer/results",
      name: "beerResults",
      component: BeerResults,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/search",
      name: "SearchBrewery",
      component: SearchBrewery,
      meta: {
        requiresAuth: false
      }
    },

  ]
});

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);
  const user = auth.getUser();

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && !user) {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;