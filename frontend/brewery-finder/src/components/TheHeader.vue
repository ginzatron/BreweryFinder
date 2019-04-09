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
        <p v-else>Welcome {{username}}</p>
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
      });
  }
};
</script>

<style scoped>
header {
  background-color: lightblue;
  border-bottom: 3px solid #777;
  height: 200px;
}

div {
  display: flex;
}

h1 {
  display: inline-block;
  font-size: 7rem;
  margin-top: 0;
  margin-bottom: 0;
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
  background-color: lightgreen;
}
</style>
