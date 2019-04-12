<template>
  <div>
    <gmap-map :center="center" :zoom="12" style="width:100%;  height: 400px;">
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
  </div>
</template>

<script>
import {EventBus} from "@/shared/event-bus";

export default {
  name: "GoogleMap",
  props: {
    zipcode: String
  },
  data() {
    return {
      breweries: [],
      center: { lat: 41.5038148, lng: -81.6408804 },
      markers: [],
      windowOpen: false,
      description: '',
      address: '',
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
      EventBus.$on('zipClick',() => {
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
           return brewery.zip == Number(this.zipcode);
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