const express = require('express');
const path = require ('path');
const exphbs = require('express-handlebars');


// DB connection
require("./src/database/connection");

require("./src/bootstrap")();

require("./src/queue")("a minha variavel");

const app = express();


//Handlebars middleware
app.engine('handlebars', exphbs({defaultLayout: 'main'}));
app.set('view engine', 'handlebars');

//Body Parser Middleware
app.use(express.json());
app.use(express.urlencoded({extended: false}));

//Set static Folder
//app.use(express.static(path.join(__dirname,'public')));

//Store API Routes
app.use('/api/store', require('./routes/api/store'));


//Homepage Route
// app.get('/', (req,res) => res.render('index'));

//Members API Routes
// app.use('/api/members', require('./routes/api/members'));

// app.get ('/index', (req,res) => {
//     res.sendFile(path.join(__dirname, 'public', 'index.html'));
// });

// app.get ('/about', (req,res) => {
//     res.sendFile(path.join(__dirname, 'public', 'about.html'));
// });



const PORT = process.env.PORT || 5000;

app.listen(PORT, () => console.log(`Server started on port ${PORT}`));