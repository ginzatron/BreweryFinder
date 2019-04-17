<template>
  <div>
    <div id="favorite">
      <i
        :id="beer.name"
        class="fas fa-beer"
        :class="{favorited:isFavorited}"
        @click="favorite($event)"
      ></i>
    </div>

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
  </div>
</template>

<script>
import auth from "@/shared/auth";
export default {
  name: "beer-info",
  props: {
    beer: Object,
    favorites: Array
  },
  computed: {
    isFavorited() {
      if (this.favorites == undefined) return false;
      return this.favorites.includes(this.beer.id);
    }
  },
  methods: {
    redirect() {
      this.$router.push("/search");
      this.$emit("beerClick", this.beer.name);
    },
    favorite($event) {
      console.log($event.target);
      if (!auth.getToken()) {
        this.$router.push("/login");
      } else if (!$event.target.classList.contains("favorited")) {
        fetch(`${process.env.VUE_APP_REMOTE_API}/beer`, {
          method: "POST",
          headers: {
            Authorization: "Bearer " + auth.getToken(),
            Accept: "application/JSON",
            "Content-Type": "application/JSON"
          },
          body: JSON.stringify({ Id: this.beer.id })
        }).then(response => {
          if (response.ok) {
            this.$emit("reloadFavs");
          }
        });
      } else if ($event.target.classList.contains("favorited")) {
        fetch(`${process.env.VUE_APP_REMOTE_API}/beer`, {
          method: "DELETE",
          headers: {
            Authorization: "Bearer " + auth.getToken(),
            Accept: "application/JSON",
            "Content-Type": "application/JSON"
          },
          body: JSON.stringify({ Id: this.beer.id })
        }).then(response => {
          if (response.ok) {
            this.$emit("reloadFavs");
          }
        });
      }
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
i {
  color: white;
  font-size: 2.25rem;
}

i.favorited {
  color: yellow;
  font-size: 2.25rem;
}
</style>
