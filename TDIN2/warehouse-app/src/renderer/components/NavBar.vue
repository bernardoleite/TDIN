<template>
    <nav>
        <h1 class="logo"><span>Book</span>Shop [Warehouse]</h1>
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
        font-style: underline;
    }
    h1.logo span{
        font-weight: 800;
    }
</style>