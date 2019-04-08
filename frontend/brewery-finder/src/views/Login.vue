<template>
      <div>
    <h2>Login</h2>
    <form action>
        <label for="username">Username:</label>
      <input type="text" name="username" />

      <label for="password">Password:</label>
      <input type="text" name="password" />

      <label for="confirm">Confirm Password:</label>
      <input type="text" name="confirm" />
      <input type="submit" value="Submit" />
    </form>
  </div>

</template>

<script>
export default {
    name: "login"
}
</script>

<style>

</style>
