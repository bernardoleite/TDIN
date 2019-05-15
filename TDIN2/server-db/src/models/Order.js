const Sequelize = require("sequelize");
var DataTypes = require('sequelize/lib/data-types');

module.exports = sequelize.define("Order", {

    id: {
        type: Sequelize.INTEGER(11),
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
    },
    uuid: {
        type: DataTypes.UUID,
        allowNull: false,
        primaryKey: false,
    },
    clientId: Sequelize.INTEGER(11),
    bookId: Sequelize.INTEGER(11),
    quantity: Sequelize.INTEGER(11),
    totalPrice: Sequelize.FLOAT,
    dispatchedDate: Sequelize.DATE,
    state: Sequelize.STRING(300),
});