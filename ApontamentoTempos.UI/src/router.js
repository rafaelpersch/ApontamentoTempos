import Vue from 'vue';
import Router from 'vue-router';

const Home = () => import('./views/Home.vue');
const RegistreSe = () => import('./views/RegistreSe.vue');
const EsqueciMinhaSenha = () => import('./views/EsqueciMinhaSenha.vue');
const Principal = () => import('./views/Principal.vue');
const Projeto = () => import('./views/Projeto.vue');
const Projetos = () => import('./views/Projetos.vue');
const Tempo = () => import('./views/Tempo.vue');
const Tempos = () => import('./views/Tempos.vue');

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    { path: '/', name: 'Home', component: Home },
    { path: '/RegistreSe', name: 'RegistreSe', component: RegistreSe },
    { path: '/EsqueciMinhaSenha', name: 'EsqueciMinhaSenha', component: EsqueciMinhaSenha },
    { path: "/Principal", name: "Principal", component: Principal, 
      children: [{ path: 'Projeto', component: Projeto }, 
                 { path: 'Projeto/:id', component: Projeto, props: true }, 
                 { path: 'Projetos', component: Projetos },
                 { path: 'Tempo', component: Tempo },
                 { path: 'Tempos', component: Tempos } ] 
    },
  ]
})
