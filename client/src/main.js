import Vue from 'vue';
import App from './App.vue';
import { ValidationProvider, ValidationObserver, extend } from 'vee-validate/dist/vee-validate.full.esm';
import router from './router';
import store from './store';
import './global-styles/index.scss';
import 'bootstrap/dist/css/bootstrap.min.css'
import {
	library
} from '@fortawesome/fontawesome-svg-core'
import {
	faTimes,
	faCloudUploadAlt,
	faSave,
	faCheck,
	faCircle,
	faEdit,
	faEye,
	faEyeSlash,
	faPaperPlane,
	faChevronLeft,
	faPlus,
	faCopy,
	faLongArrowAltLeft,
	faTasks,
	faComments,
	faUser,
	faSignOutAlt

} from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import TextareaAutosize from 'vue-textarea-autosize'

library.add(
	faTimes,
	faCloudUploadAlt,
	faSave,
	faCheck,
	faCircle,
	faEdit,
	faEye,
	faEyeSlash,
	faPlus,
	faCopy,
	faPaperPlane,
	faChevronLeft,
	faLongArrowAltLeft,
	faTasks,
	faComments,
	faUser,
	faSignOutAlt
)

Vue.component('font-awesome-icon', FontAwesomeIcon)

Vue.config.productionTip = false;
Vue.use(TextareaAutosize);
Vue.component('ValidationProvider', ValidationProvider);
Vue.component('ValidationObserver', ValidationObserver);

extend('password', {
	params: ['target'],
	validate(value, { target }) {
		return value === target;
	},
	message: 'Password confirmation does not match'
});

new Vue({
	router,
	store,
	render: h => h(App),
}).$mount('#app');
