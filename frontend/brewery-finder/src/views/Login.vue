<template>
  <section>
    <div class="form-div"  v-if="showLoginForm">
      <form v-on:submit.prevent="login">
        <h2>Login</h2>
        <a class="button" v-on:click="toggleForm">Register an Account</a>
        <div>
          <label for="username">Username:</label>
          <input type="text" name="username" v-model.trim="loginForm.username">
        </div>
        <div>
          <label for="password">Password:</label>
          <input type="password" name="password" v-model.trim="loginForm.password">
        </div>
        <input type="submit" value="Submit">
      </form>
    </div>
    <div class="form-div"  v-if="!showLoginForm">
      <form v-on:submit.prevent="register">
        <h2>Register</h2>
        <a class="button" v-on:click="toggleForm">Back to Login</a>
        <div>
          <label for="username">Username:</label>
          <input type="text" name="username" v-model.trim="signupForm.username">
        </div>
        <div>
          <label for="password">Password:</label>
          <input type="password" name="password" v-model.trim="signupForm.password">
        </div>
        <div>
          <label for="confirmPassword">Confirm Password:</label>
          <input type="password" name="confirmPassword" v-model.trim="signupForm.confirmPassword">
        </div>
        <input type="submit" value="Submit">
      </form>
    </div>
  </section>
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
          this.$emit("login", auth.getUser().sub);
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
    async register() {
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
          this.$emit("login", auth.getUser().sub);
          this.goHome();
        }
      } catch (error) {
        console.error(error);
        this.error = "There was an error attempting to register";
      }
    }
  }
};
</script>

<style scoped>
form {
  display: flex;
  flex-flow: column;
  align-items: center;
  color: black;
}

section {
  display: flex;
  justify-content:center;
}

input[type="text"], input[type="password"] {
  margin-right: 25px;
}

input[name="confirmPassword"] {
  margin-right: 90px;
}

.form-div {
  margin: 60px;
  background-color: rgba(167, 176, 173, 0.6);
  width: 35%;
  height: 225px;
  
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
  margin-bottom: 10px;
}

h2 {
  margin: 10px 0;
}

.button {
   border-top: 1px solid #96d1f8;
   background: #65a9d7;
   /* background: -webkit-gradient(linear, left top, left bottom, from(#3e779d), to(#65a9d7));
   background: -webkit-linear-gradient(top, #3e779d, #65a9d7);
   background: -moz-linear-gradient(top, #3e779d, #65a9d7);
   background: -ms-linear-gradient(top, #3e779d, #65a9d7);
   background: -o-linear-gradient(top, #3e779d, #65a9d7); */
   padding: 5px 10px;
   -webkit-border-radius: 10px;
   -moz-border-radius: 10px;
   border-radius: 10px;
   -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
   -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
   box-shadow: rgba(0,0,0,1) 0 1px 0;
   text-shadow: rgba(0,0,0,.4) 0 1px 0;
   color: white;
   font-size: 16px;
   font-family: Georgia, serif;
   text-decoration: none;
   vertical-align: middle;
   }
.button:hover {
   border-top-color: #28597a;
   background: #28597a;
   color: #ccc;
   }
.button:active {
   border-top-color: #1b435e;
   background: #1b435e;
   }
</style>
