<template>
     <table class="card" v-if="breweries">
      <thead>
        <tr>
          <th>Name</th>
          <th>Happy Hour(s)</th>
          <th>Type</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="brewery in breweries" v-bind:key="brewery.id">
          <td>
            <router-link
              v-bind:to="{name: 'view-brewery', params:{id: brewery.id}}"
            >{{brewery.name}}</router-link>
          </td>
          <td>{{timeFormat(brewery.happyHourFrom,brewery.happyHourTo)}}</td>
          <td>{{barRestaurant(brewery.isBar,brewery.isBrewery)}}</td>
        </tr>
      </tbody>
    </table>
</template>

<script>
export default {
  props: {
    breweries: Array
  },
  methods: {
     timeFormat(a,b) {
      let timeA = a.split(":").shift();
      let timeB = b.split(":").shift();
      if (timeA > 12) return (`${timeA-12} pm - ${timeB-12} pm`);
      else if (timeA > 0 && timeB <12) return (`${timeA} am - ${timeB} am`);
      else if (timeA == 0) return "nope";
    },
    barRestaurant(a,b){
      if (a && b) return ("Bar/Brewery");
      else if (a && !b) return ("Bar");
      return ("Brewery");
    }
  }
}
</script>

<style>

</style>
