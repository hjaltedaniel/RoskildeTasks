import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import './global-styles/index.scss';
import 'bootstrap/dist/css/bootstrap.min.css'
import { library } from '@fortawesome/fontawesome-svg-core'
import { 
	faTimes,
	faCloudUploadAlt,
	faSave,
	faCheck,
	faEdit,
	faEye,
	faEyeSlash,
	faPlus,
	faCopy,
	faLongArrowAltLeft
} from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

library.add(
	faTimes,
	faCloudUploadAlt,
	faSave,
	faCheck,
	faEdit,
	faEye,
	faEyeSlash,
	faPlus,
	faCopy,
	faLongArrowAltLeft
)

Vue.component('font-awesome-icon', FontAwesomeIcon)

Vue.config.productionTip = false;

new Vue({
	router,
	store,
  render: h => h(App),
}).$mount('#app');
