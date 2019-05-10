const Sequelize = require("sequelize");


module.exports = sequelize.define("Client", {

    id: {
        type: Sequelize.INTEGER(11),
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
    },
    name: Sequelize.STRING(300),
    address: Sequelize.STRING(300),
    email: {
        type: Sequelize.STRING(300),
        isEmail: true,
        allowNull: false,
    }

});