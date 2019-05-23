const express = require('express');
const path = require ('path');
//const myqueue = require("./src/queue");
const passport = require('passport');
const session = require('express-session');
const cors = require('cors');

// DB connection
require("./src/database/connection");

//Populate database
//require("./src/bootstrap")();

const app = express();

// Passport Config
require('./config/passport')(passport);

//Body Parser Middleware
app.use(express.json());
app.use(express.urlencoded({extended: true}));

app.use(cors());

// Express session
app.use(
    session({
      secret: 'secret',
      resave: true,
      saveUninitialized: true
    })
  );

// Passport middleware
app.use(passport.initialize());
app.use(passport.session());

//Homepage Route
//app.get('/', (req,res) => res.render('index'));

//Set static Folder
app.use(express.static(path.join(__dirname,'public')));

//Store API Routes
app.use('/api/store', require('./routes/api/store'));

//Users API Routes
app.use('/api/users', require('./routes/api/users'));


const PORT = process.env.PORT || 5000;

app.listen(PORT, () => console.log(`Server started on port ${PORT}`));