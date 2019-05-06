const Sequelize = require('sequelize');

module.exports = new Sequelize('storedb', 'postgres', '123', {
  host: 'localhost',
  dialect:  'postgres' 
});