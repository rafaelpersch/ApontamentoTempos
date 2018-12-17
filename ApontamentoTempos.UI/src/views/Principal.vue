<template>
    <div>
        <b-navbar toggleable="md" type="dark" variant="info" class="corNene">

            <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>

            <b-navbar-brand href="#">Apontamento de Tempos</b-navbar-brand>

            <b-collapse is-nav id="nav_collapse">

                <b-navbar-nav>
                    <b-nav-item href="#">Projetos</b-nav-item>
                    <b-nav-item href="#">Tempos</b-nav-item>
                </b-navbar-nav>

                <!-- Right aligned nav items -->
                <b-navbar-nav class="ml-auto">
                    <b-nav-item-dropdown right>
                        <!-- Using button-content slot -->
                        <template slot="button-content">
                            <em>{{usuario}}</em>
                        </template>
                        <b-dropdown-item v-on:click="Sair">Sair</b-dropdown-item>
                    </b-nav-item-dropdown>
                </b-navbar-nav>

            </b-collapse>
        </b-navbar>        

        <b-container fluid>
            <b-row class="my-1">
                <b-col sm="6">
                    <b-col sm="2">
                        <label for="input-default">Default:</label>
                    </b-col>
                    <b-col sm="10">
                        <b-form-input id="input-default" type="text" placeholder="Enter your name"/>
                    </b-col>
                </b-col>
                <b-col sm="6">
                    <b-col sm="2">
                        <label for="input-default">Default:</label>
                    </b-col>
                    <b-col sm="10">
                        <b-form-input id="input-default" type="date" placeholder="Enter your name"/>
                    </b-col>
                </b-col>
                 <b-col sm="6">
                    <b-col sm="2">
                        <label for="input-default">Select 2:</label>
                    </b-col>
                    <b-col sm="10">
                        <v-select v-model="selected" :options="['foo','bar']"></v-select>
                    </b-col>
                </b-col>
                <b-col sm="6">
                    <b-col sm="2">
                        <label for="input-default">Spinner:</label>
                    </b-col>
                    <b-col sm="10">
                        <clip-loader></clip-loader>       
                    </b-col>
                </b-col>
                <b-col sm="6">
                    <b-col sm="2">
                        <label for="input-default">Datatables:</label>
                    </b-col>
                    <b-col sm="10">
                        Falta fazer...
                    </b-col>
                </b-col>
            </b-row>
            <b-row class="my-1">
                <b-button @click="showModal">
                    Open Modal
                </b-button>
                <b-modal ref="myModalRef" hide-footer title="Using Component Methods">
                    <div class="d-block text-center">
                        <h3>Hello From My Modal!</h3>
                    </div>
                    <b-btn class="mt-3" variant="outline-danger" block @click="hideModal">
                        Close Me
                    </b-btn>
                </b-modal>
            </b-row>
        </b-container>

        <router-link :to="{ path: '/secure/recoverpassword'}">
            <a>child</a>
        </router-link>



        <router-view></router-view>

    </div>
</template>

<script>
    import Vue from 'vue';  
    import ClipLoader from 'vue-spinner/src/ClipLoader.vue';
    import vSelect from 'vue-select';

    import SessionService from '../services/SessionService';
    Vue.component('v-select', vSelect);

    export default {
        components: {
            ClipLoader 
        },
        data() {
            return {
                usuario: "",
                selected: null
            }
        },
         methods: {
            Sair() {
                this.$router.replace({ name: "Sair" });
            },
            showModal () {
                this.$refs.myModalRef.show()
            },
            hideModal () {
                this.$refs.myModalRef.hide()
            }
        },
        created() {
            this.sessionService = new SessionService();

            if (this.sessionService.get() === null ){
                this.$router.replace({ name: "Home" });         
            }

            this.$http.get('api/Usuario/' + this.sessionService.get().uid, { headers: { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken }}).then(res => {

                if (res.status == 200){
                    this.usuario = res.body.nome;                          
                }else{
                    this.$toast.error({
                        title:'Ops!',
                        message: res.body,
                    });                              
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
</style>