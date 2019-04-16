<template>
  <section>
    <form class="card" v-on:submit.prevent="loadBreweries">
      <div>
        <label>Name</label>
        <input type="text" placeholder="Brewery Name" v-model.trim="formData.name">
      </div>
      <div>
        <label>Zip Code</label>
        <input type="text" placeholder="Zip Code" v-model.trim="formData.zipCode">
      </div>
      <div>
        <label>Radius</label>
        <input type="text" placeholder="Radius in miles" v-model.trim="formData.range">
      </div>
      <div>
        <label>Happy Hour</label>
        <select v-model.trim="formData.happyHour">
          <option value></option>
          <option value="11:00">11:00 am</option>
          <option value="11:30">11:30 am</option>
          <option value="12:00">12:00 pm</option>
          <option value="12:30">12:30 pm</option>
          <option value="13:00">1:00 pm</option>
          <option value="13:30">1:30 pm</option>
          <option value="14:00">2:00 pm</option>
          <option value="14:30">2:30 pm</option>
          <option value="15:00">3:00 pm</option>
          <option value="15:30">3:30 pm</option>
          <option value="16:00">4:00 pm</option>
          <option value="16:30">4:30 pm</option>
          <option value="17:00">5:00 pm</option>
          <option value="17:30">5:30 pm</option>
          <option value="18:00">6:00 pm</option>
          <option value="18:30">6:30 pm</option>
          <option value="19:00">7:00 pm</option>
        </select>
      </div>
      <div>
        <label>Beer Name</label>
        <input type="text" placeholder="Beer Name" v-model.trim="formData.beerName">
      </div>
      <button type="submit">Submit</button>
    </form>
    <div class="map-results">
       <div class="results">
        <results v-bind:breweries="breweries"></results>
      </div>
      <div id="map">
        <the-map></the-map>
      </div>
    </div>
  </section>
</template>

<script>
import TheMap from "@/components/TheMap.vue";
import { EventBus } from "@/shared/event-bus";
import Results from "@/components/Results.vue";
export default {
  components: {
    TheMap,
    Results
  },
  data() {
    return {
      breweries: [],
      formData: {
        name: "",
        zipCode: "",
        happyHour: "",
        range: 30,
        beerName: ""
      }
    };
  },
  methods: {
    loadBreweries() {
      this.getSomeBreweries();
      EventBus.$emit("zipClick", this.formData);
    },
    timeFormat(a, b) {
      let timeA = a.split(":").shift();
      let timeB = b.split(":").shift();
      if (timeA > 12) return `${timeA - 12} pm - ${timeB - 12} pm`;
      else if (timeA > 0 && timeB < 12) return `${timeA} am - ${timeB} am`;
      else if (timeA == 0) return "nope";
    },
    getSomeBreweries() {
      fetch(
        `${process.env.VUE_APP_REMOTE_API}/brewery?zip=${
          this.formData.zipCode
        }&name=${this.formData.name}&happyHour=${
          this.formData.happyHour
        }&beerName=${this.formData.beerName}`
      )
        .then(response => {
          return response.json();
        })
        .then(json => {
          this.breweries = json;
        })
        .catch(error => console.error(error));
    }
  }
};
</script>

<style scoped>
form {
  display: flex;
  flex-direction: column;
  margin-bottom: 20px;
  align-items: space-between;
  width: 35%;
  margin-top: 10px;
}

.map-results {
  display: flex;
  flex-direction: row;
  justify-content: space-evenly;
}

#map {
  height: 400px;
  width: 800px;
}

section div {
  display: flex;
  flex-direction: column;
  align-items: center;
}
label {
  color: black;
  font-weight: bold;
}
input,
select {
  width: 300px;
  margin-bottom: 12px;
}
form div {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
button {
  margin-top: 12px;
  width: 99px;
  margin-left: 36%;
}
</style>
