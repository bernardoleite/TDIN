const Sequelize = require("sequelize");

module.exports = sequelize.define("Request", {

    id: {
        type: Sequelize.INTEGER(11),
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
    },
    orderId: Sequelize.INTEGER(11),
    bookTitle: Sequelize.STRING(300),
    quantity: Sequelize.INTEGER(11),
    state: Sequelize.STRING(300),
});