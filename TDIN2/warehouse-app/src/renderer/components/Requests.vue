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
            isLoading: true,
            search: '',
            selected: [],
            headers: [
            { text: 'ID', align: 'left', value:'id'},
            { text: 'Order ID', align: 'right', value: 'orderid' },
            { text: 'Title', align: 'right', value: 'title' },
            { text: 'Quantity', align: 'right', value: 'qnt' },
            ],
            books: [
            {
                id: 0,
                orderid: 9,
                title: 'Frozen Yogurt',
                qnt: 5,
            },
            {
                id: 1,
                orderid: 21,
                title: 'Ice cream sandwich',
                qnt: 7,
            },
            {
                id: 2,
                orderid: 23,
                title: 'Eclair',
                qnt: 8
            },
            {
                id: 3,
                orderid: 67,
                title: 'Cupcake',
                qnt: 9
            },
            {
                id: 4,
                orderid: 23,
                title: 'Gingerbread',
                qnt: 3
            },
            {
                id: 5,
                orderid: 92,
                title: 'Jelly bean',
                qnt: 2,
            },
            {
                id: 6,
                orderid: 23,
                title: 'Lollipop',
                qnt: 1,           
            },
            ],
        }
    },
    methods: {
        ship(event) {
            //TODO: get client id
            console.log(this.selected);
            

            for(let i = 0; i < this.selected.length; i++){
                if(!isNaN(this.selected[i].qnt) && Number.isInteger(parseFloat(this.selected[i].qnt, 10))){
                    console.log(parseFloat(this.selected[i].qnt, 10));
            
                    //TODO: make order
                }       
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

    .desired-input{
        width: 60%;
        margin-right: 0;
        margin-left: auto;
        text-align: right;
        padding:0;
        padding-bottom: .5em
    }

    .desired-input input{
        text-align: right;
        font-size:1.1em;
    }

</style>