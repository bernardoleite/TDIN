<template>
  <v-app id="app">
    <nav-bar></nav-bar>
    <router-view></router-view>
  </v-app>
</template>

<script>
  import NavBar from './components/NavBar'

  export default {
    name: 'store-app',
    components: {
      'nav-bar':NavBar
    }
  }
</script>

<style>
  @import url('https://fonts.googleapis.com/css?family=Nunito&display=swap');
  body{
    background-color: #f3f3f3ff;
  }
  .application{
    font-family: 'Nunito', sans-serif !important;
  }
</style>
