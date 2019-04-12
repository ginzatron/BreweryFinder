<template>
  <section>
    <div class="brewery-deet">
      <img src="#" alt="Brewery Image">
      <p>{{brewery.Name}}</p>
      <p>{{brewery.happyHourFrom}} {{brewery.happyHourTo}}</p>
      <p>{{brewery.established}}</p>
      <p>{{brewery.address}} {{brewery.city}} {{brewery.state}} {{brewery.zip}}</p>
      <p>{{brewery.description}}</p>
    </div>
    <div class="available-beers">
      <beer-info v-bind:key="beer.id" v-bind:beer="beer" v-for="beer in brewery.beersAvailable"></beer-info>
    </div>
  </section>
</template>

<script>
import BeerInfo from "@/components/BeerInfo.vue";

export default {
  data() {
    return {
      brewery: {}
    }
  },
  created() {
    const breweryId = this.$route.params.id;
      fetch(
        `${process.env.VUE_APP_REMOTE_API}/brewery/id?brewId=${
          breweryId
        }`,
        {
          method: "GET"
        }
      )
        .then(response => response.json())
        .then(json => (this.brewery = json));
  }
};
</script>

<style>
</style>
