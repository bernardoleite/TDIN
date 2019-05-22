import Vue from 'vue'
import './plugins/vuetify'
import App from './App.vue'
import router from './router'
import Vuetify from 'vuetify'
import VueSession from 'vue-session'

Vue.use(Vuetify, {
  theme: {
    primary: '#45818e',
    secondary: '#cc0000',
    accent: '#76a5af',
    error: '#e06666'
  }
})

Vue.use(VueSession)

Vue.config.productionTip = false

new Vue({
  router,
  render: function (h) { return h(App) }
}).$mount('#app')
