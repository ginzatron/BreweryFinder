<template>
  <div>
    <div>
      <h2>Search and add a pin</h2>
      <label>
        <gmap-autocomplete
          @place_changed="setPlace">
        </gmap-autocomplete>
        <button @click="addMarker">Add</button>
      </label>
      <br/>

    </div>
    <br>
    <gmap-map
      :center="center"
      :zoom="12"
      style="width:100%;  height: 400px;"
    >
      <gmap-marker v-for="(m, index) in markers" :key="index" :position="m.position" @click="center=m.position"
      ></gmap-marker>
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
      breweries: [
        {
            "name": "Sibling Revelry",
            "address": "29305 Clemens Rd, Westlake, OH 44145",
            "lat": "41.4700513",
            "lng": "-81.94677650000001"
        },
    
        {
            "name": "Brandon Ties One on",
            "address" : "25032 Maidstone Ln, Beachwood, OH 44122"
        }
    ],
      center: { lat: 41.5038148, lng: -81.6408804 },
      markers: [
          {
              position: 
              {
                lat: 41.4700513,
                lng:-81.94677650000001
              }
          },
          {
              position:
              {
                lat: 41.5008803,
                lng: -81.5926564
              }
          }
      ],
      places: [],
      currentPlace: null,
    };
  },

  mounted() {
    this.geolocate();
  },

  created() {
      fetch("breweries.json"
      ).then((response) => {
          return response.json();
      }).then((json) => {
          console.table(json);
          this.breweries = json;
      }).catch((error => console.error(error)));
    },

  methods: {
    // receives a place object via the autocomplete component
    setPlace(place) {
      this.currentPlace = place;
    },
    addMarker() {
      if (this.currentPlace) {
        const marker = {
          lat: this.currentPlace.geometry.location.lat(),
          lng: this.currentPlace.geometry.location.lng()
        };
        this.markers.push({ position: marker });
        this.places.push(this.currentPlace);
        this.center = marker;
        this.currentPlace = null;
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
  }
};
</script>