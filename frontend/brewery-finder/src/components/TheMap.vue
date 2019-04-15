<template>
  <div id="map">
    <gmap-map
      :center="center"
      :zoom="11"
      style="width:90%;  height: 700px; margin: 50px; position:relative"
    >
      <gmap-marker
        v-for="(marker, index) in markers"
        :key="index"
        n}
        :icon="marker.icon"
        :position="marker.position"
        @mouseover="toggleInfoWindow(marker,index)"
        @mouseout="toggleInfoWindow(marker,index)"
        @click="redirect(index)"
      ></gmap-marker>
      <gmap-info-window :options="infoOptions" :position="this.windowPosition" :opened="windowOpen" style="font-family: dokdo;">
        <div id='infoWindow'>
          <div class='info'>
            <div class='description'>
              {{description}}
            </div>
            <div class='address'>
              {{address}}
            </div>
          </div>
          <img :src="this.beerThumb"/>
        </div>
      </gmap-info-window>
    </gmap-map>
    <div class="transbox">
      <p>Map markers for local breweries</p>
    </div>
    <table v-if="breweries">
      <thead>
        <tr>
          <th>Name</th>
          <th>Happy Hour(s)</th>
          <th>Type</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="brewery in breweries" v-bind:key="brewery.id">
          <td>
            <router-link
              v-bind:to="{name: 'view-brewery', params:{id: brewery.id}}"
            >{{brewery.name}}</router-link>
          </td>
          <td>{{timeFormat(brewery.happyHourFrom,brewery.happyHourTo)}}</td>
          <td>{{barRestaurant(brewery.isBar,brewery.isBrewery)}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { EventBus } from "@/shared/event-bus";

export default {
  name: "GoogleMap",
  data() {
    return {
      formData: {name: '',zipCode: '', happyHour: '', range: 50},
      breweries: [],
      center: { lat: 41.5038148, lng: -81.6408804 },
      markers: [],
      windowOpen: false,
      description: "",
      address: "",
      beerThumb: null,
      windowPosition: null,
      infoOptions: {
        pixelOffset: {
          width: 0,
          height: -35
        }
      }
    };
  },

  mounted() {
    this.geolocate();
  },

  methods: {
    toggleInfoWindow: function(marker, i) {
      this.windowOpen = this.windowOpen ? false : true;
      this.description = this.breweries[i].name;
      this.address = this.breweries[i].address;
      this.windowPosition = marker.position;
      this.beerThumb = this.breweries[i].imgSrc;
    },

    addMarker() {
      this.breweries.forEach(brewery => {
        const marker = {
          lat: brewery.latitude,
          lng: brewery.longitude,
        };
        this.markers.push({ position: marker});
      });
    },
    redirect(index) {
      this.$router.push("/brewery/search/" + this.breweries[index].id);
    },
    timeFormat(a,b) {
      let timeA = a.split(":").shift();
      let timeB = b.split(":").shift();
      if (timeA > 12) return (`${timeA-12} pm - ${timeB-12} pm`);
      else if (timeA > 0 && timeB <12) return (`${timeA} am - ${timeB} am`);
      else if (timeA == 0) return "nope";
    },
    barRestaurant(a,b){
      if (a && b) return ("Bar/Brewery");
      else if (a && !b) return ("Bar");
      return ("Brewery");
    },
    updateBreweries(){
      fetch(`${process.env.VUE_APP_REMOTE_API}/brewery?zip=${this.formData.zipCode}&name=${this.formData.name}&happyHour=${this.formData.happyHour}&userLat=${this.center.lat}&userLng=${this.center.lng}&range=${this.formData.range}`)
      .then(response => {
        return response.json();
      })
      .then(json => {
        this.breweries = json;
        this.markers = [];
        this.addMarker();
      })
      .catch(error => console.error(error));
    },
    geolocate: function() {
    navigator.geolocation.getCurrentPosition(position => {
      this.center = {
        lat: position.coords.latitude,
        lng: position.coords.longitude
      };
    });
  }
  },
  created() {
    this.updateBreweries();
    EventBus.$on("zipClick",data => {
      this.formData.zipCode = data.zipCode;
      this.formData.name = data.name;
      this.formData.happyHour = data.happyHour;
      if (data.range) this.formData.range = data.range;
      else this.formData.range = 50;
      this.updateBreweries();
    });
  },
  
};
</script>
<style scoped>

  #map {
    display: flex;
    justify-content: center;
    flex-wrap: wrap;
  }
  a, td{
    color:maroon;
    font-weight:bolder;
  }  
  div.transbox {
  margin-right: 30px;
  }
  #infoWindow {
  background-color: lightgoldenrodyellow;
  display: flex;
  flex-direction:row;
  justify-content: center;
  font-family: Merriweather;
}
.info {
  display: flex;
  flex-direction: column;
  margin-top: 30px;
  margin-right: 8px;
}
.description {
  margin-bottom: 10px;
}
table {
  padding-bottom: 25px;
  border: 1px solid black;
  padding: 8px;
  border-radius: 10px;
  background-color: rgba(167,176,173,0.75);
}
img {
  height: 100px;
  width: 75px;
  align-self: center;
}
#map {
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
}
a, td {
  color: maroon;
  font-weight: bolder;
}
div.transbox {
  position: absolute;
  background-color: #ffffff;
  border: 1px solid black;
  opacity: 0.6;
  margin-top: 50px;
}

div.transbox p {
  font-weight: bold;
  color: #000000;
  padding-left: 5px;
  padding-right: 5px;
}
</style>
