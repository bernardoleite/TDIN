module.exports = async () => {

    const Request = require ("./models/Request");

    const errHandler = (err) => {
        console.error("Error: ", err);
    }

    const request = await Request.create({ 
        orderId: 1,
        bookTitle: 'IT A COISA :)',
        quantity: 2,
        state: 'waiting',
    }).catch(errHandler);



}