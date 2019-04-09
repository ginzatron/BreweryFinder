<template>
  <header>
    <div>
      <img src="#" alt="Header Image">
      <h1>Brewery Finder</h1>
    </div>
    <nav>
      <router-link to="/">SEARCH</router-link>
      <div>
        <router-link v-if="!isLoggedIn" to="/login">LOGIN / REGISTER</router-link>
        <h3 v-else>Welcome, {{username}}</h3>
      </div>
    </nav>
  </header>
</template>

<script>
import auth from "@/shared/auth.js"
export default {
  data() {
    return {
      username: ""
    }
  },
  computed: {
    isLoggedIn() {
      return auth.getToken();
    }
  },
  created() {

    fetch(`${process.env.VUE_APP_REMOTE_API}/account/currentUser`, {
      method: "GET",
      headers: {
        Authorization: "Bearer " + auth.getToken()
      }
    })
      .then(response => response.json())
      .then(json => {
        this.username = json;
        this.$router.push("/");
      });
  }
};
</script>

<style scoped>
header {
  background-color: var(--burgundy);
  color: var(--lightGrey);
  border-bottom: 3px solid #777;
  height: 200px;
}

div {
  display: flex;
}

header a {
  text-decoration: none;
  color: var(--lightGrey);
  font-weight: bolder;
}

h1 {
  display: inline-block;
  font-size: 7rem;
  margin-top: 0;
  margin-bottom: 0;
}

h3 {
  margin: 0;
}

img {
  display: inline-block;
  width: 150px;
  height: 150px;
}

nav {
  display: flex;
  justify-content: space-evenly;
  font-size: 3rem;
  background-color: var(--darkGrey);
}
</style>
