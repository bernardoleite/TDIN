<template>
    <div>
        <div class="title-wrapper">
            <hr>
            <h1>Books</h1>
            <hr>
        </div>
        <div class="table-wrapper">
            <v-text-field
            class="search"
            v-model="search"
            append-icon="search"
            label="Search"
            single-line
            hide-details
            ></v-text-field>
            <v-data-table
                v-model="selected" :headers="headers" :items="books" :search="search" item-key="id" select-all hide-actions class="elevation-1">
                <template v-slot:items="props">
                    <td>
                        <v-checkbox
                        v-model="props.selected"
                        primary
                        hide-details
                        ></v-checkbox>
                    </td>
                    <td>{{ props.item.id }}</td>
                    <td class="text-xs-right">{{ props.item.title }}</td>
                    <td class="text-xs-right">{{ props.item.price }}</td>
                    <td class="text-xs-right">{{ props.item.stock }}</td>
                    <td class="text-xs-right">
                        <v-text-field
                            v-model="props.item.qnt"
                            class="desired-input"
                            :rules="[integerRule]"
                            single-line
                            autofocus
                        ></v-text-field>
                    </td>
                </template>
            </v-data-table>
            <v-snackbar v-model="snack" :timeout="3000" :color="snackColor">
                    {{ snackText }}
                <v-btn flat @click="snack = false">Close</v-btn>
            </v-snackbar>
        </div>
        <div class="text-xs-right button-wrapper">
            <v-btn round color="accent">Buy</v-btn>
        </div>
    </div>
 </template>
  
<script>
    export default {
        name: 'books',
        data () {
        return {
            snack: false,
            snackColor: '',
            snackText: '',
            integerRule: v=> /^[0-9]*$/.test(v) || 'Input must be a integer',
            search: '',
            selected: [],
            headers: [
            { text: 'ID', align: 'left', value:'id'},
            { text: 'Title', align: 'right', value: 'title' },
            { text: 'Price (€)', align: 'right', value: 'price' },
            { text: 'Stock', align: 'right', value: 'stock' },
            { text: 'Desired Quantity', align: 'right', value: 'qnt' },
            ],
            books: [
            {
                id: 0,
                title: 'Frozen Yogurt',
                price: 159,
                stock: 6,
                qnt: '',
            },
            {
                id: 1,
                title: 'Ice cream sandwich',
                price: 237,
                stock: 9,
                qnt: '',
            },
            {
                id: 2,
                title: 'Eclair',
                price: 262,
                stock: 16,
                qnt: '',
            },
            {
                id: 3,
                title: 'Cupcake',
                price: 305,
                stock: 3,
                qnt: '',
            },
            {
                id: 4,
                title: 'Gingerbread',
                price: 356,
                stock: 16,
                qnt: '',
            },
            {
                id: 5,
                title: 'Jelly bean',
                price: 375,
                stock: 0,
                qnt: '',
            },
            {
                id: 6,
                title: 'Lollipop',
                price: 392,
                stock: 2,
                qnt: '',           
            },
            ]
        }
    },
    methods: {
        save () {
            this.snack = true
            this.snackColor = 'success'
            this.snackText = 'Data saved'
        },
        cancel () {
            this.snack = true
            this.snackColor = 'error'
            this.snackText = 'Canceled'
        },
        open () {
            this.snack = true
            this.snackColor = 'info'
            this.snackText = 'Dialog opened'
        },
        close () {
            console.log('Dialog closed')
        },
        dialogInput (val) {
            var isLetter = 65<= val.keyCode & val.keyCode <=90;
            var isCharacter = (val.keyCode == 188 | val.keyCode ==190| val.keyCode == 191 | val.keyCode == 192 | val.keyCode == 219 | val.keyCode == 220| val.keyCode ==221| val.keyCode ==222| val.keyCode ==186| val.keyCode ==187| val.keyCode ==189);

            if (isLetter|isCharacter) {
                val.preventDefault();
            }
        }
    }
  }
</script>
  
<style>
    div.title-wrapper{
        display:flex;
        align-items: center;
        margin-top:1em;
    }
    div.title-wrapper hr{
        width: 100%;
        border-top: .2em solid #76a5af;
    }
    div.title-wrapper hr:first-child{
        width: 17.6%;
        height:1px;
    }
    div.title-wrapper h1{
        color: #76a5af;
        font-size: 2em;
        padding-right: .6em;
        padding-left: .6em;
    }
    div.table-wrapper{
        display: flex;
        flex-direction: column;
        margin-left: 15%;
        margin-right: 15%;
        margin-top:1.5em;
    }

    div.button-wrapper{
        margin-left: 15%;
        margin-right: 15%;
        margin-top:1em;  
    }

    div.button-wrapper button{
        font-weight: 600;
        height: 2.6em;
        width: 5.3em;
        font-size: 1.3em;
        margin-bottom:3em;
    }

    div.table-wrapper .v-text-field.search{
        margin-bottom:1.5em;
        width: 50%;
        align-self: center;
    }

     div.table-wrapper .v-text-field.search label{
        font-size: 1.2em;
    }


    div.table-wrapper .v-text-field.search input{
        font-size: 1.2em;
    }

    th.column{
        font-weight: bold !important;
        font-size: 1.2em !important;
    }
    th.column i{
        font-size: 1.2em !important;
    }

    td{
        font-size: 1.2em !important;
        padding: 1em;
    }

    .desired-input{
        text-align: right;
        padding:0;
        padding-bottom: .5em
    }

    .desired-input input{
        text-align: right;
        font-size:1.1em;
    }

</style>