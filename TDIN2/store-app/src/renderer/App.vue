<template>
  <div id="app">
    <nav-bar></nav-bar>
    <router-view></router-view>
  </div>
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
  /* CSS */
</style>
