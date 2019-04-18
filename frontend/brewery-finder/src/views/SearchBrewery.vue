<template>
  <section>
    <div class="form-results">
      <form class="card" v-on:submit.prevent="loadBreweries">
        <div>
          <label>Name</label>
          <input type="text" placeholder="Brewery Name" v-model.trim="appData.formData.name">
        </div>
        <div>
          <label>Zip Code</label>
          <input type="radio" value="true" v-model="doZip">
          <input type="text" placeholder="Zipcode" v-model.trim="appData.formData.zipCode" v-bind:disabled="!computedZip">
        </div>
        <div>
          <label>Radius</label>
          <input type="radio" value="false" v-model="doZip">
          <select v-model.trim="appData.formData.range" v-bind:disabled="computedZip">
            <option value></option>
            <option value="5">5 miles</option>
            <option value="10">10 miles</option>
            <option value="15">15 miles</option>
            <option value="25" selected="selected">25 miles</option>
            <option value="50">50 miles</option>
          </select>
        </div>
        <div>
          <label>Happy Hour</label>
          <select v-model.trim="appData.formData.happyHour">
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
          <input type="text" placeholder="Beer Name" v-model.trim="appData.formData.beerName">
        </div>
        <div class="form-buttons">
          <button type="submit">Submit</button>
          <button type="button" @click.prevent="clearForm">Clear</button>
        </div>
      </form>
      <results v-bind:breweries="appData.breweries"></results>
    </div>
    <div id="map">
      <the-map v-bind:appData="appData"></the-map>
    </div>
  </section>
</template>

<script>
import TheMap from "@/components/TheMap.vue";
import Results from "@/components/Results.vue";

export default {
  components: {
    TheMap,
    Results
  },
  props: {
    appData: Object
  },
  data() {
    return {
      doZip: "false"
    }
  },
  methods: {
    loadBreweries() {
      this.$emit("formSubmit", this.appData);
    },
    timeFormat(a, b) {
      let timeA = a.split(":").shift();
      let timeB = b.split(":").shift();
      if (timeA > 12) return `${timeA - 12} pm - ${timeB - 12} pm`;
      else if (timeA > 0 && timeB < 12) return `${timeA} am - ${timeB} am`;
      else if (timeA == 0) return "nope";
    },
    clearForm() {
      this.$emit("clearForm");
    }
  },
  computed: {
    computedZip() {
      return this.doZip === "true";
    }
  }
};
</script>

<style scoped>
section {
  display: flex;
  flex-direction: row;
  justify-content: space-evenly;
}

.form-results {
  display: flex;
  flex-direction: column;
  min-width: 420px;
}

form {
  display: flex;
  flex-direction: column;
  align-items: space-between;
  margin: 20px 0px;
}

.results {
  width: 100%;
}

#map {
  width: 100%;
  padding: 10px;
}

label {
  color: black;
  font-weight: bold;
  text-align: left;
  min-width: 100px;
}

input {
  width: 65%;
  min-width: 190px;
  margin-bottom: 12px;
}

input[type='radio'] {
  min-width: 10px;
  width: 10px;
}

select {
  width: 66%;
  min-width: 194px;
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
  margin: 0 40px;
}

/*change style at < 800px */
</style>