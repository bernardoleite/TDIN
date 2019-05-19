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
                :loading="isLoading"
                v-model="selected"
                :headers="headers"
                :items="orders"
                :search="search"
                item-key="id"
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
                    <td class="text-xs-right">{{ props.item.state }} {{ props.item.date }}</td>
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
            isLoading: true,
            arrivedButtonDisabled: true,
            soldButtonDisabled: true,
            search: '',
            selected: [],
            headers: [
            { text: 'ID', align: 'left', value:'id'},
            { text: 'Client Name', align: 'right', value: 'client' },
            { text: 'Title', align: 'right', value: 'title' },
            { text: 'Unit Price', align: 'right', value: 'unitprice' },
            { text: 'Quantity', align: 'right', value: 'qnt' },
            { text: 'Total Price', align: 'right', value: 'totalprice' },
            { text: 'State', align: 'right', value: 'state' },
            ],
            orders: [
            {
                id: 0,
                client:'Elit Sed',
                title: 'Frozen Yogurt',
                unitprice: 10,
                qnt: 3,
                totalprice: 30,
                state: 'Waiting Expedition',
                date: ''
            },
            {
                id: 1,
                client: 'Lorem Ipsum',
                title: 'Ice cream sandwich',
                unitprice: 15,
                qnt: 4,
                totalprice: 60,
                state: 'Dispatch Will Occur at',
                date: '19/05/2019'
            },
            {
                id: 2,
                client: 'Dolor Dit Amet',
                title: 'Eclair',
                unitprice: 20,
                qnt: 2,
                totalprice: 40,
                state: 'Waiting Expedition',
                date: ''
            },
            {
                id: 3,
                client: 'Consectet Adipiscing',
                title: 'Cupcake',
                unitprice: 5,
                qnt: 3,
                totalprice: 15,
                state: 'Dispatched at',
                date: '17/05/2019'
            },
            {
                id: 4,
                client: 'Elit Sed',
                title: 'Gingerbread',
                unitprice: 10,
                qnt: 1,
                totalprice: 10,
                state: 'Dispatch Will Occur at',
                date: '19/05/2019'
            },
            {
                id: 5,
                client: 'Lorem Ipsum',
                title: 'Jelly bean',
                unitprice: 40,
                qnt: 2,
                totalprice: 80,
                state: 'Dispatched at',
                date: '17/05/2019'
            },
            {
                id: 6,
                client: 'Dolor Dit Amet',
                title: 'Lollipop',
                unitprice: 17,
                qnt: 4,
                totalprice: 68,
                state: 'Dispatch Will Occur at',
                date: '19/05/2019'
            },
            ]
        }
    },
    methods: {
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
                    if(val[i].state=='Waiting Expedition')
                        expedition=true;
                    else if(val[i].state=='Dispatch Will Occur at')
                        arrivedAble=true;
                    else if(val[i].state=='Dispatched at')
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

</style>