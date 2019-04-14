<template>
  <section>
    <img :src="`${beer.imgSrc}`" alt="Beer Image">
    <ul>
      <li>{{beer.name}}</li>
      <li>{{beer.style}}</li>
      <li>ABV: {{beer.abv}}</li>
    </ul>
  </section>
</template>

<script>
export default {
  name: "beer-info",
  props: {
    beer: Object
  }
};
</script>

<style>
</style>
