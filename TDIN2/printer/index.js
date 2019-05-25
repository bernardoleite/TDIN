const express = require('express');
var cors = require('cors');

const app = express();

app.use(cors());

//Body Parser Middleware
app.use(express.json());
app.use(express.urlencoded({extended: false}));


// Prins recipe
app.post('/printRecip', function (req, res) {
  console.log('\n', '----------------------------------------')
  console.log('Recipe for: ', req.body.email, '\n');
  console.log('Product: ', req.body.title);
  console.log('Units ', req.body.quantity, '\n');
  console.log('Unit Price: ', req.body.unitPrice, '€');
  console.log('Total Price: ', req.body.totalPrice, '€');
  console.log('----------------------------------------')
  res.sendStatus(200);
});

const PORT = process.env.PORT || 5002;

app.listen(PORT, () => console.log(`Printer started on port ${PORT}`));