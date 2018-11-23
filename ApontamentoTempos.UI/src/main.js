import Vue from 'vue';
import App from './App.vue';
import VueRouter from 'vue-router';
import router from './router';
import BootstrapVue from 'bootstrap-vue';
import VueResource from 'vue-resource';
import VeeValidate from 'vee-validate';
import CxltToastr from 'cxlt-vue2-toastr';
import msg from './pt_BR';

import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import 'cxlt-vue2-toastr/dist/css/cxlt-vue2-toastr.css';

var toastrConfigs = {
  position: 'top full width',
  showDuration: 500,
  delay: 10
};

Vue.use(BootstrapVue);
Vue.use(VueRouter);
Vue.use(VueResource);
Vue.use(CxltToastr, toastrConfigs);
Vue.use(VeeValidate, {
  locale: 'pt_BR',
  dictionary: {
    pt_BR: {
      messages: msg
    }
  }
});

Vue.http.options.root = 'http://localhost:62137';
Vue.http.headers.common['Access-Control-Allow-Origin'] = '*'

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
