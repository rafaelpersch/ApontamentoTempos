import Vue from 'vue';
import App from './App.vue';
import vSelect from 'vue-select';
import VueRouter from 'vue-router';
import router from './router';
import BootstrapVue from 'bootstrap-vue';
import VueResource from 'vue-resource';
import VeeValidate from 'vee-validate';
import CxltToastr from 'cxlt-vue2-toastr';
import VueGoogleCharts from 'vue-google-charts';
import msg from './pt_BR';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faSave, faSignOutAlt, faClock, faFile, faPlusSquare, faPencilAlt, faTrash, faUserAlt, faKey } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { ServerTable, ClientTable} from 'vue-tables-2';

import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import 'cxlt-vue2-toastr/dist/css/cxlt-vue2-toastr.css';

library.add(faSave, faSignOutAlt, faClock, faFile, faPlusSquare, faPencilAlt, faTrash, faUserAlt, faKey);

Vue.component('font-awesome-icon', FontAwesomeIcon);
Vue.component('v-select', vSelect);

Vue.use(BootstrapVue);
Vue.use(VueRouter);
Vue.use(VueResource);
Vue.use(ClientTable);
Vue.use(ServerTable);
Vue.use(VueGoogleCharts);

var toastrConfigs = {
  position: 'top full width',
  showDuration: 500,
  delay: 10
};

Vue.use(CxltToastr, toastrConfigs);

if (process.env.NODE_ENV === 'production'){
  Vue.http.options.root = "http://52.67.198.123:81";
}else{
  Vue.http.options.root = "https://localhost:5001";
}
Vue.http.headers.common['Access-Control-Allow-Origin'] = '*';
Vue.http.headers.common['Content-Type'] = 'application/json';

Vue.use(VeeValidate, {
  locale: 'pt_BR',
  dictionary: {
    pt_BR: {
      messages: msg
    }
  }
});

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
