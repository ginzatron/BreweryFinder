<template>
  <div class="container" @click="redirect(beer.name)">
    <div class="beer-card">
      <div class="card-inner">
        <div class="card-front">
          <div class="card">
            <img :src="`${beer.imgSrc}`" alt="Beer Image">
          </div>
        </div>
        <div class="card-back">
          <div class="card">
            <!-- <img class="card" :src="`${beer.imgSrc}`"> -->
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
import { EventBus } from "@/shared/event-bus";

export default {
  name: "beer-info",
  props: {
    beer: Object
  },
  methods: {
    redirect(beerName) {
      this.$router.push("/search");
      EventBus.$emit("beerClick", this.beer.name);
      console.log(this.beer.name);
    }
  }
};
</script>

<style scoped>
.container {
  height: 300px;
}

.beer-card div {
  height: 260px;
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
  width: 150px;
  height: 245px;
}

.card-front,
.card-back {
  position: absolute;
  width: 100%;
  height: 100%;
  backface-visibility: hidden;
}

.card-text {
  position: relative;
  color: var(--burgundy);
  font-weight: bolder;
}

.card-back {
  /* background-color: #fff; */
  color: black;
  transform: rotateY(180deg);
}
</style>
