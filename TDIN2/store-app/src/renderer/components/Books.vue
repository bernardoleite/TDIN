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
                :loading="isLoading"
                v-model="selected"
                :headers="headers"
                :items="books"
                :search="search"
                item-key="id"
                select-all
                hide-actions
                class="elevation-1">
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
                    <td class="text-xs-right">{{ props.item.unitprice }} €</td>
                    <td class="text-xs-right">{{ props.item.stock }}</td>
                    <v-autocomplete
                        class="desired-client"
                        v-model="props.item.client"
                        :items="clients"
                        :search-input.sync="props.item.clientsearch"
                        color="accent"
                        hide-selected
                        item-text="name"
                        item-value="id"
                        return-object
                    ></v-autocomplete>
                    <td class="text-xs-right">
                        <v-text-field
                            v-model="props.item.qnt"
                            class="desired-input"
                            :rules="[integerRule]"
                            single-line
                        ></v-text-field>
                    </td>
                    <td class="text-xs-right">{{ props.item.totalprice }} €</td>
                </template>
            </v-data-table>
        </div>
        <div class="total-price"><p>{{totalPrice}} €</p></div>
        <div class="text-xs-right button-wrapper">
            <v-btn round color="accent" v-on:click.native="sell">Sell</v-btn>
        </div>
    </div>
    </template>
    
<script>
    export default {
        name: 'books',
        data () {
        return {
            isLoading: true,
            integerRule: v=> /^[0-9]*$/.test(v) || 'Input must be a integer',
            search: '',
            selected: [],
            headers: [
            { text: 'ID', align: 'left', value:'id'},
            { text: 'Title', align: 'right', value: 'title' },
            { text: 'Unit Price', align: 'right', value: 'unitprice' },
            { text: 'Stock', align: 'right', value: 'stock' },
            { text: 'Client', align: 'right', value: 'qnt' },
            { text: 'Desired Quantity', align: 'right', value: 'qnt' },
            { text: 'Total Price', align: 'right', value:'totalprice'  },
            ],
            books: [
            {
                id: 0,
                title: 'Frozen Yogurt',
                unitprice: 10,
                stock: 6,
                qnt: '',
                totalprice: 0,
                clientsearch:'',
                client:'',
            },
            {
                id: 1,
                title: 'Ice cream sandwich',
                unitprice: 25,
                stock: 9,
                qnt: '',
                totalprice: 0,
                clientsearch:'',
                client:'',
            },
            {
                id: 2,
                title: 'Eclair',
                unitprice: 15,
                stock: 16,
                qnt: '',
                totalprice: 0,
                clientsearch:'',
                client:'',
            },
            {
                id: 3,
                title: 'Cupcake',
                unitprice: 22,
                stock: 3,
                qnt: '',
                totalprice: 0,
                clientsearch:'',
                client:'',
            },
            {
                id: 4,
                title: 'Gingerbread',
                unitprice: 11,
                stock: 16,
                qnt: '',
                totalprice: 0,
                clientsearch:'',
                client:'',
            },
            {
                id: 5,
                title: 'Jelly bean',
                unitprice: 9,
                stock: 0,
                qnt: '',
                totalprice: 0,
                clientsearch:'',
                client:'',
            },
            {
                id: 6,
                title: 'Lollipop',
                unitprice: 6,
                stock: 2,
                qnt: '',
                totalprice: 0, 
                clientsearch:'',
                client:'',          
            },
            ],
            oldbooks:[],
            totalPrice: 0,
            clients: [
                { name: 'Sandra Adams', id:0 },
                { name: 'Ali Connors', id:1 },
                { name: 'Trevor Hansen', id:2 },
                { name: 'Tucker Smith', id:3 },
                { name: 'Britta Holt', id:4 },
                { name: 'Jane Smith ', id:5 },
                { name: 'John Smith', id:6 },
                { name: 'Sandra Williams', id:7 }
            ],
        }
    },
    watch: {
        books: {
            handler: function (after, before) {
                var vm = this;
                var changed = after.filter( function( p, idx ) {
                    return Object.keys(p).some( function( prop ) {
                        return p[prop] !== vm.$data.oldbooks[idx][prop];
                    })
                })
                var difference = changed[0].totalprice - this.oldbooks.find(x => x.id ===  changed[0].id).totalprice;

                vm.setValue();
                var obj = changed[0];
                if(!isNaN(obj.qnt) && Number.isInteger(parseFloat(obj.qnt,10))){
                    obj.totalprice=obj.qnt*obj.unitprice;
                }
                else{
                    obj.totalprice=0;
                }
                this.totalPrice = this.totalPrice + difference;
                
            },
            deep: true
        }
    },
    mounted(){
        this.setValue();   
    },
    methods: {
        setValue: function() {
            var _ = require('lodash');
            this.$data.oldbooks = _.cloneDeep(this.$data.books);
        },
        sell(event) {
            //TODO: get client id
            console.log(this.selected);
            

            for(let i = 0; i < this.selected.length; i++){
                if(!isNaN(this.selected[i].qnt) && Number.isInteger(parseFloat(this.selected[i].qnt, 10))){
                    console.log(parseFloat(this.selected[i].qnt, 10));
            
                    //TODO: make order
                }       
            }  
        }
    },
    
    }
</script>
    
<style>

    div.table-wrapper{
        margin-bottom:1em !important;
    }

    .desired-input{
        width: 70%;
        margin-right: 0;
        margin-left: auto;
        text-align: right !important;
        padding:0;
        padding-bottom: .5em
    }

    .desired-client{
        width: 70%;
        margin-right: 0;
        margin-left: auto;
        text-align: right !important;
        padding:0;
        padding-bottom: .5em
    }

    .desired-input input, .desired-client input{
        text-align: right !important;
        font-size:1.1em !important;
        padding-right: .5em;
    }

</style>