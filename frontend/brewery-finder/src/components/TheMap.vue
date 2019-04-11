<template>
  <div>
    <gmap-map
      :center="center"
      :zoom="12"
      style="width:75%;  height: 400px;"
    >
      <gmap-marker v-for="(marker, index) in markers" :key="index" :position="marker.position" ></gmap-marker>
    </gmap-map>
    <div v-for="brewery in this.breweries" v-bind:key="brewery.name">
        <h3>{{brewery.name}} {{brewery.address}}</h3>
    </div>
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
    };
  },

  mounted() {
    this.geolocate();
  },

  methods: {
    addMarker() {
      this.breweries.forEach((brewery) => {
        const marker = {
          lat: brewery.latitude,
          lng: brewery.longitude
        }
        this.markers.push({ position: marker });
      })
    }
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