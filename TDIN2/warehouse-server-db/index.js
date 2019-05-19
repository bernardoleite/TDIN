const express = require('express');
//var amqp = require('amqplib/callback_api');
const newRequest = require("./src/createRequest");
let q = 'store_warehouse2';
let open = require('amqplib').connect('amqp://localhost');

// DB connection
require("./src/database/connection");

// Populate Database
require("./src/bootstrap")();

const app = express();

// Consumes Messages
open.then(function(conn) {
    return conn.createChannel();
  }).then(function(ch) {
    return ch.assertQueue(q).then(function(ok) {
      return ch.consume(q, function(msg) {
        if (msg !== null) {
          console.log(msg.content.toString());
          newRequest(msg.content.toString());
          ch.ack(msg);
        }
      });
    });
  }).catch(console.warn);

//Body Parser Middleware
app.use(express.json());
app.use(express.urlencoded({extended: false}));

//Warehouse API Routes
app.use('/api/warehouse', require('./routes/api/warehouse'));

const PORT = process.env.PORT || 5001;

app.listen(PORT, () => console.log(`Server started on port ${PORT}`));