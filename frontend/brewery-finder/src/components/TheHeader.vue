<template>
  <header>
    <nav>
      <router-link to="/">Home</router-link>
      <router-link to="/brewery/search">Search  <span><a href="#" class="fa fa-search"></a>  </span></router-link>
      <h2>Brewery Finder</h2>
      <div class='login'>
        <router-link v-if="!username" to="/login">Login / Register</router-link>
        <a v-else>Welcome {{username}}</a>
        <a v-if="username" @click="logout">Logout</a>
      </div>
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
  /* border-bottom: 3px solid #777; */
  height: 120px;
  font-family: archivo;
}
.fa-search {
  color: goldenrod;
}

div {
  display: flex;
}

.centered {
  justify-content: center;
}

.home-search {
  display: flex;
  justify-content: space-around;
}

header a {
  text-decoration: none;
  color: var(--lightGrey);
  font-weight: bolder;
  cursor: pointer;
}

h1 {
  display: inline-block;
  font-size: 4rem;
  margin-top: 0;
  margin-bottom: 0;
}

h2 {
  margin: 0;
  margin-top:15px;
  padding-right: 100px;
  font-size: 3em;
}

img {
  display: inline-block;
  width: 150px;
  height: 150px;
}

nav {
  display: flex;
  justify-content: space-evenly;
  font-size: 1.5rem;
  background-color: var(--burgundy);
  padding: 5px 0;
  margin-top:15px;
  align-items: center;

}
</style>
