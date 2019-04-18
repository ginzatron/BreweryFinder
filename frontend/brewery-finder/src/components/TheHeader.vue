<template>
  <header>
    <nav>
      <div class="home-search">
        <div class="push-right" @click="goHome">
          <router-link to="/">Home</router-link>
        </div>
        <div>
          <router-link to="/search">
            Search
            <span>
              <i class="fa fa-search"/>
            </span>
          </router-link>
        </div>
      </div>
      <h2 @click="goHome">
        <router-link to="/">BreweryFinder</router-link>
      </h2>
      <div class="push-right">
        <router-link v-if="!appData.username" to="/login">Login / Register</router-link>
        <a v-else>Welcome {{appData.username}} </a>
        <a v-if="appData.username" @click="logout">Logout</a>
      </div>
    </nav>
  </header>
</template>

<script>
import auth from "@/shared/auth.js";

export default {
  props: {
    appData: Object
  },
  methods: {
    logout() {
      auth.destroyToken();
      this.appData.username = "";
      this.$router.push("/");
      this.$emit('logout');
    },
    goHome() {
      this.$emit("goHome");
    }
  }
};
</script>

<style scoped>
header {
  background-color: var(--burgundy);
  color: var(--lightGrey);
  border-bottom: 3px solid #777;
  font-family: archivo;
}

nav {
  display: flex;
  justify-content: space-around;
  font-size: 1.5rem;
  background-color: var(--burgundy);
  padding: 5px 0;
  align-items: center;
}

.fa-search {
  color: goldenrod;
}

.home-search {
  display: flex;
}

header a {
  text-decoration: none;
  color: var(--lightGrey);
  font-weight: bolder;
  cursor: pointer;
}

.push-right {
  margin-right: 50px;
}

h2 {
  margin: 0;
  font-size: 2em;
}

/* change to length view at <800px */
</style>
