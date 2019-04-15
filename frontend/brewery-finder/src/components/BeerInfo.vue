<template>
  <div class="container">
    <div class="beer-card">
      <div class="card-inner">
        <div class="card-front">
          <img class="card" :src="`${beer.imgSrc}`" alt="Beer Image">
        </div>
        <div class="card-back">
          <div class="card">
            <img class="card" :src="`${beer.imgSrc}`">
            <div class="card-text">
            <p>{{beer.name}}</p>
            <p>{{beer.style}}</p>
            <p>ABV: {{beer.abv}} %</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
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
.container {
  height: 300px;
}

.beer-card div {
  /* background-color: transparent; */
  width: 175px;
  perspective: 1000px;
}

.beer-card:hover .card-inner {
  transform: rotateY(180deg);
}

.card-inner {
  position: relative;
  width: 100%;
  height: 100%;
  text-align: center;
  transition: transform 1.5s;
  transform-style: preserve-3d;
}

.card-front img {
  width: 90%;
  height: auto;
}

.card-front,
.card-back {
  position: absolute;
  width: 100%;
  height: 100%;
  backface-visibility: hidden;
}

.card-front {
  /* background-color: #bbb; */
}

.card-back img {
  /* position: absolute; */
  width:90%;
  height: auto;
  visibility:hidden;
}

.card-text {
  position: relative;
}

.card-back {
  /* background-color: #fff; */
  color: black;
  transform: rotateY(180deg);
}
</style>
