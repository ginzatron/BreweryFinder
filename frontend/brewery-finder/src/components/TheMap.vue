<template>
  <div id="map">
    <gmap-map
      :center="userCenter"
      :zoom="10"
      style="height : 100%; width : 100%; position : absolute; position: top; "
    >
    <gmap-cluster :zoomOnClick="zoom">
      <gmap-marker
        v-for="(marker, index) in computedMarkers"
        :key="index"
        :icon="marker.icon"
        :position="marker.position"
        @mouseover="toggleInfoWindow(marker,index)"
        @mouseout="toggleInfoWindow(marker,index)"
        @click="redirect(index)"
      ></gmap-marker>
    </gmap-cluster>
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
  </div>
</template>

<script>
export default {
  name: "GoogleMap",
  props: {
    appData: Object,
  },
  data() {
    return {
      markers: [],
      // center: { lat: 41.5038148, lng: -81.6408804 },
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
  computed: {
    computedMarkers() {
      let myMarkers = [];
      this.appData.breweries.forEach(brewery => {
        const marker = {
          lat: brewery.latitude,
          lng: brewery.longitude,
        };
        myMarkers.push({ position: marker});
      });

      return myMarkers;
    },
    userCenter() {
      if(this.appData.formData.center.lat) {
        return { lat: this.appData.formData.center.lat, lng: this.appData.formData.center.lng }
      }
      return { lat: 41.5038148, lng: -81.6408804 }
    }
  },
  methods: {
    toggleInfoWindow: function(marker, i) {
      this.windowOpen = this.windowOpen ? false : true;
      this.description = this.appData.breweries[i].name;
      this.address = this.appData.breweries[i].address;
      this.windowPosition = marker.position;
      this.beerThumb = this.appData.breweries[i].imgSrc;
    },
    redirect(index) {
      this.$router.push("/brewery/" + this.appData.breweries[index].name);
    },
  },
};
</script>

<style scoped>

#map {
  height: 650px;
}
a, td{
  color:maroon;
  font-weight:bolder;
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
  margin-bottom: 12px;
}
img {
  height: 100px;
  width: 75px;
  align-self: center;
}

a, td {
  color: maroon;
  font-weight: bolder;
}

</style>
