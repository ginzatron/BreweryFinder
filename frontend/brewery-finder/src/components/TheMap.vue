<template>
  <div>
    <gmap-map :center="center" :zoom="12" style="width:100%;  height: 400px;">
      <gmap-marker v-for="(marker, index) in markers" :key="index" :position="marker.position" @click="toggleInfoWindow(marker,index)">
      </gmap-marker>
      <gmap-info-window
                :position="this.windowPosition"
                :opened="windowOpen"
      >
      {{description}}
      </gmap-info-window>
    </gmap-map>
  </div>
</template>

<script>
export default {
  name: "GoogleMap",
  props: {
    apiURL: String
  },
  data() {
    return {
      breweries: [],
      center: { lat: 41.5038148, lng: -81.6408804 },
      markers: [],
      windowOpen: false,
      description: '',
      windowPosition: null
    };
  },

  mounted() {
    this.geolocate();
  },

  methods: {
    toggleInfoWindow: function(marker,i) {
      this.windowOpen = this.windowOpen ? false :true;
      this.description = this.breweries[i].name;
      this.windowPosition = marker.position;
    },

    addMarker() {
      this.breweries.forEach((brewery) => {
        const marker = {
          lat: brewery.latitude,
          lng: brewery.longitude,
        }
        this.markers.push({ position: marker });
      })
    },
  },
  created() {

      fetch(`${process.env.VUE_APP_REMOTE_API}/brewery?zip=44113`
      ).then((response) => {
          return response.json();
      }).then((json) => {
          console.table(json);
          this.breweries = json;
          this.addMarker();
      }).catch((error => console.error(error)));
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