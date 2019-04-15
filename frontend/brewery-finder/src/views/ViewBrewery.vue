<template>
  <section>
    <div class="brewery-deet">
      <div class="brewery-details-top">
        <img id="brewery-img" v-bind:src="`${brewery.imgSrc}`" :alt="`${brewery.Name}` + 'image'">
        <div id="brewery-info">
          <div id="brewery-name">
            <p>{{brewery.name}}</p>
          </div>
          <p>Happy Hour(s) {{brewery.happyHourFrom}} {{brewery.happyHourTo}}</p>
          <p>Established: {{brewery.established}}</p>
          <p>{{brewery.address}}</p>
          <p>{{brewery.city}} {{brewery.state}} {{brewery.zip}}</p>
          <p></p>
          <div id=brewery-description>
          <p>{{brewery.description}}</p>
          </div>
        </div>
      </div>
    </div>
    <h2>Beer Available at {{brewery.name}}</h2>
    <div class="available-beers">
      <beer-info v-bind:key="beer.id" v-bind:beer="beer" v-for="beer in brewery.beersAvailable"></beer-info>
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
      brewery: {}
    };
  },
  created() {
    const breweryId = this.$route.params.id;
    fetch(`${process.env.VUE_APP_REMOTE_API}/brewery/id?brewId=${breweryId}`, {
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
  color: var(--burgundy);
  text-decoration: underline;
  font-size: 3rem;
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