module.exports = async (newRequest) => {

    const Request = require ("./models/Request");

    const errHandler = (err) => {
        console.error("Error: ", err);
    }

    const request = await Request.create({ 
        orderId: 1,
        bookTitle: 'NOVO LIVRO ATRAVÉS DA QUEUE',
        quantity: 2,
        state: 'waiting',
    }).catch(errHandler);



}