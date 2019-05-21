const express = require('express');
const router = express.Router();
const db = require('../../src/database/connection');
const Client = require('../../src/models/Client');
const Sequelize = require('sequelize');
let q = 'store_warehouse2';
let open = require('amqplib').connect('amqp://localhost');
let nodemailer = require('nodemailer');
const { ensureAuthenticated } = require('../../config/auth');

let transporter = nodemailer.createTransport({
  service: 'gmail',
  auth: {
    user: 'tdinbookstore@gmail.com',
    pass: 'tdinbookstore27'
  }
});

function sendRequestToQueue(msg){

  open.then(function(conn) {
    return conn.createChannel();
  }).then(function(ch) {
    return ch.assertQueue(q).then(function(ok) {
      return ch.sendToQueue(q, Buffer.from(msg));
    });
  }).catch(console.warn);

}

function sendEmail(clientEmail, subject, msg){

  let mailOptions = {
    from: 'tdinbookstore@gmail.com',
    to: clientEmail,
    subject: subject,
    text:  msg
  };
  
  transporter.sendMail(mailOptions, function(error, info){
    if (error) {
      console.log(error);
    } else {
      console.log('Email sent: ' + info.response);
    }
  });

}

// Just an experiment
router.get('/', (req, res) => 
  Client.findAll()
    .then(clients => {
        console.log(clients[0].name);
        res.send(clients);
      })
    .catch(err => console.log(err)));

//Insert book (title, stock, price)
router.post('/insertBook', (req, res) => {
  let sql = `INSERT INTO books (title, stock, unitprice) VALUES ('${req.body.title}', '${req.body.stock}', '${req.body.unitprice}')`;
  db.query(sql, { type: Sequelize.QueryTypes.INSERT }, () => {})
  .then(rows => {
    res.sendStatus(200);
  })
  .catch(err => res.send(err));

  // Send Request to Queue

  let msg = 'something to do';
  sendRequestToQueue(msg);

});

//Get All Books
router.get('/getAllBooks', (req, res) => {
  let sql = `SELECT * FROM books`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    if(rows.length == 0) 
      res.sendStatus(404)
    else 
      res.send(rows);
  })
  .catch(err => res.send(err));
});

//Get book (bookId)
router.get('/getBook/:bookid', (req, res) => {
  let sql = `SELECT * FROM books WHERE id = ${req.params.bookid}`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    if(rows.length == 0) 
      res.sendStatus(404)
    else 
      res.send(rows);
  })
  .catch(err => res.send(err));
});

//Delete book (bookId)
router.delete('/deleteBook/:bookid', (req, res) => {
  let sql = `DELETE FROM books WHERE id = ${req.params.bookid}`;
  db.query(sql,  {})
  .then(rows => {
    if(rows[0].affectedRows == 0) 
      res.sendStatus(404)
    else if(rows[0].affectedRows == 1)
      res.sendStatus(200);
  })
  .catch(err => res.send(err));
});

//Update Book Stock (boodid, newstock)
router.put('/updateBookStock', (req, res) => {
  let sql = `UPDATE books SET stock = stock + ${req.body.newstock} WHERE id = ${req.body.bookid}`;
  db.query(sql,  {})
  .then(rows => {
    if(rows[0].affectedRows == 0) 
      res.sendStatus(404)
    else if(rows[0].affectedRows == 1)
      res.sendStatus(200);
  })
  .catch(err => res.send(err));
});

//Create Order (clientEmail, bookTitle, quantity, state)
router.post('/createOrder', (req, res) => {
  let sql = `INSERT INTO orders (clientEmail, bookId, quantity, totalPrice, dispatchedDate, state) VALUES ('${req.body.clientId}', '${req.body.bookId}', '${req.body.quantity}', '${req.body.totalPrice}', '${req.body.dispatchedDate}', '${req.body.state}')`;
  db.query(sql, { type: Sequelize.QueryTypes.INSERT }, {})
  .then(rows => {
    res.sendStatus(200);
  })
  .catch(err => res.send(err));
});


//Update Order State (clientEmail, bookTitle, quantity, state)
router.put('/updateOrder/:orderId', (req, res) => {
  let sql = `UPDATE orders SET state = '${req.body.newstate}' WHERE id = ${req.params.orderId}`;
  db.query(sql,  {})
  .then(rows => {
    if(rows[0].affectedRows == 0) 
      res.sendStatus(404)
    else if(rows[0].affectedRows == 1)
      res.sendStatus(200);
  })
  .catch(err => res.send(err));
});

// Get Client by name
router.get('/getclientByName/:name', (req, res) => {
  let sql = `SELECT * FROM clients WHERE name = ${req.params.name}`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    res.send(rows);
  })
  .catch(err => console.log(err));
});

// Get Orders By email
router.get('/getOrdersByEmail', ensureAuthenticated, (req, res) => {
  let sql = `SELECT * FROM orders WHERE clientEmail = '${req.body.email}'`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    res.send(rows);
  })
  .catch(err => console.log(err));
});

router.get('/getSales', (req, res) => {
  let sql = `SELECT * FROM orders O INNER JOIN books B ON O.bookId=B.id WHERE state = 'sold'`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    res.send(rows);
  })
  .catch(err => console.log(err));
});

module.exports = router;