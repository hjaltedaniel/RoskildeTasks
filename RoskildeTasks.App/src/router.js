import Vue from 'vue'
import Router from 'vue-router'
import Tasks from './views/Tasks.vue'
import Messages from './views/Messages.vue'
import Ressources from './views/Ressources.vue'
import Profile from './views/Profile.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [{
      path: '/',
      name: 'tasks',
      component: Tasks
    },
    {
      path: '/messages',
      name: 'messages',
      component: Messages
    },
    {
      path: '/ressources',
      name: 'ressources',
      component: Ressources
    },
    {
      path: '/profile',
      name: 'profile',
      component: Profile
    }
  ]
})