/*const express = require('express');
const router = express.Router();
const db = require('../../config/database');
const Client = require('../../models/Client');

// Get Client list
router.get('/', (req, res) => 
  Client.findAll()
    .then(clients => {
        console.log(clients);
        res.sendStatus(200);
      })
    .catch(err => console.log(err)));

module.exports = router;*/