const express = require('express');
const router = express.Router();
const path = require ('path');
const db = require('../../src/database/connection');
const Client = require('../../src/models/Client');
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
                  res.status(200).send("Register success.");
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
  res.status(200).send(req.user);
});

  // Logout
  router.get('/logout', function(req, res){
    req.logout();
    res.status(200).send("Logout success.");
});

module.exports = router;