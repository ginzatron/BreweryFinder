<template>
  <section class="container">
    <div class="beer-card">
      <div class="card-inner">
        <div class="card-front">
          <img :src="`${beer.imgSrc}`" alt="Beer Image">
        </div>
        <div class="card-back">
            <p>{{beer.name}}</p>
            <p>{{beer.style}}</p>
            <p>ABV: {{beer.abv}} %</p>
        </div>
      </div>
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

.container {
  height: 300px;
}

.beer-card div {
  background-color: transparent;
  width: 175px;
  border: 1px solid #f1f1f1;
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
  transition: transform 0.8s;
  transform-style: preserve-3d;
}

.card-front img {
  width: 90%;
  height: auto;
}

/* .card-front li {
  list-style: none;
  color: var(--burgundy);
  font-family: archivo;
  font-weight: bold;
  text-align: left;
} */

.card-front, .card-back {
  position: absolute;
  width: 100%;
  height: 100%;
  backface-visibility: hidden;
}

.card-front {
  background-color: #bbb;
  z-index: 1;
}

.card-back {
  background-color: #bbb;
  color: black;
  transform: rotateY(180deg);
  z-index: 2;
}

/* .beer-info div{
  width:175px;
  margin: 5px 0px;
  text-align: center;
  border: 3px solid var(--darkGrey);
  border-radius: 10%;
} */

</style>
