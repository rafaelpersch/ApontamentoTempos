<template>
    <div>
        <div class="text-center login-back">
            <form class="form-signin" v-on:keyup.enter="recuperarSenha()">
                <img class="mb-4" src="..\assets\stopwatch.png" alt="" width="75" height="75">
                <h1 class="h3 mb-3 font-weight-normal">Apontamento de Tempos</h1>
                <br>
                <span class="erro" v-show="errors.has('email')">{{ errors.first('email') }}</span>
                <div class="col text-left">
                    <h5>Senha</h5>
                </div>
                <input id="senha" class="form-control" placeholder="Senha" required type="password" name="senha" v-model="input.senha" v-validate data-vv-rules="required" ref="senha">
                <span class="erro" v-show="errors.has('senha')">{{ errors.first('senha') }}</span>
                <div class="col text-left">
                    <h5>Repetir Senha</h5>
                </div>
                <input id="repetirSenha" class="form-control" placeholder="Repetir Senha" required type="password" name="repetirSenha" v-model="input.repetirSenha" v-validate data-vv-rules="required|confirmed:senha">                        
                <span class="erro" v-show="errors.has('repetirSenha')">{{ errors.first('repetirSenha') }}</span>
                <button class="btn btn-lg btn-primary btn-block" type="button" v-on:click="recuperarSenha()" :disabled="input.disable">Recuperar Senha</button>
                <br>
                <b-col sm="12" v-if="input.disable">
                    <clip-loader></clip-loader>       
                </b-col>
                <div class="col text-right">
                    <router-link to="/" class="badge">Voltar</router-link>
                </div>                        
                <p class="mt-2 text-muted">Desenvolvido por Nene (Rafael Persch)</p>
            </form>
        </div>
    </div>
    
</template>

<script>

    import SessionService from '../services/SessionService';
    import HttpService from '../services/HttpService.js';
    import ClipLoader from 'vue-spinner/src/ClipLoader.vue';

    export default {
        props: ['id'],
        components: {
            ClipLoader 
        },
        data() {
            return {
                input: {
                    usuarioId: "",
                    senha: "",
                    repetirSenha: "",
                    disable: false
                }
            }
        },
        methods: {
            recuperarSenha(){
                this.$validator.validateAll().then(success => {
                    if(success) {

                        this.input.disable = true;

                        this.httpService.put('api/RecuperacaoSenha', this.id, { Id: this.id, Senha : this.input.senha }, true).then(resolve => {
                            if (resolve.status == 200){
                                this.$toast.success({
                                    title:'Success',
                                    message: "Senha alterada !",
                                });
                                
                                this.input.disable = false;

                                this.$router.replace({ name: "Home" });
                            }else{
                                this.input.disable = false;

                                this.$toast.error({
                                    title:'Ops!',
                                    message: resolve.retorno,
                                });
                            }
                        });               
                    }
                });  
            },
        },
        created() {

            this.sessionService = new SessionService();
            this.httpService = new HttpService(this.$http, this.sessionService);

            if (this.sessionService.get() !== null ){
                this.$router.replace({ name: "Principal" });         
            }
        },
    }
</script>

<style scoped>

    .login-back {
        display: -ms-flexbox;
        display: -webkit-box;
        display: flex;
        -ms-flex-align: center;
        -ms-flex-pack: center;
        -webkit-box-align: center;
        align-items: center;
        -webkit-box-pack: center;
        justify-content: center;
        padding-top: 40px;
        padding-bottom: 40px;
        background-color: #f5f5f5;
    }

    .form-signin {
        width: 100%;
        max-width: 330px;
        padding: 15px;
        margin: 0 auto;
    }
    .form-signin .checkbox {
        font-weight: 400;
    }
    .form-signin .form-control {
        position: relative;
        box-sizing: border-box;
        height: auto;
        padding: 10px;
        font-size: 16px;
    }

    .erro {
        color: red;
    } 

</style>