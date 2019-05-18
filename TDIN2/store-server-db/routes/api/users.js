const express = require('express');
const router = express.Router();
const path = require ('path');
const db = require('../../src/database/connection');
const Client = require('../../src/models/Client');
const bcrypt = require('bcryptjs');

router.get('/', (req, res) => res.send('welcome to users api'));

// Login Page
router.get('/login', (req, res) => res.sendFile(path.join(__dirname, '../../public', 'login.html')));

// Register Page
router.get('/register', (req, res) => res.sendFile(path.join(__dirname, '../../public', 'register.html')));


//Register handler
router.post('/register', (req,res) => {
    const {name, email, password, password2} = req.body;
    let errors = [];
    console.log(req.body);

  if (!name || !email || !password || !password2) {
    errors.push({ msg: 'Please enter all fields' });
  }

  if (password != password2) {
    errors.push({ msg: 'Passwords do not match' });
  }

  if (password.length < 6) {
    errors.push({ msg: 'Password must be at least 6 characters' });
  }

  if (errors.length > 0) {
    res.sendStatus(500);
  } 

  Client.findOne({
    where: {
        email: email
    }
}).then(function(user) {
    // if there are any errors, return the error

    // if user is found, return the message
    if (user){
        res.sendStatus(500);
    }
    else  {
        const newUser = new Client({
            name,
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
                   res.sendStatus(200);
                })
                .catch(err => console.log(err));
            });
        });

    }

 
    });

});

module.exports = router;