<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <nav aria-label="breadcrumb">
          <ol class="breadcrumb">
            <li class="breadcrumb-item"><router-link :to="{ path: '/Principal'}"><a>Home</a></router-link></li>
            <li class="breadcrumb-item"><router-link :to="{ path: '/Principal/Projetos'}"><a>Projetos</a></router-link></li>
            <li class="breadcrumb-item active" aria-current="page">Cadastro de Projeto</li>
          </ol>
        </nav>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12">
        <h4 class="mb-3">Cadastro de Projeto</h4>
        <form class="needs-validation" v-on:submit.prevent="save()">
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Nome</label>
              <input id="nome" class="form-control" placeholder="Nome" required name="nome" v-model="input.nome" v-validate data-vv-rules="required">
              <span class="erro" v-show="errors.has('nome')">{{ errors.first('nome') }}</span>              
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

export default {
  props: ['id'],
  data() {
      return {
          input: {
              id: "8bac866f-b8c2-4cf3-8603-3f509c76fc00",
              nome: "",
              disable: false
          },
      }
  },
  methods: {
      save(){
          this.$validator.validateAll().then(success => {
              if(success) {
                  
                  this.input.disable = true;

                  let projeto = { 
                    Id: this.input.id,
                    Nome: this.input.nome
                  };

                  this.$http.post('api/Projeto', projeto, { headers: { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken }}).then(res => {

                      this.$toast.success({
                          title:'Success',
                          message: "Projeto registrado com sucesso!",
                      });                                
                      
                      this.input.disable = false;



                      this.$router.replace({ path: '/Principal/Projetos' });

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

      if (this.id != undefined){
        this.input.id = this.id;
      }
  },
}
</script>

<style scoped>

    .erro {
        color: red;
    } 

</style>