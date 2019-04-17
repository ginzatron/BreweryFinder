<template>
  <section>
    <div class="brewery-deet">
      <div class="brewery-details-top">
        <img id="brewery-img" v-bind:src="`${brewery[0].imgSrc}`" :alt="`${brewery[0].name}` + 'image'">
        <div id="brewery-info">
          <div id="brewery-name">
            <p>{{brewery[0].name}}</p>
          </div>
          <p>Happy Hour(s) {{timeFormat(brewery[0].happyHourFrom,brewery[0].happyHourTo)}}</p>
          <p>Established: {{brewery[0].established}}</p>
          <p>{{brewery[0].address}}</p>
          <p>{{brewery[0].city}} {{brewery[0].state}} {{brewery[0].zip}}</p>
          <p></p>
          <div id=brewery-description>
          <p>{{brewery[0].description}}</p>
          </div>
        </div>
      </div>
    </div>
    <h2 class="card">Beer Available at {{brewery[0].name}}</h2>
    <div class="available-beers">
      <beer-info v-bind:key="beer.id" v-bind:beer="beer" v-for="beer in brewery[0].beersAvailable" @beerClick="handleBeerClick"></beer-info>
    </div>
  </section>
</template>

<script>
import BeerInfo from "@/components/BeerInfo.vue";

export default {
  components: {
    BeerInfo
  },
  data() {
    return {
      brewery: [{name:'',imgSrc:'',happyHourFrom:'',happyHourTo:''}]
    };
  },
  methods: {
    timeFormat(a,b) {
      let timeA = a.split(":").shift();
      let timeB = b.split(":").shift();
      if (timeA > 12) return (`${timeA-12} pm - ${timeB-12} pm`);
      else if (timeA > 0 && timeB <12) return (`${timeA} am - ${timeB} am`);
      else if (timeA == 0) return "nope";
    },
    handleBeerClick(beerName) {
      this.$emit("beerChosen", beerName);
    }
  },
  created() {
    const breweryName = this.$route.params.name;
    fetch(`${process.env.VUE_APP_REMOTE_API}/brewery?name=${breweryName}`, {
      method: "GET"
    })
      .then(response => response.json())
      .then(json => (this.brewery = json));
  }
};
</script>

<style>
.brewery-details-top {
  display: grid;
  grid-template-columns: 1fr 2fr;
  grid-template-areas: "brewery-image brewery-info";
}

h2 {
  text-decoration: underline;
  font-size: 3rem;
  display: inline-block;
}

#brewery-img {
  max-width: 85%;
  height: 350px;
  padding: 40px;
}

#brewery-img {
  grid-area: brewery-image;
}

#brewery-name{
  font-size: 1.5rem;
  color: #490000;
}

#brewery-info {
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

#brewery-description{
  margin-top: 30px;
  font-weight: bold;
}

div.available-beers {
  display: flex;
  flex-direction: row;
  justify-content: space-around;
}
</style>