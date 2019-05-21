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
            
            <div class="dialogs">
                
                <v-dialog v-model="dialogClient" max-width="500px">
                        <template v-slot:activator="{ on }">
                            <v-btn color="accent" outline round v-on="on" class="add-client">Add Client</v-btn>
                        </template>
                        <v-card>
                        <v-card-title>
                            <span class="headline">Add Client</span>
                        </v-card-title>
                        <v-card-text>
                            <v-container grid-list-md>
                            <v-layout wrap>
                                <v-flex xs12 sm6 md6>
                                <v-text-field v-model="editedClient.name" label="Name"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm6 md6>
                                <v-text-field v-model="editedClient.email" label="Email"></v-text-field>
                                </v-flex>
                            </v-layout>
                            </v-container>
                        </v-card-text>
                
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="accent" round flat @click="closeClientDialog">Cancel</v-btn>
                            <v-btn color="accent" outline round flat @click="saveClientDialog">Save</v-btn>
                        </v-card-actions>
                        </v-card>
                </v-dialog>

                <v-dialog v-model="dialogBook" max-width="500px">
                        <template v-slot:activator="{ on }">
                            <v-btn color="accent" round v-on="on" class="add-book">Add Book</v-btn>
                        </template>
                        <v-card>
                        <v-card-title>
                            <span class="headline">Add Book</span>
                        </v-card-title>
                        <v-card-text>
                            <v-container grid-list-md>
                            <v-layout wrap>
                                <v-flex xs12 sm6 md4>
                                <v-text-field v-model="editedBook.title" label="Title"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm6 md4>
                                <v-text-field v-model="editedBook.unitprice" :rules="[integerRule]" label="Unit Price (€)"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm6 md4>
                                <v-text-field v-model="editedBook.stock" :rules="[integerRule]" label="Stock"></v-text-field>
                                </v-flex>
                            </v-layout>
                            </v-container>
                        </v-card-text>
                
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="accent" round flat @click="closeBookDialog">Cancel</v-btn>
                            <v-btn color="accent" outline round flat @click="saveBookDialog">Save</v-btn>
                        </v-card-actions>
                        </v-card>
                </v-dialog>
            </div>

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
                    <td class="text-xs-right">
                        <v-icon
                          @click="deleteBook(props.item)"
                        > delete </v-icon>
                    </td>
                    <v-autocomplete
                        class="desired-client"
                        v-model="props.item.client"
                        :items="clients"
                        :search-input.sync="props.item.clientsearch"
                        color="accent"
                        hide-selected
                        item-text="email"
                        item-value="id"
                        return-object
                    ></v-autocomplete>
                    </v-flex>
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
            dialogClient: false,
            dialogBook: false,
            editedBook: {
                id: 0,
                title: '',
                unitprice: '',
                stock: '',
                qnt: 0,
                totalprice: 0,
                clientsearch:'',
                client:'',
            },
            defaultBook: {
                id: 0,
                title: '',
                unitprice: 0,
                stock: 0,
                qnt: '',
                totalprice: 0,
                clientsearch:'',
                client:'',
            },
            selected: [],
            headers: [
            { text: 'ID', align: 'left', value:'id'},
            { text: 'Title', align: 'right', value: 'title' },
            { text: 'Unit Price', align: 'right', value: 'unitprice' },
            { text: 'Stock', align: 'right', value: 'stock' },
            { text: '', align: 'right' },
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
            editedClient: {
                id: 0,
                name: '',
                email: '',
            },
            defaultClient: {
                id: 0,
                name: '',
                email: '',
            },
            clients: [],
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
                if(changed.length != 0){
                
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

                }
               
            },
            deep: true
        },
        dialog (val) {
            val || this.close()
        }
    },
    mounted(){
        this.getAllBooks(); 
        this.setValue(); 
        this.getClients();  
    },
    methods: {
        getAllBooks(){
            let vm=this;
            vm.isLoading = true;
            axios.get('http://localhost:5000/api/store/getAllBooks')
            .then(function (response) {
                // handle success
                vm.books=[];
                let allBooks = response.data;
                let tempBooks =[];
                for(let i=0; i< allBooks.length; i++){
                    let book = {
                        id: allBooks[i].id,
                        title: allBooks[i].title,
                        unitprice: allBooks[i].unitprice,
                        stock: allBooks[i].stock,
                        qnt: '',
                        totalprice: 0,
                    }

                    tempBooks.push(book);
                }
                
                vm.setValueWithArray(tempBooks);
                vm.books=tempBooks;
                //vm.setValueWithArray(tempBooks)

                vm.selected=[];
                vm.isLoading = false;

            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
        },
        setValue: function() {
            var _ = require('lodash');
            this.$data.oldbooks = _.cloneDeep(this.$data.books);
        },
        setValueWithArray: function(books) {
            var _ = require('lodash');
            this.$data.oldbooks = _.cloneDeep(books);
        },
        getClients(){
            let vm=this;
            axios.get('/getClients')
            .then(function (response) {
                console.log(response);
                // handle success
                vm.clients=[];
                let clients = response.data;
                for(let i=0; i< clients.length; i++){
                    let request = {
                        id: clients[i].id,
                        name: clients[i].name,
                        email:clients[i].email,
                    };

                    vm.clients.push(request);
                }
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
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
        },
        deleteBook (item) {
            const index = this.books.indexOf(item);
            confirm('Are you sure you want to delete this book?') && this.books.splice(index, 1);

            //TODO: Delete Book
        },
        closeBookDialog() {
            this.dialogBook = false
            setTimeout(() => {
                this.editedBook = Object.assign({}, this.defaultBook)
            }, 300)
        },

        saveBookDialog() {
            this.editedBook.id=this.books[this.books.length-1].id + 1;
            this.books.push(this.editedBook)
            this.closeBookDialog()
        },

        closeClientDialog() {
            this.dialogClient = false
            setTimeout(() => {
                this.editedClient = Object.assign({}, this.defaultClient)
            }, 300)
        },

        saveClientDialog() {
            this.editedClient.id=this.clients[this.clients.length-1].id + 1;
            this.clients.push(this.editedClient)
            this.closeClientDialog()
        }
    },
    
    }
</script>
    
<style>

    div.table-wrapper{
        margin-bottom:1em !important;
    }

    .desired-input{
        width:70%;
        margin-right: 0;
        margin-left: auto;
        text-align: right !important;
        padding:0;
        padding-bottom: .5em
    }

    .desired-client{
        width: 90%;
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
    i.v-icon:hover{
        color: #76a5af !important;
    }

    button{
        width: 8em !important;
    }
    .dialogs{
        margin-left: auto;
        margin-right: 0;
    }
    .dialogs button.add-book {
        height: 2.6em;
        font-size: 1.2em;
        margin-bottom:.7em;
        font-weight: 300 !important;
    }

     .dialogs button.add-client {
        height: 2.6em;
        font-size: 1.2em;
        margin-bottom:.7em;
        font-weight: 300 !important;
    }

</style>