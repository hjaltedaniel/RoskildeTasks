import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '../views/Home';
import Tasks from '../views/Tasks';
import Messages from '../views/Messages';
import Profile from '../views/Profile';
import Resources from '../views/Resources'

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'home',
	component: Home,
	exact: true
  },
  {
    path: '/tasks',
    name: 'tasks',
	component: Tasks,
	exact: true
  },
  {
    path: '/messages',
    name: 'Messages',
	component: Messages,
	exact: true
  },
  {
    path: '/resources',
    name: 'Resources',
	component: Resources,
	exact: true
  },
  {
	  path: '/profile',
	  name: 'Profile',
	  component: Profile,
	  exact: true
  }
];

const router = new VueRouter({
  routes,
});

export default router;
