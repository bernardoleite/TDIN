'use strict';

module.exports = {
  up: (queryInterface, Sequelize) => {
    return queryInterface.createTable("orders", {
      id: {
        type: Sequelize.INTEGER(11),
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
    },
    quantity: Sequelize.STRING(300),
    product: Sequelize.STRING(300),
    clientId: Sequelize.INTEGER(11),
    createdAt: Sequelize.DATE,
    updatedAt: Sequelize.DATE,
    })
  },
  down: (queryInterface, Sequelize) => {
    return queryInterface.dropTable("orders");
  }
};
