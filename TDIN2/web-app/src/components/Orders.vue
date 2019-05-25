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
                    <td class="text-xs-right capitalize">{{ props.item.state }}</td>
                    <td class="text-xs-right">{{ props.item.date }}</td>
                </template>
            </v-data-table>
        </div>
    </div>
 </template>
  
<script>
    import axios from 'axios'
    
    export default {
        name: 'orders',
        data () {
        return {
            search: '',
            headers: [
            { text: 'ID', align: 'left', value:'id'},
            { text: 'Title', align: 'right', value: 'title' },
            { text: 'Unit Price', align: 'right', value: 'unitprice' },
            { text: 'Quantity', align: 'right', value: 'qnt' },
            { text: 'Total Price', align: 'right', value: 'totalprice' },
            { text: 'State', align: 'right', value: 'state' },
            { text: 'Arrive Date', align: 'right', value: 'date' },
            ],
            orders: [
            ]
        }
    },
    created () {
      if (!this.$session.exists()) {
        this.$router.push('/')
      }
    },
    mounted: function () {
        this.getOrders(this);
        this.getOrdersInterval();
    },
    methods: {
        getOrders(self){
            console.log("Get Orders called");
            let vm=this;
            
            axios.get('http://localhost:5000/api/store/getOrdersByEmail', {
                params: {
                    email: vm.$session.get('email'),
                },
            })
            .then(function (response) {
                // handle success
                vm.orders=[];
                let orders = response.data;
            
                for(let i=0; i< orders.length; i++){
                    let date = orders[i].dispatchedDate;
                    let dateParsed = date.split("T");

                    let order = {
                        id: orders[i].id,
                        title: orders[i].title,
                        unitprice: orders[i].unitprice,
                        qnt: orders[i].quantity,
                        totalprice: orders[i].totalPrice,
                        state: orders[i].state,
                        date: dateParsed[0]
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
        getOrdersInterval(){
            var self= this;
            setInterval(function () {
                self.getOrders(self);
            },  10000);
        },
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

     .capitalize:first-letter{
        text-transform:capitalize;
    }

</style>