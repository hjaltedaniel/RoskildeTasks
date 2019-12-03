import Vue from 'vue';
import VueRouter from 'vue-router';
import Tasks from '../views/Tasks';
import TaskDetails from '../views/TaskDetails';
import Messages from '../views/Messages';
import Profile from '../views/Profile';
import Resources from '../views/Resources';
import ResourceFileList from '../components/ResourceFileList';
import ResourceTabs from '../components/ResourceTabs';

Vue.use(VueRouter);

const routes = [
	{
		path: '/',
		name: 'home',
		component: Tasks,
	},
	{
		path: '/tasks',
		name: 'tasks',
		component: Tasks,
	},
	{
		path: '/tasks/:id',
		name: 'tasks.task-details',
		component: TaskDetails,
	},
	{
		path: '/messages',
		name: 'Messages',
		component: Messages,
	},
	{
		path: '/resources',
		name: 'Resources',
		component: Resources,
		children: [
			{
				path: ':resourcelist',
				component: ResourceFileList
			},
		]
	},
	{
		path: '/profile',
		name: 'Profile',
		component: Profile,
	}
];

const router = new VueRouter({
	mode: 'hash',
	// exact: true,
  	routes,
});

export default router;
