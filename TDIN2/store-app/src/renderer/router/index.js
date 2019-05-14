import Vue from 'vue'
import Router from 'vue-router'

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
      path: '/sales',
      name: 'sales',
      component: require('@/components/Sales').default
    },
    {
      path: '*',
      redirect: '/'
    }
  ]
})
