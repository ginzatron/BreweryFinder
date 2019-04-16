<template>
  <section>
    <div class="beer info">
    <img id ="beer-img" v-bind:src="`${beer.imgSrc}`" :alt="`${beer.name}` + 'image'">

    <p>{{beer.name}}</p>

    <p>{{beer.style}}</p>
    <p>{{beer.description}}
    <p>{{beer.abv}}</p>
    
    
    <p>Favorite</p>
    </div>
    <the-map></the-map>

    <table>
      <thead>
        <tr>
          <th>Name</th>
          <th>Address</th>
          <th>Happy Hours</th>
          <th>Bar or Brewery</th>
        </tr>
      </thead>

      <tbody>
        <brewery-info v-for="brewery in breweries" v-bind:key="brewery.id" v-bind:brewery="brewery"></brewery-info>
      </tbody>
    </table>
  </section>
</template>

<script>
import TheMap from "@/components/TheMap.vue";
export default {
  components: {
    TheMap
  },
  data() {
    return {
      breweries: []
    };
  },
  computed: {},
  methods: {},
  created() {
    const id = this.$route.params.id;
    fetch(`${process.env.vue_app_remote_api}/beer/${id}`, {
      method: "get"
    })
      .then(response => response.json())
      .then(json => {
        this.breweries = json;
      });
  }
};
</script>

<style>
</style>
