<template>
  <div>
    <gmap-map :center="center" :zoom="12" style="width:100%;  height: 400px;">
      
      <gmap-marker v-for="(marker, index) in markers" :key="index" :position="marker.position" @click="toggleInfoWindow(marker,index)"> <gmap-info-window :options="infoOptions" :position="infoWindowPos" :opened="infoWinOpen" @closeclick="infoWinOpen=true">
        {{infoContent}}
      </gmap-info-window> ></gmap-marker>
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
      infoContent: {
        infoWindowPos: null,
        infoWinOpen: false,
        currentMidx:null,
        infoOptions: {
          pixelOffset: {
            width: 0,
            height: -35
          }
        },
      }
    };
  },

  mounted() {
    this.geolocate();
  },

  methods: {
    toggleInfoWindow: function(marker, idx) {
            this.infoWindowPos = marker.position;
            this.infoContent = marker.name;
            //check if its the same marker that was selected if yes toggle
            if (this.currentMidx == idx) {
              this.infoWinOpen = !this.infoWinOpen;
            }
            //if different marker set infowindow to open and reset current marker index
            else {
              this.infoWinOpen = true;
              this.currentMidx = idx;
            }
    },

    addMarker() {
      this.breweries.forEach((brewery) => {
        const marker = {
          lat: brewery.latitude,
          lng: brewery.longitude
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