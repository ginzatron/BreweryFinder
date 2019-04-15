<template>
  <section>
    <div class="brewery-deet">
      <div class="brewery-details-top">
      <div id="brewery-img">
       <img v-bind:src="`${brewery.imgSrc}`" :alt="`${brewery.Name}` + 'image'">
       </div>
      <div id="brewery-info">

      <p>{{brewery.Name}}</p>
      <p>Happy Hour(s) {{brewery.happyHourFrom}} {{brewery.happyHourTo}}</p>
      <p>Established: {{brewery.established}}</p>
      <p>{{brewery.address}} {{brewery.city}} {{brewery.state}} {{brewery.zip}}</p>
      </div>
      </div>

      
      <div class="brewery-description">
      <p>{{brewery.description}}</p>
      </div>
    </div>
    <div class="available-beers">
      <beer-info v-bind:key="beer.id" v-bind:beer="beer" v-for="beer in brewery.beersAvailable"></beer-info>
    </div>
  </section>
</template>

<script>
import BeerInfo from "@/components/BeerInfo.vue";

export default {
  components: {
    BeerInfo,
  },
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

.brewery-details-top {
  
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;
    justify-content: space-around;
    align-content: flex-start;
    align-items: center;
    margin-top:30px;
    margin-bottom:30px;
    }

#brewery-image{
    order: 0;
    flex: 0 1 auto;
    align-self: auto;
    }

#brewery-info{
    order: 0;
    flex: 0 1 auto;
    align-self: auto;
    justify-content:left;
    text-align:left;
    background-color:aqua
    }

div.available-beers{
      display:flex;
      flex-direction: row;
      justify-content:space-around;
    }
    

</style>
