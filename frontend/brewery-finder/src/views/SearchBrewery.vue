<template>
  <section>
    <div>
      <form v-on:submit.prevent="loadBreweries">
        <label>Name</label>
        <input type="text" placeholder="Brewery Name" v-model.trim="formData.name">
        <label>Zip Code</label>
        <input type="text" placeholder="Zip Code" v-model.trim="formData.zipCode">
        <label>Radius</label>
        <input type="text" placeholder="Radius in miles" v-model.trim="formData.range">
        <label>Happy Hour</label>
        <input type="text" placeholder="Happy Hour" v-model.trim="formData.happyHour">
        <button type="submit">Submit</button>
      </form>
    </div>
    <div>
      <the-map></the-map>
    </div>
  </section>
</template>

<script>
import TheMap from "@/components/TheMap.vue";
import {EventBus} from "@/shared/event-bus";
export default {
    components:{
        TheMap
    },
  data() {
    return {
      breweries: [],
      formData: {
        name: "",
        zipCode: "",
        happyHour:"",
        range:30
      }
    };
  },

  methods: {
    loadBreweries() {
        EventBus.$emit('zipClick',this.formData);
    },
  }
};
</script>

<style>
  form {
    margin-top: 20px;
    margin-bottom: 20px;
  }

  label{
    color: black;
    font-weight: bold;
  }
</style>
