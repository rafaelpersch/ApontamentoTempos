<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <nav aria-label="breadcrumb">
          <ol class="breadcrumb">
            <li class="breadcrumb-item"><router-link :to="{ path: '/Principal'}"><a>Home</a></router-link></li>
            <li class="breadcrumb-item active" aria-current="page">Troca de Senha</li>
          </ol>
        </nav>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12">
        <h4 class="mb-3">Troca de Senha</h4>
        <form class="needs-validation" v-on:submit.prevent="save()">
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Senha</label>
              <input id="senha" class="form-control" placeholder="Senha" required type="password" name="senha" v-model="input.senha" v-validate data-vv-rules="required" ref="senha">
              <span class="erro" v-show="errors.has('senha')">{{ errors.first('senha') }}</span>
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Repetir Senha</label>
              <input id="repetirSenha" class="form-control" placeholder="Repetir Senha" required type="password" name="repetirSenha" v-model="input.repetirSenha" v-validate data-vv-rules="required|confirmed:senha">                        
              <span class="erro" v-show="errors.has('repetirSenha')">{{ errors.first('repetirSenha') }}</span>
            </div>
          </div>          
          <div class="row">
            <div class="col-md-6 text-right">
              <button class="btn btn-primary mt-2" type="button" v-on:click="save()" :disabled="input.disable"><font-awesome-icon icon="save" /> Gravar</button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
    import SessionService from '../services/SessionService';
    import HttpService from '../services/HttpService.js';
    
    export default {
        data() {
            return {
                input: {
                    senha: "",
                    repetirSenha: "",
                    disable: false
                },
            }
        },
        methods: {
            save(){
                this.$validator.validateAll().then(success => {
                    if(success) {
                        
                        this.input.disable = true;

                        this.httpService.post('api/TrocaSenha', { Senha: this.input.senha }, false).then(resolve => {
                            if (resolve.status == 200){
                                this.$toast.success({
                                    title:'Success',
                                    message: "Senha alterada!",
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
            }
        },
        created() {
            this.sessionService = new SessionService();
            this.httpService = new HttpService(this.$http, this.sessionService);

            if (this.sessionService.get() == null ){
                this.$router.replace({ name: "Home" });         
            }

        },
    }
</script>

<style scoped>

    .erro {
        color: red;
    } 

</style>