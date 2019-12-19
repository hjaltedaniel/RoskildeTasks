import Vue from 'vue';
import VueRouter from 'vue-router';
import Tasks from '../views/Tasks';
import TaskDetails from '../views/TaskDetails';
import Messages from '../views/Messages';
import Profile from '../views/Profile';
import Resources from '../views/Resources';
import ResourceFileList from '../components/ResourceFileList';
import Login from '../views/Login';
import { authenticationRouteGuard } from './guards/authenticationRouteGuard';
import { protectedRouteGuard } from './guards/protectedRouteGuard';

Vue.use(VueRouter);

const routes = [
	{
		path: '/',
		name: 'home',
		component: Tasks,
		beforeEnter: protectedRouteGuard,
	},
	{
		path: '/tasks',
		name: 'tasks',
		component: Tasks,
		beforeEnter: protectedRouteGuard,
	},
	{
		path: '/tasks/:id',
		name: 'tasks.task-details',
		component: TaskDetails,
		beforeEnter: protectedRouteGuard,
	},
	{
		path: '/messages',
		name: 'Messages',
		component: Messages,
		beforeEnter: protectedRouteGuard,
	},
	{
		path: '/resources',
		name: 'Resources',
		component: Resources,
		beforeEnter: protectedRouteGuard,
		children: [
			{
				path: ':resourcelist',
				component: ResourceFileList
			},
		]
	},
	{
		path: '/login',
		name: 'Login',
		component: Login,
		beforeEnter: authenticationRouteGuard
	},
	{
		path: '/profile',
		name: 'Profile',
		component: Profile,
		beforeEnter: protectedRouteGuard,
	}
];

const router = new VueRouter({
	mode: 'hash',
	// exact: true,
  	routes,
});

export default router;
