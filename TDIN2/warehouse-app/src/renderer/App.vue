<template>
    <v-app id="app">
        <nav-bar></nav-bar>
        <requests></requests>
    </v-app>
</template>

<script>
    import NavBar from './components/NavBar'
    import Requests from './components/Requests'

    export default {
        name: 'warehouse-app',
        components: {
        'nav-bar':NavBar,
        'requests':Requests
        },
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
  
  