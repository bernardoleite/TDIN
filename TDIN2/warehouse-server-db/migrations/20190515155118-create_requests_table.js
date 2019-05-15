'use strict';

module.exports = {
  up: (queryInterface, Sequelize) => {
    return queryInterface.createTable("requests", {
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
    createdAt: Sequelize.DATE,
    updatedAt: Sequelize.DATE,
    })
  },
  down: (queryInterface, Sequelize) => {
    return queryInterface.dropTable("requests");
  }
};
