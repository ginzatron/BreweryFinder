<template>
  <section>
    
    <div class="beer-deet">
      <div class="beer-details-top">
        <img id="beer-img" v-bind:src="`${beer.imgSrc}`" :alt="`${beer.name}` + 'image'">
        <div id="beer-info">
          <div id="beer-name">
            <p>{{beer.name}}</p>
          </div>
         
          <div id=beer-description>
          <p>{{beer.description}}</p>
          </div>
        </div>
      </div>
    </div>
    <h2 class="card">Beer Available at {{brewery[0].name}}</h2>
    <div class="available-breweries">
      <beer-info v-bind:key="beer.id" v-bind:beer="beer" v-for="beer in brewery[0].beersAvailable"></beer-info>
    </div>
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
.beer-details-top {
  display: grid;
  grid-template-columns: 1fr 2fr;
  grid-template-areas: "brewery-image brewery-info";
}

h2 {
  text-decoration: underline;
  font-size: 3rem;
  display: inline-block;
}

#beer-img {
  max-width: 85%;
  height: 350px;
  padding: 40px;
}

#beer-img {
  grid-area: brewery-image;
}

#beer-name{
  font-size: 1.5rem;
  color: #490000;
}

#beer-info {
  grid-area: brewery-info;
  margin: auto 0;
  text-align: left;
  justify-self: left;
  font-weight: bold;
  margin-right: 30px;
  border: 3px solid var(--burgundy);
  border-radius: 10px;
  background-color: rgba(167, 176, 173, 0.9);
  padding: 15px;
}

#beer-description{
  margin-top: 30px;
  font-weight: bold;
}

div.available-breweries {
  display: flex;
  flex-direction: row;
  justify-content: space-around;
}
</style>
