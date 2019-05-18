<template>
    <div>
        <div class="title-wrapper">
            <hr>
            <h1>My Orders</h1>
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
                :headers="headers"
                :items="orders"
                :search="search"
                item-key="id"
                hide-actions 
                class="elevation-1">
                <v-progress-linear v-slot:progress color="accent" indeterminate></v-progress-linear>
                <template v-slot:items="props">
                    <td>{{ props.item.id }}</td>
                    <td class="text-xs-right">{{ props.item.title }}</td>
                    <td class="text-xs-right">{{ props.item.unitprice }} €</td>
                    <td class="text-xs-right">{{ props.item.qnt }}</td>
                    <td class="text-xs-right">{{ props.item.totalprice }} €</td>
                    <td class="text-xs-right">{{ props.item.state }}</td>
                </template>
            </v-data-table>
        </div>
    </div>
 </template>
  
<script>
    export default {
        name: 'orders',
        data () {
        return {
            isLoading: true,
            search: '',
            headers: [
            { text: 'ID', align: 'left', value:'id'},
            { text: 'Title', align: 'right', value: 'title' },
            { text: 'Unit Price', align: 'right', value: 'unitprice' },
            { text: 'Quantity', align: 'right', value: 'qnt' },
            { text: 'Total Price', align: 'right', value: 'totalprice' },
            { text: 'State', align: 'right', value: 'state' },
            ],
            orders: [
            {
                id: 0,
                title: 'Frozen Yogurt',
                unitprice: 10,
                qnt: 3,
                totalprice: 30,
                state: 'Waiting Expedition'
                
            },
            {
                id: 1,
                title: 'Ice cream sandwich',
                unitprice: 15,
                qnt: 4,
                totalprice: 60,
                state: 'Dispatch Will Occur at 19/05/2019'
            },
            {
                id: 2,
                title: 'Eclair',
                unitprice: 20,
                qnt: 2,
                totalprice: 40,
                state: 'Sold'
            },
            {
                id: 3,
                title: 'Cupcake',
                unitprice: 5,
                qnt: 3,
                totalprice: 15,
                state: 'Dispatched at 17/05/2019'
            },
            {
                id: 4,
                title: 'Gingerbread',
                unitprice: 10,
                qnt: 1,
                totalprice: 10,
                state: 'Dispatched at 17/05/2019'
            },
            {
                id: 5,
                title: 'Jelly bean',
                unitprice: 40,
                qnt: 2,
                totalprice: 80,
                state: 'Dispatch Will Occur at 19/05/2019'
            },
            {
                id: 6,
                title: 'Lollipop',
                unitprice: 17,
                qnt: 4,
                totalprice: 68,
                state: 'Sold'           
            },
            ]
        }
    },
    methods: {
        dialogInput (val) {
            var isLetter = 65<= val.keyCode & val.keyCode <=90;
            var isCharacter = (val.keyCode == 188 | val.keyCode ==190| val.keyCode == 191 | val.keyCode == 192 | val.keyCode == 219 | val.keyCode == 220| val.keyCode ==221| val.keyCode ==222| val.keyCode ==186| val.keyCode ==187| val.keyCode ==189);

            if (isLetter|isCharacter) {
                val.preventDefault();
            }
        },
        buy(event) {
            //TODO: get client id
            console.log(this.selected);
            for(let i = 0; i < this.selected.length; i++){
                let qnt = parseInt(this.selected[i].qnt, 10);
                if(!isNaN(qnt) & qnt!=0){
                    console.log(qnt);
                    //TODO: make order
                }       
            }                
        }
    }
  }
</script>
  
<style scoped>
    div.title-wrapper hr:first-child{
        width: 18.5%;
    }
    div.title-wrapper h1{
        width: 8em;
    }
    div.table-wrapper{
        margin-bottom:3em
    }

</style>