import Vue from 'vue';
import Router from 'vue-router';

const Home = () => import('./views/Home.vue');
const RegistreSe = () => import('./views/RegistreSe.vue');
const EsqueciMinhaSenha = () => import('./views/EsqueciMinhaSenha.vue');

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    { path: '/', name: 'Home', component: Home },
    { path: '/RegistreSe', name: 'RegistreSe', component: RegistreSe },
    { path: '/EsqueciMinhaSenha', name: 'EsqueciMinhaSenha', component: EsqueciMinhaSenha }
  ]
})
