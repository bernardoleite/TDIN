const express = require('express');
const router = express.Router();
const path = require ('path');
const db = require('../../src/database/connection');
const Client = require('../../src/models/Client');
const Sequelize = require('sequelize');
const bcrypt = require('bcryptjs');
const passport = require('passport');
const { ensureAuthenticated } = require('../../config/auth');

router.get('/', (req, res) => res.send('welcome to users api'));

// Login Page
router.get('/login', (req, res) => res.sendFile(path.join(__dirname, '../../public', 'login.html')));

// Register Page
router.get('/register', (req, res) => res.sendFile(path.join(__dirname, '../../public', 'register.html')));

router.get('/dashboard', (req, res) => res.sendFile(path.join(__dirname, '../../public', 'dashboard.html')));

router.get('/about',ensureAuthenticated, (req, res) => res.sendFile(path.join(__dirname, '../../public', 'about.html')));

router.get('/welcome', (req, res) => res.sendFile(path.join(__dirname, '../../public', 'welcome.html')));

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

//Register handler
router.post('/register', (req,res) => {
    const {name, address, email, password, repeatpassword} = req.body;
    let errors = [];

  if (!name || !email || !password || !repeatpassword || !address) {
    errors.push('Please enter all fields');
  }

  if (password != repeatpassword) {
    errors.push('Passwords do not match');
  }

  if (password.length < 6) {
    errors.push('Password must be at least 6 characters');
  }

  if (errors.length > 0) {
    res.status(500).send(errors);
  } 

  else {

  Client.findOne({
    where: {
        email: email
    }
}).then(function(user) {
    // if there are any errors, return the error

    // if user is found, return the message
    if (user){
        res.status(500).send("User already exists.");
    }
    else  {
        const newUser = new Client({
            name,
            address,
            email,
            password
        });

        //Hash Password
        bcrypt.genSalt(10, (err, salt) => {
            bcrypt.hash(newUser.password, salt, (err, hash) => {
              if (err) throw err;
              newUser.password = hash;
              newUser
                .save()
                .then(user => {   
                  //OK, registered - Redirect to Login
                  res.status(200).send([newUser.id, newUser.name, newUser.email, newUser.address]);
                })
                .catch(err => console.log(err));
            });
        });

    }

 
    });

}

});

// Login
router.post('/login', 
    passport.authenticate('local'),
    function(req, res) {
  // If this function gets called, authentication was successful.
  // `req.user` contains the authenticated user.
  res.status(200).send([req.user.id, req.user.name, req.user.email, req.user.address]);
});

  // Logout
  router.get('/logout', function(req, res){
    req.logout();
    res.status(200).send("Logout success.");
});

module.exports = router;