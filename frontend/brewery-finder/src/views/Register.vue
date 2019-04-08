<template>
  <div>
    <h2>Registration</h2>
    <form action>
      <div>
        <label for="username">Username:</label>
        <input type="text" name="username">
      </div>
      <div>
        <label for="password">Password:</label>
        <input type="text" name="password">
      </div>
      <div>
        <label for="confirm">Confirm Password:</label>
        <input type="text" name="confirm">
      </div>
      <input type="submit" value="Submit">
    </form>
  </div>
</template>

<script>
export default {
  name: "register",
  data() {
    return {};
  },
  computed: {}
};
</script>

<style scoped>
form {
  display: flex;
  flex-flow: column;
}

input[type=submit] {
    width: 100px;
    text-align: center;
}

input {
    margin-bottom: 10px;
}
</style>
