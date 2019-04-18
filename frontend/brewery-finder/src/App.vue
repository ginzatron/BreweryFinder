<template>
  <div id="app">
    <the-header @goHome="clearForm" :appData="appData" @logout="performLogout"></the-header>
    <router-view v-bind:appData="appData" @beerChosen="searchBeer" @formSubmit="searchForm" @login="performLogin" @reloadFavs="reloadFavs"/>
    <the-footer></the-footer>
  </div>
</template>

<script>
import TheHeader from "@/components/TheHeader.vue"
import TheFooter from "@/components/TheFooter.vue"
import auth from "@/shared/auth";

export default {
    components: {
    TheHeader,
    TheFooter
  },
  data() {
    return {
      appData: {
        formData: {
          name: '',
          zipCode: '',
          happyHour: '',
          range: 30,
          beerName: '',
          center: {
            lat: '',
            lng: ''
          }
        },
        breweries: [],
        favorites: [],
        username: ''
      }
    }
  },
  methods: {
    searchBeer(beerName) {
      this.appData.formData.name = '';
      this.appData.formData.zipCode = '';
      this.appData.formData.happyHour = '';
      this.appData.formData.range = 30;
      this.appData.formData.beerName = beerName;
      this.fetchBreweries();
    },
    searchForm(searchData) {
      this.appData = searchData;
      this.fetchBreweries();
    },
    clearForm() {
      this.appData.formData.name = '';
      this.appData.formData.zipCode = '';
      this.appData.formData.happyHour = '';
      this.appData.formData.range = 30;
      this.appData.formData.beerName = '';
      this.fetchBreweries();
    },
    fetchBreweries() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/brewery?zip=${this.appData.formData.zipCode}&name=${this.appData.formData.name}&happyHour=${this.appData.formData.happyHour}&userLat=${this.appData.formData.center.lat}&userLng=${this.appData.formData.center.lng}&range=${this.appData.formData.range}&beerName=${this.appData.formData.beerName}`)
      .then(response => {
        return response.json();
      })
      .then(json => {
        this.appData.breweries = json;
      })
      .catch(error => console.error(error));
    },
    geolocate: function() {
      navigator.geolocation.getCurrentPosition(position => {
        this.appData.formData.center = {
          lat: position.coords.latitude,
          lng: position.coords.longitude
        };
      });
    },
    performLogin(){
      this.appData.username = auth.getUser().sub;
      this.reloadFavs();
    },
    performLogout() {
      this.appData.favorites = [];
    },
    reloadFavs() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/beer`, {
        method: "GET",
        headers: {
          Authorization: 'Bearer '+ auth.getToken(),
        },
      }).then(response => {
        return response.json();
      }).then(json => {
        this.appData.favorites = json;
      })
    }
  },
  created() {
    this.fetchBreweries();
    this.geolocate();
    if(auth.getUser()) this.appData.username = auth.getUser().sub;
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

section {
    background-image: url('~@/assets/vats.jpg');
    background-size: cover;
    background-repeat: no-repeat;
    background-attachment: fixed;
    background-color: rgba(68, 67, 29, 0.9);
    background-blend-mode: screen;
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
