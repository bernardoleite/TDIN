const Sequelize = require("sequelize");
var DataTypes = require('sequelize/lib/data-types');

module.exports = sequelize.define("Order", {

    id: {
        type: DataTypes.UUID,
        allowNull: false,
        primaryKey: true,
    },
    clientId: Sequelize.INTEGER(11),
    bookId: Sequelize.INTEGER(11),
    quantity: Sequelize.INTEGER(11),
    totalPrice: Sequelize.FLOAT,
    dispatchedDate: Sequelize.DATE,
    state: Sequelize.STRING(300),
});