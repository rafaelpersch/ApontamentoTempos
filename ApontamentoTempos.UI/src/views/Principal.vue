<template>
    <div>
        <b-navbar toggleable="md" type="dark" variant="info" class="corNene">

            <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>

            <b-navbar-brand :to="{ path: '/Principal'}">Apontamento de Tempos</b-navbar-brand>

            <b-collapse is-nav id="nav_collapse">

                <b-navbar-nav>
                    <b-nav-item :to="{ path: '/Principal/Projetos'}"><font-awesome-icon icon="file" /> Projetos</b-nav-item>
                    <b-nav-item :to="{ path: '#'}"><font-awesome-icon icon="clock" /> Tempos</b-nav-item>
                </b-navbar-nav>

                <!-- Right aligned nav items -->
                <b-navbar-nav class="ml-auto">
                    <b-nav-item-dropdown right>
                        <!-- Using button-content slot -->
                        <template slot="button-content">
                            <em><font-awesome-icon icon="sign-out-alt" />  {{usuario}}</em>
                        </template>
                        <b-dropdown-item v-on:click="Sair"><font-awesome-icon icon="sign-out-alt" /> Sair</b-dropdown-item>
                    </b-nav-item-dropdown>
                </b-navbar-nav>

            </b-collapse>
        </b-navbar>        

        <div class="container-fluid">
            <br>
            <transition name="mytransition">
                <router-view></router-view>
            </transition>
        </div>

    </div>
</template>

<script>
    import SessionService from '../services/SessionService';
    import HttpService from '../services/HttpService.js';

    export default {
        data() {
            return {
                usuario: "",
            }
        },
         methods: {
            Sair() {
                this.sessionService.set(null);
                this.$router.replace({ name: "Home" });
            }
        },
        created() {
            this.sessionService = new SessionService();
            this.httpService = new HttpService(this.$http, this.sessionService);

            if (this.sessionService.get() === null ){
                this.$router.replace({ name: "Home" });         
            }

            this.httpService.get('api/Usuario', false).then(resolve => {
                if (resolve.status == 200){
                    this.usuario = resolve.retorno.nome;
                }else{
                    this.Sair();
                }
            });          
        },
    }
</script>

<style scoped>
    #secure {
        background-color: #FFFFFF;
        border: 1px solid #CCCCCC;
        padding: 20px;
        margin-top: 10px;
    }
    .corNene{
        background-color: #007bff !important;
    }

    .mytransition-enter, .mytransition-leave-active {

        opacity: 0;
    }

    .mytransition-enter-active, .mytransition-leave-active {
        transition: opacity .4s;
    }
</style>