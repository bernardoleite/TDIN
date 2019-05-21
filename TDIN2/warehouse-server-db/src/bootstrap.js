module.exports = async () => {

    const Request = require ("./models/Request");

    const errHandler = (err) => {
        console.error("Error: ", err);
    }
    const request = await Request.create({ 
        orderId: 1,
        bookTitle: 'IT - A Coisa',
        quantity: 7,
        state: 'waiting',
    }).catch(errHandler);

    const request2 = await Request.create({ 
        orderId: 8,
        bookTitle: 'The Girl With the Dragon Tattoo',
        quantity: 2,
        state: 'waiting',
    }).catch(errHandler);

    const request3 = await Request.create({ 
        orderId: 7,
        bookTitle: 'The Girl Who Played With Fire',
        quantity: 2,
        state: 'waiting',
    }).catch(errHandler);
}