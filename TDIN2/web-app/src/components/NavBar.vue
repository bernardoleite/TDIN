<template>
    <nav>
        <h1 class="logo"><span>Book</span>Shop</h1>
        <div>
            <router-link class="nav-link" to="/books">Books</router-link>
            <router-link  class="nav-link" to="/orders">My Orders</router-link>
        </div>
    </nav>
</template>
    
<script>
    export default {
        name: 'navbar',
    }
</script>
    
<style>
    nav{
        display: flex;
        justify-content: space-between;
        margin-right: 15%;
        margin-left: 15%;
        margin-top: 1em;
        align-items: flex-end;
        
    }
    h1.logo{
        font-weight: lighter;
        font-size: 3em;
        color: #45818e;
        user-select: none;
    }
    h1.logo span{
        font-weight: bold;
    }
    a.nav-link{
        font-size: 1.5em;
        color: #cc0000;
        margin-left:1em;
        text-decoration: none;
        font-weight: bolder;
    }
    a.router-link-active{
        color: #e06666;
        border-bottom:3px solid #e06666
    }



</style>