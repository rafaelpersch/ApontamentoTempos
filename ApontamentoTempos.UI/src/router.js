import Vue from 'vue';
import Router from 'vue-router';

const Home = () => import('./views/Home.vue');
const RegistreSe = () => import('./views/RegistreSe.vue');
const EsqueciMinhaSenha = () => import('./views/EsqueciMinhaSenha.vue');
const RecuperacaoSenha = () => import('./views/RecuperacaoSenha.vue');
const Principal = () => import('./views/Principal.vue');
const Projeto = () => import('./views/Projeto.vue');
const Projetos = () => import('./views/Projetos.vue');
const Tempo = () => import('./views/Tempo.vue');
const Tempos = () => import('./views/Tempos.vue');
const Dashboard = () => import('./views/Dashboard.vue');
const TrocaSenha = () => import('./views/TrocaSenha.vue');

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    { path: '/', name: 'Home', component: Home },
    { path: '/RegistreSe', name: 'RegistreSe', component: RegistreSe },
    { path: '/EsqueciMinhaSenha', name: 'EsqueciMinhaSenha', component: EsqueciMinhaSenha },
    { path: '/RecuperacaoSenha/:id', name: 'RecuperacaoSenha', component: RecuperacaoSenha, props: true  },
    { path: "/Principal", name: "Principal", component: Principal, 
      children: [{ path: 'Dashboard', name: 'Dashboard', component: Dashboard }, 
                 { path: 'TrocaSenha', name: 'TrocaSenha', component: TrocaSenha }, 
                 { path: 'Projeto', component: Projeto }, 
                 { path: 'Projeto/:id', component: Projeto, props: true }, 
                 { path: 'Projetos', component: Projetos },
                 { path: 'Tempo', component: Tempo },
                 { path: 'Tempo/:id', component: Tempo, props: true }, 
                 { path: 'Tempos', component: Tempos } ] 
    },
  ]
})
