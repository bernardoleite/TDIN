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

// Get Client by id
router.get('/getclientById/:id', (req, res) => {
  let sql = `SELECT * FROM clients WHERE id = ${req.params.id}`;
  db.query(sql, { type: Sequelize.QueryTypes.SELECT }, () => {})
  .then(rows => {
    res.send(rows);
  })
  .catch(err => console.log(err));
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