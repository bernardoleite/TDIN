module.exports = async () => {

    const Client = require ("./models/Client");
    const Order = require ("./models/Order");
    const Book = require ("./models/Book");
    const uuidv1 = require('uuid/v1');

    Client.hasMany(Order, {as: "Orders", foreignKey: 'clientEmail'});
    Order.belongsTo(Client, { as: "Client", foreignKey: 'clientEmail'});

    Order.hasMany(Book, {as: "Books", foreignKey: 'bookId'});
    Book.belongsTo(Order, { as: "Order", foreignKey: 'bookId'});

    const errHandler = (err) => {
        console.error("Error: ", err);
    }

    
    let emailR = Math.random().toString(36).substring(7);
    emailR = emailR + '@gmail.com';

    const client = await Client.create({ 
        name: "Bernardo", 
        email: emailR,
        password: 'fsmf4wfsdb2',
        address: "Rua da Feup",
    }).catch(errHandler);

    const book = await Book.create({ 
        title: "IT - A Coisa",
        stock: 3,
        unitprice: 19.99,
    }).catch(errHandler);

    const book1 = await Book.create({ 
        title: "The Girl With the Dragon Tattoo",
        stock: 2,
        unitprice: 29.99,
    }).catch(errHandler);

    const book2 = await Book.create({ 
        title: "The Girl Who Dreamed With Fire",
        stock: 5,
        unitprice: 10.12,
    }).catch(errHandler);

    const order = await Order.create({ 
        uuid: uuidv1(),
        clientEmail: client.email,
        bookId: book.id,
        quantity: 2,
        totalPrice: 13.4,
        //dispatchedDate: Sequelize.DATE,
        state: "waiting",
    }).catch(errHandler);

    const order1 = await Order.create({ 
        uuid: uuidv1(),
        clientEmail: client.email,
        bookId: book.id,
        quantity: 2,
        totalPrice: 13.4,
        //dispatchedDate: Sequelize.DATE,
        state: "dispatched",
    }).catch(errHandler);

    const order2 = await Order.create({ 
        uuid: uuidv1(),
        clientEmail: client.email,
        bookId: book.id,
        quantity: 2,
        totalPrice: 13.4,
        //dispatchedDate: Sequelize.DATE,
        state: "ready",
    }).catch(errHandler);

    const sale = await Order.create({ 
        uuid: uuidv1(),
        clientEmail: client.email,
        bookId: book.id,
        quantity: 2,
        totalPrice: 13.4,
        //dispatchedDate: Sequelize.DATE,
        state: "sold",
    }).catch(errHandler);

    const clients = await Client.findAll({ where: { name: 'Bernardo'}, include: [{model: Order, as: "Orders"}]}).catch(errHandler);

}