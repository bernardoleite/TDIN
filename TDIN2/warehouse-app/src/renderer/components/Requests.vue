<template>
    <div>
        <div class="title-wrapper">
            <hr>
            <h1>Requests</h1>
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
                :items="requests"
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
                    <td class="text-xs-right">{{ props.item.orderid }}</td>
                    <td class="text-xs-right">{{ props.item.title }}</td>
                    <td class="text-xs-right">{{ props.item.qnt }}</td>
                </template>
            </v-data-table>
        </div>
        <div class="text-xs-right button-wrapper">
            <v-btn round color="accent" v-on:click.native="ship">Ship</v-btn>
        </div>
    </div>
    </template>
    
<script>
    export default {
        name: 'requests',
        data () {
        return {
            componentKey: 0,
            search: '',
            selected: [],
            headers: [
            { text: 'ID', align: 'left', value:'id'},
            { text: 'Order ID', align: 'right', value: 'orderid' },
            { text: 'Title', align: 'right', value: 'title' },
            { text: 'Quantity', align: 'right', value: 'qnt' },
            ],
            requests: [],
        }
    },
    mounted: function () {
        this.getAllPendingRequests(this);
        this.getAllPendingRequestsInterval();
    },
    methods: {
        ship(event) {

            let isError = false;
            console.log(this.selected);

            for(let i = 0; i < this.selected.length; i++){
                let vm=this;
                
                axios.put('/updateRequestStateByOrderId/' + vm.selected[i].orderid , 
                {
                    newstate: 'shipped'
                })
                .then(function (response) {
                    // handle success
                    console.log(response);
  
                }).catch(function (error) {
                    // handle error
                    isError=true;
                    console.log(error);
                })

                if(!isError){
                    vm.requests.splice(vm.requests.indexOf(vm.selected[i]),1)
                    vm.selected.splice(i,1);
                    i--; 
                }
            }
        },
        getAllPendingRequests(self){
            console.log("Get Pending Requests called");

            let vm=this;
        
            axios.get('/getAllPendingRequests')
            .then(function (response) {
                // handle success
                vm.requests=[];
                let pendingRequests = response.data;
                for(let i=0; i< pendingRequests.length; i++){
                    let request = {
                        id: pendingRequests[i].id,
                        orderid: pendingRequests[i].orderId,
                        title: pendingRequests[i].bookTitle,
                        qnt: pendingRequests[i].quantity,
                    }

                    vm.requests.push(request);
                }
                vm.selected=[];

            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
        },
        getAllPendingRequestsInterval(){
            var self= this;
            setInterval(function () {
                self.getAllPendingRequests(self);
            }, 10000);
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
        width: 18%;
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
    div.total-price{
        margin-left: 15%;
        margin-right: 15%;
        margin-top:1em;
    }

    div.total-price p{
        width: 100%;
        text-align: right;
        font-weight: bold;
        font-size: 1.7em;
        padding-right: 1em;
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


</style>