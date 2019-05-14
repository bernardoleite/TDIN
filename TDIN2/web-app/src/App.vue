<template>
  <div id="app">
    <nav-bar></nav-bar>
    <router-view></router-view>
  </div>
</template>

<script>
  import NavBar from './components/NavBar'

  export default {
    name: 'web-app',
    components: {
      'nav-bar':NavBar
    }
  }
</script>

<style>
  @import url('https://fonts.googleapis.com/css?family=Nunito');
  body{
    font-family: 'Nunito', sans-serif;
  }
</style>

