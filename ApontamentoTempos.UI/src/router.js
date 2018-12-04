import Vue from 'vue';
import Router from 'vue-router';

const Home = () => System.import('./views/Home.vue');
const RegistreSe = () => System.import('./views/RegistreSe.vue');
const EsqueciMinhaSenha = () => System.import('./views/EsqueciMinhaSenha.vue');

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    { path: '/', name: 'Home', component: Home },
    { path: '/RegistreSe', name: 'RegistreSe', component: RegistreSe },
    { path: '/EsqueciMinhaSenha', name: 'EsqueciMinhaSenha', component: EsqueciMinhaSenha }
  ]
})
