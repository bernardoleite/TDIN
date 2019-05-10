const Sequelize = require("sequelize");


module.exports = sequelize.define("Book", {

    id: {
        type: Sequelize.INTEGER(11),
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
    },
    title: Sequelize.STRING(300),
    stock: Sequelize.INTEGER(11),
    price: Sequelize.FLOAT,

});