<template>
  <section>
    <div>
      <form v-on:submit.prevent="loadBreweries">
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
          <option value=""></option>
          <option value="">11:00 am</option>
          <option value="">11:30 am</option>
          <option value="">12:00 pm</option>
          <option value="">12:30 pm</option>
          <option value="">1:00 pm</option>
          <option value="">1:30 pm</option>
          <option value="">2:00 pm</option>
          <option value="">2:30 pm</option>
          <option value="">3:00 pm</option>
          <option value="">3:30 pm</option>
          <option value="">4:00 pm</option>
          <option value="">4:30 pm</option>
          <option value="">5:00 pm</option>
          <option value="">5:30 pm</option>
          <option value="">6:00 pm</option>
          <option value="">6:30 pm</option>
          <option value="">7:00 pm</option>
        </select>
        </div>
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

<style scoped>
  form {
    display: flex;
    flex-direction: column;
    margin-bottom: 20px;
    align-items: space-between;
    width: 35%;
    border: 1px solid black;
    padding: 8px;
    margin-top: 10px;
    border-radius: 10px;
    background-color: rgba(167,176,173,0.75);
  }

  section div {
    display: flex;
    flex-direction: column;
    align-items: center;
  }
  label{
    color: black;
    font-weight: bold;
  }
  input, select {
    width: 300px;
    margin-bottom:12px;
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
