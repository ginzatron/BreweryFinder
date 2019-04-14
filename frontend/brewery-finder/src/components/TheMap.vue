<template>
  <div id="map">
    <gmap-map :center="center" :zoom="11" style="width:90%;  height: 700px; margin: 50px; position:relative">
      <gmap-marker v-for="(marker, index) in markers" :key="index" :position="marker.position" @click="toggleInfoWindow(marker,index)">
      </gmap-marker>
      <gmap-info-window
                :options="infoOptions"
                :position="this.windowPosition"
                :opened="windowOpen"
      >
      {{description}}
      <br>
      {{address}}
      </gmap-info-window>
    </gmap-map>
    <div class=transbox>
          <p>Map markers for local breweries</p>
        </div>
    <table v-if="zipcode">
        <thead>
          <tr>
            <th>Name</th>
            <th>Happy Hour(s)</th>
            <th>Type</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="brewery in filteredBreweries" v-bind:key="brewery.id">
            <td>
              <router-link
                v-bind:to="{name: 'view-brewery', params:{id: brewery.id}}"
              >{{brewery.name}}</router-link>
            </td>
            <td>{{brewery.happyHourFrom}} to {{brewery.happyHourTo}}</td>
            <td>{{brewery.isBar}} {{brewery.isBrewery}}</td>
          </tr>
        </tbody>
      </table>
  </div>
</template>

<script>
import {EventBus} from "@/shared/event-bus";

export default {
  name: "GoogleMap",
  data() {
    return {
      zipcode: '',
      breweries: [],
      center: { lat: 41.5038148, lng: -81.6408804 },
      markers: [],
      windowOpen: false,
      description: '',
      address: '',
      breweryLocId:'',
      windowPosition: null,
      infoOptions: {
          pixelOffset: {
            width: 0,
            height: -35
          }
        },
    };
  },

  mounted() {
    this.geolocate();
  },

  methods: {
    toggleInfoWindow: function(marker,i) {
      this.windowOpen = this.windowOpen ? false :true;
      this.description = this.breweries[i].name;
      this.address = this.breweries[i].address;
      this.windowPosition = marker.position;
    },

    addMarker() {
      this.filteredBreweries.forEach((brewery) => {
        const marker = {
          lat: brewery.latitude,
          lng: brewery.longitude,
        }
        this.markers.push({ position: marker });
      })
    },
  },
  created() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/brewery`
      ).then((response) => {
          return response.json();
      }).then((json) => {
          this.breweries = json;
          this.addMarker();
      }).catch((error => console.error(error)));
      EventBus.$on('zipClick',(z) => {
        this.zipcode = z;
        this.markers = [];
        this.addMarker();
      });
  },
  computed: {
    filteredBreweries() {
      if (!this.zipcode) {
        return this.breweries;
      } else {
          return  this.breweries.filter(brewery => {
           return brewery.zip == this.zipcode;
          })
    }
  }
  },
    geolocate: function() {
      navigator.geolocation.getCurrentPosition(position => {
        this.center = {
          lat: position.coords.latitude,
          lng: position.coords.longitude
        };
      });
    }
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
    position: absolute;
    background-color: #ffffff;
    border: 1px solid black;
    opacity: 0.6;
    margin-top: 50px;
}

div.transbox p {
  font-weight: bold;
  color: #000000;
}
  </style>
