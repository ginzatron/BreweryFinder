<template>
  <section>
    <div>
      <form v-on:submit="loadBreweries">
          <label>Name</label>
          <input type="text" placeholder="Brewery Name" v-model="formData.name"/>
          <label>Zip Code</label>
          <input type="text" placeholder="Zip Code" v-model="formData.zipCode"/>
          <button type="submit">Submit</button>
      </form>
    </div>
    <div>
      <the-map></the-map>
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Happy Hour(s)</th>
            <th>Type</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="brewery in breweries" v-bind:key="brewery.id">
            <td>{{brewery.name}}</td>
            <td>{{brewery.happyHourFrom}} to {{brewery.happyHourTo}}</td>
            <td>{{brewery.isBar}} {{brewery.isBrewery}}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </section>
</template>

<script>
import TheMap from "@/components/TheMap.vue";
export default {
  data() {
    return {
      breweries: [],
      formData:{
          name:"",
          zipCode:""
      }
    };
  },

  methods: {
    loadBreweries() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/brewery?zip=${this.formData.zipCode}`, {
        method: "GET"
      })
        .then(response => response.json())
        .then(json => (this.breweries = json));
    }
  }
};
</script>

<style>
</style>
