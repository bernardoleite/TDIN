const LocalStrategy = require('passport-local').Strategy;
const bcrypt = require('bcryptjs');

// Load Client model
const Client = require('../src/models/Client');

module.exports = function(passport) {
  passport.use(
    new LocalStrategy({ usernameField: 'email' }, (email, password, done) => {
      // Match user
      Client.findOne({
        where: {
            email: email
        }
      }).then(user => {
        if (!user) {
          return done(null, false, { message: 'That email is not registered' });
        }

        // Match password
        bcrypt.compare(password, user.password, (err, isMatch) => {
          if (err) throw err;
          if (isMatch) {
            return done(null, user);
          } else {
            return done(null, false, { message: 'Password incorrect' });
          }
        });
      });
    })
  );

  passport.serializeUser(function(user, done) {
    done(null, user.id);
  });

  passport.deserializeUser(function(id, done) {
    Client.findByPk(id).then(function(user) {
      done(null, user);
    }).catch(function(err) {
      if (err) {
        throw err;
      }
   });
  });

};



