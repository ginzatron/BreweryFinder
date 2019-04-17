<template>
  <div id="app">
    <the-header @goHome="clearForm"></the-header>
    <router-view v-bind:appData="appData" @beerChosen="searchBeer" @formSubmit="searchForm"/>
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
        breweries: []
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
    }
  },
  created() {
    this.fetchBreweries();
    this.geolocate();
  }
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
