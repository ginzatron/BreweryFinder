<template>
  <section class="beer-info">
    <div>
    <img class="beer-img" :src="`${beer.imgSrc}`" alt="Beer Image">
    <ul>
      <li>{{beer.name}}</li>
      <li>{{beer.style}}</li>
      <li>ABV: {{beer.abv}} %</li>
    </ul>
    </div>
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

<style scoped>
.beer-info div{
  width:175px;
  align-self: center;
  text-align:justify;
}

.beer-img{
  width: 100%;
  height: auto;
  margin-left: 10px;
  }

.beer-info ul li{
  list-style: none;
  color: var(--burgundy);
  font-family: archivo;
  font-weight: bold;
  }

</style>
