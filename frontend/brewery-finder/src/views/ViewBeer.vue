<template>
  <section>
    <div class="beer-deet">
      <div class="beer-details-top">
        <img id="beer-img" v-bind:src="`${thisBeer.ImgSrc}`" :alt="`${thisBeer.Name}` + 'image'">
        <div id="beer-info">
          <div id="beer-name">
            <p>{{thisBeer.Name}}</p>
          </div>
          <p>Style: {{thisBeer.Style}}</p>
          <p>ABV: {{thisBeer.Abv}}</p>
          <div id=beer-description>
          <p>{{thisBeer.Description}}</p>
          </div>
        </div>
      </div>
    </div>
    <h2 class="card">This Beer Available At</h2>
    <div class="available-breweries">
      <brewery-info v-bind:key="brewery.id" v-bind:brewery="brewery" v-for="brewery in breweries"></brewery-info>
    </div>
  </section>

</template>

<script>
import BreweryInfo from "@/components/BreweryInfo.vue";

export default {
  components: {
    BreweryInfo,
  },
  data() {
    return {
      breweries: []
    };
  },
  computed: {
    thisBeer() {
      return breweries[0].beersAvailable.filter(value => value.name = this.$route.params.name);
    }
  },
  methods: {},
  created() {
    const beerName = this.$route.params.name;
    fetch(`${process.env.vue_app_remote_api}/brewery?beerName=${beerName}`, {
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
