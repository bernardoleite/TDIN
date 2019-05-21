<template>
    <div>
        <div class="title-wrapper">
            <hr>
            <h1>Orders</h1>
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
                v-model="selected"
                :headers="headers"
                :items="orders"
                :search="search"
                item-key="id"
                v-bind:pagination.sync="pagination"
                select-all
                hide-actions 
                class="elevation-1">
                <v-progress-linear v-slot:progress color="accent" indeterminate></v-progress-linear>
                <template v-slot:items="props">
                    <td>
                        <v-checkbox
                        v-model="props.selected"
                        primary
                        hide-details
                        ></v-checkbox>
                    </td>
                    <td>{{ props.item.id }}</td>
                    <td class="text-xs-right">{{ props.item.client }}</td>
                    <td class="text-xs-right">{{ props.item.title }}</td>
                    <td class="text-xs-right">{{ props.item.unitprice }} €</td>
                    <td class="text-xs-right">{{ props.item.qnt }}</td>
                    <td class="text-xs-right">{{ props.item.totalprice }} €</td>
                    <td class="text-xs-right capitalize">{{ props.item.state }}</td>
                    <td class="text-xs-right">{{ props.item.date }}</td>
                </template>
            </v-data-table>
        </div>
        <div class="text-xs-right button-wrapper">
            <v-btn id="arrived_button" v-bind:disabled="arrivedButtonDisabled" round color="accent" v-on:click.native="arrived">Arrived</v-btn>
            <v-btn id="sold_button" v-bind:disabled="soldButtonDisabled" round color="accent" v-on:click.native="sold">Sold</v-btn>
        </div>
    </div>
 </template>
  
<script>
    export default {
        name: 'orders',
        data () {
        return {
            arrivedButtonDisabled: true,
            soldButtonDisabled: true,
            search: '',
            pagination: {'sortBy': 'state', 'descending': true, 'rowsPerPage': -1},
            selected: [],
            headers: [
            { text: 'ID', align: 'left', value:'id'},
            { text: 'Client Name', align: 'right', value: 'client' },
            { text: 'Title', align: 'right', value: 'title' },
            { text: 'Unit Price', align: 'right', value: 'unitprice' },
            { text: 'Quantity', align: 'right', value: 'qnt' },
            { text: 'Total Price', align: 'right', value: 'totalprice' },
            { text: 'State', align: 'right', value: 'state' },
            { text: 'Arrive Date', align: 'right', value: 'date' },
            ],
            orders: []
        }
    },
    mounted: function () {
        this.getOrders();
    },
    methods: {
        getOrders(){
            let vm=this;
            axios.get('/getOrders')
            .then(function (response) {
                // handle success
                vm.orders=[];
                let orders = response.data;
                console.log(orders);
                for(let i=0; i< orders.length; i++){
                    let order = {
                        id: orders[i].id,
                        client:orders[i].clientEmail,
                        title: orders[i].bookTitle,
                        unitprice: orders[i].unitprice,
                        qnt: orders[i].quantity,
                        totalprice: orders[i].totalPrice,
                        state: orders[i].state,
                        date: orders[i].dispatchedDate
                    };

                    vm.orders.push(order);
                }
                vm.selected=[];

            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
        },
        sold(event) {
            for(let i = 0; i< this.selected.length; i++){
                console.log(this.selected[i]);
                //TODO: Change State
            }
        },
        arrived(event) {
            for(let i = 0; i< this.selected.length; i++){
                console.log(this.selected[i]);
                //TODO: Change State
            }
        }
    },
    watch:{
        selected:{
            handler (val) {
                let arrivedAble = false;
                let soldAble = false;
                let expedition = false
    
                for(var i = 0; i < Object.keys(val).length; i++){
                    if(val[i].state=='waiting')
                        expedition=true;
                    else if(val[i].state=='dispatched')
                        arrivedAble=true;
                    else if(val[i].state=='ready')
                        soldAble=true;
                }

                let arrivedButton = document.getElementById("arrived_button");
                let soldButton = document.getElementById("sold_button");

                
                if(expedition){
                    this.arrivedButtonDisabled = true;
                    this.soldButtonDisabled = true;
                }
                else if(arrivedAble & !soldAble){
                    this.arrivedButtonDisabled = false;
                    this.soldButtonDisabled = true;
                }
                else if(!arrivedAble & soldAble){
                    this.arrivedButtonDisabled = true;
                    this.soldButtonDisabled = false;
                }
                else if(arrivedAble & soldAble){
                    this.arrivedButtonDisabled = true;
                    this.soldButtonDisabled = true;
                }
                else if(!arrivedAble & !soldAble){
                    this.arrivedButtonDisabled = true;
                    this.soldButtonDisabled = true;
                }
            },
        }
    }
  }
</script>
  
<style scoped>

    div.title-wrapper hr:first-child{
        width: 17.7% !important;
    }

     div.table-wrapper{
        margin-bottom:1em
    }

    div.button-wrapper{
        margin-left: 15%;
        margin-right: 15%;
        margin-top:1em;  
    }

    div.button-wrapper button{
        font-weight: 500 !important;
        height: 2.6em;
        width: 7em;
        font-size: 1.3em;
        margin-bottom:3em;
    }
    .capitalize:first-letter{
        text-transform:capitalize;
    }

</style>