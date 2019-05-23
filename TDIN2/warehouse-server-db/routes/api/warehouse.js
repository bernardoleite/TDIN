const express = require('express');
const router = express.Router();
const db = require('../../src/database/connection');
const Request = require('../../src/models/Request');
const Sequelize = require('sequelize');
var request = require('request');

//Get All Books
router.get('/getAllPendingRequests', (req, res) => {
  let sql = `SELECT * FROM requests WHERE state='pending'`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    if(rows.length == 0) 
      res.sendStatus(404)
    else 
      res.send(rows);
  })
  .catch(err => res.send(err));
});

//Insert Request (orderId, bookTitle, quantity, state)
router.post('/insertRequest', (req, res) => {
    let sql = `INSERT INTO requests (orderId, bookTitle, quantity, state) VALUES (${req.body.orderId}, '${req.body.bookTitle}', ${req.body.quantity}, '${req.body.state}' )`;
    db.query(sql, { type: Sequelize.QueryTypes.INSERT }, () => {})
    .then(rows => {
      res.sendStatus(200);
    })
    .catch(err => res.send(err));
  });

//Update Request State by orderId
router.put('/updateRequestStateByOrderId/:orderId', (req, res) => {
    let sql = `UPDATE requests SET state = '${"shipped"}' WHERE orderId = ${req.params.orderId}`;
    db.query(sql,  {})
    .then(rows => {
      if(rows[0].affectedRows == 0){ 
        updateStore(req.params.orderId);
        res.send("Nothing to change")
      }
      else if(rows[0].affectedRows == 1){
        //CALL STORE API - UPDATE
        updateStore(req.params.orderId);
        res.sendStatus(200);
      }
    })
    .catch(err => res.send(err));
  });

//Update Request State by internal Id
router.put('/updateRequestStateById/:id', (req, res) => {
    let sql = `UPDATE requests SET state = '${"shipped"}' WHERE id = ${req.params.id}`;
    db.query(sql,  {})
    .then(rows => {
      if(rows[0].affectedRows == 0) 
        res.sendStatus(404)
      else if(rows[0].affectedRows == 1)
        res.sendStatus(200);
    })
    .catch(err => res.send(err));
  });

  function updateStore(orderId){

    let dispatchedDate = new Date();
    dispatchedDate.setDate(dispatchedDate.getDate()+2);
  
    let putData =  JSON.stringify({
      "newstate": "dispatched",
      "dispatchedDate": dispatchedDate,
      "orderId": orderId,
  })
  
    let clientServerOptions = {
      url: 'http://localhost:5000/api/store/updateOrdersAll',
      body: putData,
      method: 'PUT',
      headers: {
          'Content-Type': 'application/json'
      }
  }
  request(clientServerOptions, function (error, response) {
    console.log("chega aqui");
      console.log(error,response.body);
      return;
  });

  }








module.exports = router;