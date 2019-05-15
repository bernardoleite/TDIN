const express = require('express');
const path = require ('path');
const exphbs = require('express-handlebars');

// DB connection
require("./src/database/connection");

require("./src/bootstrap")();


const app = express();

//Body Parser Middleware
app.use(express.json());
app.use(express.urlencoded({extended: false}));

//Warehouse API Routes
app.use('/api/warehouse', require('./routes/api/warehouse'));

const PORT = process.env.PORT || 5001;

app.listen(PORT, () => console.log(`Server started on port ${PORT}`));