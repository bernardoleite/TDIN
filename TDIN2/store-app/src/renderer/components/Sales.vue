<template>
    <div>
        <div class="title-wrapper">
            <hr>
            <h1>Sales</h1>
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
                :items="sales"
                :search="search"
                item-key="id"
                hide-actions 
                class="elevation-1">
                <v-progress-linear v-slot:progress color="accent" indeterminate></v-progress-linear>
                <template v-slot:items="props">
                    <td>{{ props.item.id }}</td>
                    <td class="text-xs-right">{{ props.item.client }}</td>
                    <td class="text-xs-right">{{ props.item.title }}</td>
                    <td class="text-xs-right">{{ props.item.unitprice }} €</td>
                    <td class="text-xs-right">{{ props.item.qnt }}</td>
                    <td class="text-xs-right">{{ props.item.totalprice }} €</td>
                </template>
            </v-data-table>
        </div>
    </div>
 </template>
  
<script>
    export default {
        name: 'sales',
        data () {
        return {
            isLoading: true,
            search: '',
            headers: [
            { text: 'ID', align: 'left', value:'id'},
            { text: 'Client Name', align: 'right', value: 'client' },
            { text: 'Title', align: 'right', value: 'title' },
            { text: 'Unit Price', align: 'right', value: 'unitprice' },
            { text: 'Quantity', align: 'right', value: 'qnt' },
            { text: 'Total Price', align: 'right', value: 'totalprice' },
            ],
            sales: [
            {
                id: 0,
                client:'Elit Sed',
                title: 'Frozen Yogurt',
                unitprice: 10,
                qnt: 3,
                totalprice: 30,
            },
            {
                id: 1,
                client: 'Lorem Ipsum',
                title: 'Ice cream sandwich',
                unitprice: 15,
                qnt: 4,
                totalprice: 60,
            },
            {
                id: 2,
                client: 'Dolor Dit Amet',
                title: 'Eclair',
                unitprice: 20,
                qnt: 2,
                totalprice: 40,
            },
            {
                id: 3,
                client: 'Consectet Adipiscing',
                title: 'Cupcake',
                unitprice: 5,
                qnt: 3,
                totalprice: 15,
            },
            {
                id: 4,
                client: 'Elit Sed',
                title: 'Gingerbread',
                unitprice: 10,
                qnt: 1,
                totalprice: 10,
            },
            {
                id: 5,
                client: 'Lorem Ipsum',
                title: 'Jelly bean',
                unitprice: 40,
                qnt: 2,
                totalprice: 80,
            },
            {
                id: 6,
                client: 'Dolor Dit Amet',
                title: 'Lollipop',
                unitprice: 17,
                qnt: 4,
                totalprice: 68,
            },
            ]
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
        width: 17.5%;
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
        margin-bottom:3em
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

    table.v-table thead th.sortable{
        font-weight: bold !important;
        font-size: 1.2em !important;
    }

</style>