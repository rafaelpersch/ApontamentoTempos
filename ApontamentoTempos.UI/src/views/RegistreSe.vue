<template>
    <div>
        <div class="text-center login-back">
            <form class="form-signin" v-on:keyup.enter="cadastreSe()">
                <img class="mb-4" src="..\assets\stopwatch.png" alt="" width="75" height="75">
                <h1 class="h3 mb-3 font-weight-normal">Apontamento de Tempos</h1>
                <br>
                <div class="col text-left">
                    <h5>Nome</h5>
                </div>            
                <input id="nome" class="form-control" placeholder="Nome" required name="nome" v-model="input.nome" v-validate data-vv-rules="required">
                <span class="erro" v-show="errors.has('nome')">{{ errors.first('nome') }}</span>
                <div class="col text-left">
                    <h5>Email</h5>
                </div>            
                <input id="email" class="form-control" placeholder="E-mail" required type="email" name="email" v-model="input.email" v-validate data-vv-rules="required|email">
                <span class="erro" v-show="errors.has('email')">{{ errors.first('email') }}</span>
                <div class="col text-left">
                    <h5>Senha</h5>
                </div>
                <input id="senha" class="form-control" placeholder="Senha" required type="password" name="senha" v-model="input.senha" v-validate data-vv-rules="required" ref="senha">
                <span class="erro" v-show="errors.has('senha')">{{ errors.first('senha') }}</span>
                <div class="col text-left">
                    <h5>Repeat Password:</h5>
                </div>
                <input id="repetirSenha" class="form-control" placeholder="Repetir Senha" required type="password" name="repetirSenha" v-model="input.repetirSenha" v-validate data-vv-rules="required|confirmed:senha">                        
                <span class="erro" v-show="errors.has('repetirSenha')">{{ errors.first('repetirSenha') }}</span>
                <button class="btn btn-lg btn-primary btn-block" type="button" v-on:click="cadastreSe()" :disabled="input.disable">Cadastre-se</button>
                <br>
                <b-col sm="12" v-if="input.disable">
                    <clip-loader></clip-loader>       
                </b-col>
                <div class="col text-right">
                    <router-link to="/" class="badge">Voltar</router-link>
                </div>                        
                <p class="mt-5 mb-3 text-muted">Desenvolvido por Nene (Rafael Persch)</p>
            </form>
        </div>
    </div>
    
</template>

<script>
    import SessionService from '../services/SessionService';
    import ClipLoader from 'vue-spinner/src/ClipLoader.vue';
    
    export default {
        components: {
            ClipLoader 
        },
        data() {
            return {
                input: {
                    nome: "",
                    email: "",
                    senha: "",
                    repetirSenha: "",
                    disable: false
                },
            }
        },
        methods: {
            cadastreSe(){
                this.$validator.validateAll().then(success => {
                    if(success) {
                        
                        this.input.disable = true;

                        let user = { 
                            Nome: this.input.nome, 
                            Email: this.input.email, 
                            Senha: this.input.senha 
                        };

                        this.$http.post('api/Usuario', user).then(res => {

                            this.$toast.success({
                                title:'Success',
                                message: "Usuário registrado com sucesso!",
                            });                                
                            
                            this.input.disable = false;

                            this.$router.replace({ name: "Home" });

                        }, err => {

                            this.input.disable = false;

                            this.$toast.error({
                                title:'Ops!',
                                message: err.body,
                            });
                        });
                    }
                });
            }
        },
        created() {
            this.sessionService = new SessionService();

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