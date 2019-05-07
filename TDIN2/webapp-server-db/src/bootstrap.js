module.exports = async () => {

    const Client = require ("./models/Client");
    const Order = require ("./models/Order");

    Client.hasMany(Order, {as: "Orders", foreignKey: 'clientId'});
    Order.belongsTo(Client, { as: "Client", foreignKey: 'clientId'});

    const errHandler = (err) => {
        console.error("Error: ", err);
    }

    const client = await Client.create({ 
        name: "Bernardo", 
        address: "Rua da Feup" 
    }).catch(errHandler);

    const order = await Order.create({ 
        quantity: "123", 
        product: "Produto 1",
        clientId: client.id,
    }).catch(errHandler);

    const clients = await Client.findAll({ where: { name: 'Bernardo'}, include: [{model: Order, as: "Orders"}]}).catch(errHandler);


    console.log(clients[0].name);
}