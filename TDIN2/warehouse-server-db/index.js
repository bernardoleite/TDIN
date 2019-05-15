const express = require('express');
var amqp = require('amqplib/callback_api');
const newRequest = require("./src/createRequest");

// DB connection
require("./src/database/connection");

// Populate Database
require("./src/bootstrap")();

const app = express();


amqp.connect('amqp://localhost', function(error0, connection) {
    if (error0) {
        throw error0;
    }
    connection.createChannel(function(error1, channel) {
        if (error1) {
            throw error1;
        }

        var queue = 'store_warehouse';

        channel.assertQueue(queue, {
            durable: false
        });

        console.log(" [*] Waiting for messages in %s. To exit press CTRL+C", queue);

        channel.consume(queue, function(msg) {
            console.log(" [x] Received %s", msg.content.toString());
            newRequest(msg.content.toString());
        }, {
            noAck: true
        });
    });
});

//Body Parser Middleware
app.use(express.json());
app.use(express.urlencoded({extended: false}));

//Warehouse API Routes
app.use('/api/warehouse', require('./routes/api/warehouse'));

const PORT = process.env.PORT || 5001;

app.listen(PORT, () => console.log(`Server started on port ${PORT}`));