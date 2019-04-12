<template>
  <section>
      <div class="brewery-deet">
      <img src="#" alt="Brewery Image"/>
      <p>{{brewery.Name}}</p>
      <p>{{brewery.happyHourFrom}}  {{brewery.happyHourTo}}</p>
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
  props: {
      brewery: Object
  }
};
</script>

<style>
</style>
