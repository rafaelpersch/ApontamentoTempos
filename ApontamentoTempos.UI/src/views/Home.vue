<template>
    <div>
        <div v-if="telaLogin" id="login" class="text-center login-back">
            <form class="form-signin">
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
                <input id="password" class="form-control" placeholder="Senha" required type="password" name="password" v-model="input.password" v-validate data-vv-rules="required" ref="password">
                <span class="erro" v-show="errors.has('password')">{{ errors.first('password') }}</span>
                <button class="btn btn-lg btn-primary btn-block" type="button" v-on:click="efetuarLogin()">Entrar</button>
                <div class="col text-right">
                    <router-link to="#" class="badge"><span v-on:click="esqueceuSuaSenha()">Esqueceu sua senha?</span></router-link>
                </div>
                <div class="col text-right">
                    <router-link to="#" class="badge"><span v-on:click="cadastrese()">Cadastre-se!</span></router-link>
                </div>                    
                <p class="mt-5 mb-3 text-muted">Desenvolvido por Nene (Rafael Persch)</p>
            </form>
        </div>
        <div v-if="telaCadastrese" id="cadastrese" class="text-center login-back">
            <form class="form-signin">
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
                <input id="password" class="form-control" placeholder="Senha" required type="password" name="password" v-model="input.password" v-validate data-vv-rules="required">
                <span class="erro" v-show="errors.has('password')">{{ errors.first('password') }}</span>
                <button class="btn btn-lg btn-primary btn-block" type="button" v-on:click="efetuarCadastro()">Cadastre-se</button>
                <div class="col text-right">
                    <router-link to="#" class="badge"><span v-on:click="login()">Voltar</span></router-link>
                </div>                        
                <p class="mt-5 mb-3 text-muted">Desenvolvido por Nene (Rafael Persch)</p>
            </form>
        </div>
        <div v-if="telaEsqueceuSuaSenha" id="esqueceusuasenha" class="text-center login-back">
            <form class="form-signin">
                <img class="mb-4" src="..\assets\stopwatch.png" alt="" width="75" height="75">
                <h1 class="h3 mb-3 font-weight-normal">Apontamento de Tempos</h1>
                <br>
                <div class="col text-left">
                    <h5>Email</h5>
                </div>            
                <input id="email" class="form-control" placeholder="E-mail" required autofocus type="email" name="email" v-model="input.email" v-validate data-vv-rules="required|email">
                <span class="erro" v-show="errors.has('email')">{{ errors.first('email') }}</span>
                <button class="btn btn-lg btn-primary btn-block" type="button" v-on:click="efetuarRecuperacaoSenha()">Recuperar Senha</button>
                <div class="col text-right">
                    <router-link to="#" class="badge"><span v-on:click="login()">Voltar</span></router-link>
                </div>                        
                <p class="mt-5 mb-3 text-muted">Desenvolvido por Nene (Rafael Persch)</p>
            </form>
        </div>
    </div>
    
</template>

<script>

    export default {
        data() {
            return {
                input: {
                    nome: "",
                    email: "",
                    password: ""
                },
                telaLogin: true,
                telaEsqueceuSuaSenha: false,
                telaCadastrese: false
            }
        },
        methods: {
            efetuarLogin() {
                if(this.input.email != "" && this.input.password != "") {
                    if(this.input.email == "teste@teste.com" && this.input.password == "111") {
                        this.$emit("authenticated", true);
                        this.$router.replace({ name: "secure" });
                    } else {
                        //console.log("The email and / or password is incorrect");
                    }
                } else {
                    //console.log("A email and password must be present");
                }
            },
            efetuarCadastro(){
                this.$validator.validateAll().then(success => {
                    if(success) {

                        let user = { 
                            Nome: this.input.nome, 
                            Email: this.input.email, 
                            Senha: this.input.password 
                        };

                        this.$http.post('api/Usuario', user)
                            .then((response) => {
                            alert(response.body);
                        });
                    }
                });
            },
            efetuarRecuperacaoSenha(){

            },
            login(){
                this.telaCadastrese = false;
                this.telaEsqueceuSuaSenha = false;
                this.telaLogin = true;
            },
            esqueceuSuaSenha(){
                this.telaLogin = false;
                this.telaCadastrese = false;
                this.telaEsqueceuSuaSenha = true;
            },
            cadastrese(){
                this.telaLogin = false;
                this.telaEsqueceuSuaSenha = false;
                this.telaCadastrese = true;
            }
        },
        created() {
            /*this.$toast.success({
                title:'Success',
                message:'message',
                
            });

            this.$toast.error({
                title:'Error',
                message:'message',
            });*/
        }
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