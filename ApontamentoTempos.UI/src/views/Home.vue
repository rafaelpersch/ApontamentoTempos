<template>
    <div>
        <div class="text-center login-back">
            <form class="form-signin"  v-on:keyup.enter="login()">
                <img class="mb-4" src="..\assets\stopwatch.png" alt="" width="75" height="75">
                <h1 class="h3 mb-3 font-weight-normal">Apontamento de Tempos</h1>
                <br>
                <div class="col text-left">
                    <h5>Email</h5>
                </div>            
                <input id="email" class="form-control" placeholder="E-mail" required autofocus name="email" v-model="input.email" v-validate data-vv-rules="required|email">
                <span class="erro" v-show="errors.has('email')">{{ errors.first('email') }}</span>
                <div class="col text-left">
                    <h5>Senha</h5>
                </div>
                <input id="senha" class="form-control" placeholder="Senha" required type="password" name="senha" v-model="input.senha" v-validate data-vv-rules="required" ref="senha">
                <span class="erro" v-show="errors.has('senha')">{{ errors.first('senha') }}</span>
                <button class="btn btn-lg btn-primary btn-block" type="button" v-on:click="login()" :disabled="input.disable">Entrar</button>
                <br>
                <b-col sm="12" v-if="input.disable">
                    <clip-loader></clip-loader>       
                </b-col>
                <div class="col text-right">
                    <router-link to="/EsqueciMinhaSenha" class="badge">Esqueceu sua senha?</router-link>
                </div>
                <div class="col text-right">
                    <router-link to="/RegistreSe" class="badge">Cadastre-se!</router-link>
                </div>                    
                <p class="mt-2 text-muted">Desenvolvido por Nene (Rafael Persch)</p>
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
                    email: "",
                    senha: "",
                    disable: false
                }
            }
        },
        methods: {
            login(){
                this.$validator.validateAll().then(success => {
                    if(success) {
                        
                        this.input.disable = true;

                        let user = { 
                            Nome: "login",
                            Email: this.input.email, 
                            Senha: this.input.senha 
                        };

                        this.$http.post('api/Login', user).then(res => {
                            
                            this.sessionService.set(res.body);

                            this.$router.replace({ name: "Principal" });
                            
                            this.input.disable = false;

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