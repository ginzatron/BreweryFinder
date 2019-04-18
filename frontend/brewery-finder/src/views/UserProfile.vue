<template>
  <section>
    <h2 class="card">{{appData.username}}'s Favorites</h2>
    <div class="favorites">
      <beer-info
        v-for="beer in beerList"
        :beer="beer"
        :key="beer.id"
        :favorites="appData.favorites"
        @beerClick="handleBeerClick"
        @reloadFavs="reloadFavs"
      ></beer-info>
    </div>
  </section>
</template>

<script>
import BeerInfo from "@/components/BeerInfo.vue";

export default {
  props: {
    appData: Object
  },
  components: {
    BeerInfo
  },
  computed: {
    beerList() {
      let beers = [];
      this.appData.breweries.forEach(brewery => {
        brewery.beersAvailable.forEach(beer => {
          if (this.appData.favorites.includes(beer.id)) beers.push(beer);
        });
      });
      return beers;
    }
  },
  methods: {
    handleBeerClick(beerName) {
      this.$emit("beerChosen", beerName);
    },
    reloadFavs() {
      this.$emit("reloadFavs");
    }
  }
};
</script>

<style>
.favorites {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: space-around;
}
</style>
