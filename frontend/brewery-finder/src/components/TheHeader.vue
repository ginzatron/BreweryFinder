<template>
  <header>
    <div>
      <img src="#" alt="Header Image">
      <h1>Brewery Finder</h1>
    </div>
    <nav>
      <router-link to="/">SEARCH</router-link>
      <div>
        <router-link to="/login">LOGIN / REGISTER</router-link>
      </div>
    </nav>
  </header>
</template>

<script>
export default {};
</script>

<style scoped>
header {
  background-color: lightblue;
  border-bottom: 3px solid #777;
  height: 200px;
}

div {
  display: flex;
}

h1 {
  display: inline-block;
  font-size: 7rem;
  margin-top: 0;
  margin-bottom: 0;
}

img {
  display: inline-block;
  width: 150px;
  height: 150px;
}

nav {
  display: flex;
  justify-content: space-evenly;
  font-size: 3rem;
  background-color: lightgreen;
}
</style>
