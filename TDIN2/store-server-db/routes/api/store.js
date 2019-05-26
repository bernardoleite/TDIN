const express = require('express');
const router = express.Router();
const db = require('../../src/database/connection');
const Client = require('../../src/models/Client');
const Book = require('../../src/models/Book');
const Sequelize = require('sequelize');
let q = 'store_warehouse2';
let open = require('amqplib').connect('amqp://localhost');
let nodemailer = require('nodemailer');
const { ensureAuthenticated } = require('../../config/auth');
const request = require('request');

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

// dispatchedDate, title, price, pricePerbook, qty
function prepareEmail(dispDate, title, price, unitPrice, qty){

  let all = "";
  
  let intro = "Thank you for your purchase from tdinstore ! \n \n";
  let item = ""; item = item.concat("Item: ", title, "\n");
  let dispatchedDate = ""; dispatchedDate = dispatchedDate.concat("Dispatched Date: ", dispDate, " \n \n \n");


  let quantity= ""; quantity = quantity.concat("Quantity: ", qty.toString(), "\n");
  let unitp= ""; unitp = unitp.concat("Unit Price: ", unitPrice.toString(), "\n");
  let total= ""; total = total.concat("Total Price: ", price.toString(), "€", "\n");


  all = all.concat(intro,item,dispatchedDate,quantity,unitp,total);

  return all;


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
  let sql_insert = `INSERT INTO books (title, stock, unitprice) VALUES ('${req.body.title}', ${req.body.stock}, ${req.body.unitprice})`;
  //let sql_get = `SELECT * FROM books WHERE title = '${req.body.title}' AND stock = '${req.body.stock}' AND uniprice '${req.body.unitprice}')`;
  db.query(sql_insert, { type: Sequelize.QueryTypes.INSERT }, () => {})
  .then(rows => {
    res.send(rows)
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

//Create Order (clientEmail, bookId, quantity, local) 
router.post('/createOrder', async (req, res) => {

  let refbook = await Promise.resolve(db.query(`select title, stock, unitprice FROM books WHERE id = ${req.body.bookId}`));
  
  let BOOKID = req.body.bookId;
  let BOOKSTOCK = refbook[0][0].stock;
  let BOOKTITLE = refbook[0][0].title;
  let BOOKUNITPRICE = refbook[0][0].unitprice;
  
  let state;
  let currentDate = new Date();
  let dispatchedDate;
  let finalQty = 0;
  
  let totalPrice = req.body.quantity * BOOKUNITPRICE;
  
    if(req.body.local == 'store' && req.body.quantity <= BOOKSTOCK )
    {
      state = 'sold';
      dispatchedDate = currentDate;
      dispatchedDate = dispatchedDate.toISOString().slice(0, 19).replace('T', ' ');
      finalQty = req.body.quantity;

      await Promise.resolve(db.query(`UPDATE books SET stock = stock - ${req.body.quantity} WHERE id = ${req.body.bookId}`));

      //client, item, quantity, unitprice, totalprice
      printRecip(req.body.clientEmail, refbook[0][0].title, req.body.quantity, refbook[0][0].unitprice, totalPrice);
        
    }
    else if(req.body.local == 'store' && req.body.quantity > BOOKSTOCK )
    {
      state = 'waiting';
      dispatchedDate = new Date(null).toISOString().slice(0, 19).replace('T', ' ');
      finalQty = (req.body.quantity - BOOKSTOCK) + 10;

      await Promise.resolve(db.query(`UPDATE books SET stock = 0 WHERE id = ${req.body.bookId}`));
      
    }
    else if(req.body.local == 'webapp' && req.body.quantity <= BOOKSTOCK )
    {
      state = 'dispatched';
      dispatchedDate = currentDate;
      dispatchedDate.setDate(currentDate.getDate()+1);
      dispatchedDate = dispatchedDate.toISOString().slice(0, 19).replace('T', ' ');
      finalQty = req.body.quantity;

      await Promise.resolve(db.query(`UPDATE books SET stock = stock - ${req.body.quantity} WHERE id = ${req.body.bookId}`));

    }
    else if(req.body.local == 'webapp' && req.body.quantity > BOOKSTOCK )
    {
      state = 'waiting';
      dispatchedDate = new Date(null).toISOString().slice(0, 19).replace('T', ' ');
      finalQty = (req.body.quantity - BOOKSTOCK) + 10;

      await Promise.resolve(db.query(`UPDATE books SET stock = 0 WHERE id = ${req.body.bookId}`));

    }
  
    let sql = `INSERT INTO orders (clientEmail, bookId, quantity, totalPrice, dispatchedDate, state) VALUES ('${req.body.clientEmail}', '${BOOKID}', '${req.body.quantity}', '${totalPrice}', '${dispatchedDate}', '${state}')`;
    db.query(sql, { type: Sequelize.QueryTypes.INSERT }, {})
    .then(rows => {
      
    if(state == 'waiting')
    {
      let newRequest = {
        "orderId":rows[0],
        "bookTitle": BOOKTITLE || 'undefined',
        "quantity":finalQty,
        "state":'pending',
        }
      sendRequestToQueue(JSON.stringify(newRequest));
    }
    if(dispatchedDate == new Date(null).toISOString().slice(0, 19).replace('T', ' '))
      dispatchedDate = 'Waiting Expedition';
 
      let emailContent = prepareEmail(dispatchedDate, BOOKTITLE, totalPrice, BOOKUNITPRICE, req.body.quantity);
      sendEmail(req.body.clientEmail, 'Your Order', emailContent);
      res.sendStatus(200);
    })
    .catch(err => res.send(err));
  
  });


//Update Order State (body newstate, clientEmail)
router.put('/updateOrder/:orderId', async (req, res) => {

  //get order quantity
  let refOrder = await Promise.resolve(db.query(`select clientEmail, totalPrice, quantity, bookId, state FROM orders WHERE id = ${req.params.orderId}`));

  //get book stock
  let refBook = await Promise.resolve(db.query(`select unitprice, stock, title FROM books WHERE id = ${refOrder[0][0].bookId}`));

  if(req.body.newstate == 'ready' && refOrder[0][0].state != 'ready')
  {
    //update book stock when books arrived
    if(refBook[0][0].stock == 0){
      await Promise.resolve(db.query(`UPDATE books SET stock = 10 WHERE id = ${refOrder[0][0].bookId}`));
    }

    //updates dispatchedDate
    await Promise.resolve(db.query(`UPDATE orders SET dispatchedDate = CURDATE() WHERE id = ${req.params.orderId}`));
    let updatedOrderDate = await Promise.resolve(db.query(`SELECT dispatchedDate FROM orders WHERE id = ${req.params.orderId}`));

    //TODO sends email 
    let emailContent = prepareEmail(updatedOrderDate[0][0].dispatchedDate, refBook[0][0].title, refOrder[0][0].totalPrice, refBook[0][0].unitprice, refOrder[0][0].quantity);
    sendEmail(refOrder[0][0].clientEmail, 'Your Order', emailContent);
  }

  else if(req.body.newstate == 'sold' && refOrder[0][0].state != 'sold' )
  {
    //client, item, quantity, unitprice, totalprice
    printRecip(refOrder[0][0].clientEmail, refBook[0][0].title, refOrder[0][0].quantity, refBook[0][0].unitprice, refOrder[0][0].totalPrice);
      
  }

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

router.put('/updateOrdersAll', (req, res) => {
  let sql = `UPDATE orders SET state = '${req.body.newstate}', dispatchedDate = '${req.body.dispatchedDate}'  WHERE id = ${req.body.orderId}`;
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
router.get('/getOrdersByEmail', (req, res) => {
  let sql = `SELECT O.id, O.clientEmail, B.title, B.unitprice, O.quantity, O.totalPrice, O.state, O.dispatchedDate FROM orders O INNER JOIN books B ON B.id = O.bookId WHERE clientEmail = '${req.query['email']}'`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    res.send(rows);
  })
  .catch(err => console.log(err));
});

router.get('/getSales', (req, res) => {
  let sql = `SELECT O.id, O.clientEmail, B.title, B.unitprice, O.quantity, O.totalPrice, O.dispatchedDate FROM orders O INNER JOIN books B ON B.id = O.bookId WHERE O.state = 'sold'`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    res.send(rows);
  })
  .catch(err => console.log(err));
});

router.get('/getOrders', (req, res) => {
  let sql = `SELECT O.id, O.clientEmail, B.title, B.unitprice, O.quantity, O.totalPrice, O.state, O.dispatchedDate FROM orders O INNER JOIN books B ON B.id = O.bookId WHERE O.state <> 'sold'`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    res.send(rows);
  })
  .catch(err => console.log(err));
});

// Get Client by name
router.get('/getClients', (req, res) => {
  let sql = `SELECT * FROM clients`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    res.send(rows);
  })
  .catch(err => console.log(err));
});

//Insert Client (name, address, email)
router.post('/insertClient', (req, res) => {
  let sql = `INSERT INTO clients (name, email) VALUES ('${req.body.name}', '${req.body.email}')`;
  db.query(sql, { type: Sequelize.QueryTypes.INSERT }, () => {})
  .then(rows => {
    res.send(rows);
  })
  .catch(err => res.send(err));
});

function printRecip(email, title, qty, unitPrice, totalPrice){

  console.log("chega aqui");

  let putData =  JSON.stringify({
    "email": email,
    "title": title,
    "quantity": qty,
    "unitPrice": unitPrice,
    "totalPrice": totalPrice,
})

  let clientServerOptions = {
    url: 'http://localhost:5002/printRecip',
    body: putData,
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    }
}
request(clientServerOptions, function (error, response) {
    return;
});

}

module.exports = router;