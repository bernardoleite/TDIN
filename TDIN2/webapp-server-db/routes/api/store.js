const express = require('express');
const router = express.Router();
const db = require('../../src/database/connection');
const Client = require('../../src/models/Client');

// Get Client list
router.get('/', (req, res) => 
  Client.findAll()
    .then(clients => {
        console.log(clients[0].name);
        res.sendStatus(200);
      })
    .catch(err => console.log(err)));

module.exports = router;