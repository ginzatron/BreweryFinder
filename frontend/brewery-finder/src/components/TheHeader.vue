<template>
  <header>
    <div>
      <img src="#" alt="Header Image">
      <h1>Brewery Finder</h1>
    </div>
    <nav>
      <router-link to="/">SEARCH</router-link>
      <div>
        <router-link v-if="!username" to="/login">Login / Register</router-link>
        <a v-else>Welcome {{username}}</a>
      </div>
      <a v-if="username" @click="logout">Logout</a>
    </nav>
  </header>
</template>

<script>
import auth from "@/shared/auth.js"
import {EventBus} from "@/shared/event-bus"

export default {
  data() {
    return {
      username: ""
    }
  },
  computed: {
    isLoggedIn() {
      return auth.getUser() !== null;
    }
  },
  created() {
    EventBus.$on('login', (ev) => {
      this.username = ev;
    }),

    fetch(`${process.env.VUE_APP_REMOTE_API}/account/currentUser`, {
      method: "GET",
      headers: {
        Authorization: "Bearer " + auth.getToken()
      }
    })
      .then(response => {
        if(response.ok) {
          response.json();
        }
      })
      .then(json => {
        this.username = json;
        this.$router.push("/");
    });
  },
  methods: {
    logout() {
      auth.destroyToken();
      this.username = null;
    }
  }
};
</script>

<style scoped>
header {
  background-color: var(--burgundy);
  color: var(--lightGrey);
  border-bottom: 3px solid #777;
  height: 200px;
  font-family: archivo;
}

div {
  display: flex;
}

header a {
  text-decoration: none;
  color: var(--lightGrey);
  font-weight: bolder;
  cursor: pointer;
}

h1 {
  display: inline-block;
  font-size: 7rem;
  margin-top: 0;
  margin-bottom: 0;
}

h2 {
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
