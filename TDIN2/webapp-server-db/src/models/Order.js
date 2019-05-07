const Sequelize = require("sequelize");

module.exports = sequelize.define("Order", {

    id: {
        type: Sequelize.INTEGER(11),
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
    },
    quantity: Sequelize.STRING(300),
    product: Sequelize.STRING(300),
    clientId: Sequelize.INTEGER(11),

});