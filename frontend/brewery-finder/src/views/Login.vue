<template>
  <div>
    <form v-if="showLoginForm" v-on:submit.prevent="login">
      <h2>Login</h2>
      <a v-on:click="toggleForm">Register an Account</a>
      <div>
        <label for="username">Username:</label>
        <input type="text" name="username" v-model.trim="this.loginForm.username" />
      </div>
      <div>
        <label for="password">Password:</label>
        <input type="password" name="password" v-model.trim="this.loginForm.password" />
      </div>
      <input type="submit" value="Submit" />
    </form>
    <form v-else v-on:submit.prevent="register">
      <h2>Register</h2>
      <a v-on:click="toggleForm">Back to Login</a>
      <div>
        <label for="username">Username:</label>
        <input type="text" name="username" v-model.trim="this.signupForm.username" />
      </div>
      <div>
        <label for="password">Password:</label>
        <input type="password" name="password" v-model.trim="this.signupForm.password" />
      </div>
      <div>
        <label for="confirmPassword">Confirm Password:</label>
        <input type="password" name="confirmPassword" v-model.trim="this.signupForm.confirmPassword" />
      </div>
      <input type="submit" value="Submit" />
    </form>
  </div>
</template>

<script>
import auth from "@/shared/auth";

export default {
  name: "login",
  data() {
    return {
      showLoginForm: true,
      error: "",
      /** Contents of the login form */
      loginForm: {
        username: "",
        password: ""
      },
      /** Contents of the sign up form */
      signupForm: {
        password: "",
        username: "",
        confirmPassword: "",
        role: "user"
      }
    };
  },
  methods: {
    toggleForm() {
      this.showLoginForm = !this.showLoginForm;
      this.error = "";
    },
    goHome() {
      this.$router.push("/");
    },
    async login() {
      this.error = "";

      /**
       * This example uses async/await over Promise .then()
       * Under the hood it still relies on promises but the syntax of
       * await Promise makes it a bit shorter and easier to read.
       * It also helps with one issue which is the need to conditionally
       * run logic based on the response.statusCode.
       */
      try {
        const url = `${process.env.VUE_APP_REMOTE_API}/account/login`;
        const response = await fetch(url, {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json"
          },
          body: JSON.stringify(this.loginForm)
        });

        if (response.status === 401) {
          this.error = "Your username and/or password is invalid";
          this.loginForm.password = "";
        } else {
          // Parse output and save authentication token
          const token = await response.json();
          auth.saveToken(token);
          this.goHome();
        }
      } catch (error) {
        console.error(error);
        this.error = "There was an error logging in";
      }
    },
    /**
     * Signs the user up and then redirects them to the dashboard.
     */
    async signup() {
      this.error = "";

      try {
        const url = `${process.env.VUE_APP_REMOTE_API}/account/register`;
        const response = await fetch(url, {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json"
          },
          body: JSON.stringify(this.signupForm)
        });

        // We definitely need the response if success or not.
        const data = await response.json();

        if (response.status === 400) {
          this.error = data.message;
        } else {
          auth.saveToken(data);
          this.goHome();
        }
      } catch (error) {
        console.error(error);
        this.error = "There was an error attempting to register";
      }
    },
  }
};
</script>

<style scoped>
form {
  display: flex;
  flex-flow: column;
  align-items: center;
}

input[type="submit"] {
  width: 100px;
  text-align: center;
}

input {
  margin-bottom: 10px;
}

a {
    cursor: pointer;
}
</style>
