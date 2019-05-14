'use strict';

const DataTypes = require('sequelize/lib/data-types');

module.exports = {
  up: (queryInterface, Sequelize) => {
    return queryInterface.createTable("orders", {
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
    createdAt: Sequelize.DATE,
    updatedAt: Sequelize.DATE,
    })
  },
  down: (queryInterface, Sequelize) => {
    return queryInterface.dropTable("orders");
  }
};
