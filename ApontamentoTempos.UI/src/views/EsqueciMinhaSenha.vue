<template>
    <div>
        <div class="text-center login-back">
            <form class="form-signin">
                <img class="mb-4" src="..\assets\stopwatch.png" alt="" width="75" height="75">
                <h1 class="h3 mb-3 font-weight-normal">Apontamento de Tempos</h1>
                <br>
                <div class="col text-left">
                    <h5>Email</h5>
                </div>            
                <input id="email" class="form-control" placeholder="E-mail" required autofocus type="email" name="email" v-model="input.email" v-validate data-vv-rules="required|email">
                <span class="erro" v-show="errors.has('email')">{{ errors.first('email') }}</span>
                <button class="btn btn-lg btn-primary btn-block" type="button" v-on:click="esqueciMinhaSenha()">Recuperar Senha</button>
                <div class="col text-right">
                    <router-link to="/" class="badge">Voltar</router-link>
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
                    email: ""
                }
            }
        },
        methods: {
            esqueciMinhaSenha(){
                alert("implementar");
            },
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