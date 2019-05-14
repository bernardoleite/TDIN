import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: require('@/components/Books').default
    },
    {
      path: '/books',
      name: 'books',
      component: require('@/components/Books').default
    },
    {
      path: '/orders',
      name: 'orders',
      component: require('@/components/Orders').default
    },
    {
      path: '*',
      redirect: '/'
    }
  ]
})
