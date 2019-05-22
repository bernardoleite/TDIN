<template>
  <v-app id="app">
    <nav-bar v-if="isAuth()""></nav-bar>
    <router-view></router-view>
  </v-app>
</template>

<script>
  import NavBar from './components/NavBar'

  export default {
    name: 'web-app',
    components: {
      'nav-bar':NavBar
    },
    created () {
      if (!this.$session.exists()) {
        this.$router.push('/')
      }
    },
    methods: {
      isAuth() {
        return this.$session.exists() ;
      }
    },
  }
</script>

<style>
  @import url('https://fonts.googleapis.com/css?family=Nunito&display=swap');
  body{
    background-color: #f3f3f3ff;
  }
  .application{
    font-family: 'Nunito', sans-serif;
  }
</style>

