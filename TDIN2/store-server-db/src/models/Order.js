const Sequelize = require("sequelize");
var DataTypes = require('sequelize/lib/data-types');
const uuidv1 = require('uuid/v1');

module.exports = sequelize.define("Order", {

    id: {
        type: Sequelize.INTEGER(11),
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
    },
    uuid: {
        type: DataTypes.UUID,
        defaultValue: uuidv1(),
        allowNull: false,
        primaryKey: false,
    },
    clientEmail: Sequelize.STRING(300),
    bookId: Sequelize.INTEGER(11),
    quantity: Sequelize.INTEGER(11),
    totalPrice: Sequelize.FLOAT,
    dispatchedDate: Sequelize.DATE,
    state: Sequelize.STRING(300),
});