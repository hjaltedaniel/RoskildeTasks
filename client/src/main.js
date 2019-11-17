import Vue from 'vue';
import App from './App.vue';
import router from './router';
import './global-styles/index.scss';
import 'bootstrap/dist/css/bootstrap.min.css'

Vue.config.productionTip = false;

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');
