const express = require('express');
const router = express.Router();
const db = require('../../src/database/connection');
const Client = require('../../src/models/Client');
const Sequelize = require('sequelize');

// Just an experiment
router.get('/', (req, res) => 
  Client.findAll()
    .then(clients => {
        console.log(clients[0].name);
        res.send(clients);
      })
    .catch(err => console.log(err)));

//Insert Client (name, address, email)
router.post('/insertClient', (req, res) => {
  let sql = `INSERT INTO clients (name, address, email) VALUES ('${req.body.name}', '${req.body.address}', '${req.body.email}')`;
  db.query(sql, { type: Sequelize.QueryTypes.INSERT }, () => {})
  .then(rows => {
    res.sendStatus(200);
  })
  .catch(err => res.send(err));
});

// Get Client by id
router.get('/getclientById/:id', (req, res) => {
  let sql = `SELECT * FROM clients WHERE id = ${req.params.id}`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    if(rows.length == 0) 
      res.sendStatus(404)
    else 
      res.send(rows);
  })
  .catch(err => res.send(err));
});

//Delete Client (clientid)
router.delete('/deleteClient/:id', (req, res) => {
  let sql = `DELETE FROM clients WHERE id = ${req.params.id}`;
  db.query(sql,  {})
  .then(rows => {
    if(rows[0].affectedRows == 0) 
      res.sendStatus(404)
    else if(rows[0].affectedRows == 1)
      res.sendStatus(200);
  })
  .catch(err => res.send(err));
});

//Get Client Orders (cliendid)
router.get('/getclientOrders/:id', (req, res) => {
  let sql = `SELECT * FROM orders WHERE clientId = ${req.params.id}`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    if(rows.length == 0) 
      res.sendStatus(404)
    else 
      res.send(rows);
  })
  .catch(err => res.send(err));
});

//Insert book (title, stock, price)
router.post('/insertBook', (req, res) => {
  let sql = `INSERT INTO books (title, stock, price) VALUES ('${req.body.title}', '${req.body.stock}', '${req.body.price}')`;
  db.query(sql, { type: Sequelize.QueryTypes.INSERT }, () => {})
  .then(rows => {
    res.sendStatus(200);
  })
  .catch(err => res.send(err));
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

//Create Order (clientId, bookTitle, quantity, state)
router.post('/createOrder', (req, res) => {
  let sql = `INSERT INTO orders (clientId, bookId, quantity, totalPrice, dispatchedDate, state) VALUES ('${req.body.clientId}', '${req.body.bookId}', '${req.body.quantity}', '${req.body.totalPrice}', '${req.body.dispatchedDate}', '${req.body.state}')`;
  db.query(sql, { type: Sequelize.QueryTypes.INSERT }, {})
  .then(rows => {
    res.sendStatus(200);
  })
  .catch(err => res.send(err));
});


//Update Order State (clientId, bookTitle, quantity, state)
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

// Get Order By id
router.get('/getOrder/:id', (req, res) => {
  let sql = `SELECT * FROM orders WHERE id = ${req.params.id}`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    res.send(rows);
  })
  .catch(err => console.log(err));
});

module.exports = router;