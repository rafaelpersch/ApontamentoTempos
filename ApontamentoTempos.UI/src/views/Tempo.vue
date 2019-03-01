<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <nav aria-label="breadcrumb">
          <ol class="breadcrumb">
            <li class="breadcrumb-item"><router-link :to="{ path: '/Principal'}"><a>Home</a></router-link></li>
            <li class="breadcrumb-item"><router-link :to="{ path: '/Principal/Tempos'}"><a>Tempos</a></router-link></li>
            <li class="breadcrumb-item active" aria-current="page">Apontamento de Tempo</li>
          </ol>
        </nav>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12">
        <h4 class="mb-3">Apontamento de Tempo</h4>
        <form class="needs-validation" v-on:submit.prevent="save()">
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Projeto</label>
              <input id="projetoId" class="form-control" placeholder="Projeto" required name="projetoId" v-model="input.projetoId" v-validate data-vv-rules="required">
              <span class="erro" v-show="errors.has('projetoId')">{{ errors.first('projetoId') }}</span>              
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Data</label>
              <input id="data" class="form-control" placeholder="Data" required name="data" v-model="input.data" v-validate data-vv-rules="required">
              <span class="erro" v-show="errors.has('data')">{{ errors.first('data') }}</span>              
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Issue</label>
              <input id="issue" class="form-control" placeholder="Issue" required name="issue" v-model="input.issue" v-validate data-vv-rules="required">
              <span class="erro" v-show="errors.has('issue')">{{ errors.first('issue') }}</span>              
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Temp</label>
              <input id="tempo" class="form-control" placeholder="Tempo" required name="tempo" v-model="input.tempo" v-validate data-vv-rules="required">
              <span class="erro" v-show="errors.has('tempo')">{{ errors.first('tempo') }}</span>              
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Produtivo</label>
              <input id="produtivo" class="form-control" placeholder="Produtivo" required name="produtivo" v-model="input.produtivo">
            </div>
          </div>
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Atividade</label>
              <input id="atividade" class="form-control" placeholder="atividade" required name="atividade" v-model="input.atividade" v-validate data-vv-rules="required">
              <span class="erro" v-show="errors.has('atividade')">{{ errors.first('atividade') }}</span>              
            </div>
          </div>                              
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Observações</label>
              <input id="observacao" class="form-control" placeholder="Observações" name="observacao" v-model="input.observacao">
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
  props: ['id'],
  data() {
      return {
          input: {
              disable: false,
              id: "00000000-0000-0000-0000-000000000000",
              projetoId : "",
              data: "":,
              issue: "",
              tempo : "",
              produtivo : false,
              atividade: 0,
              observacao: ""              
          },
      }
  },
  methods: {
      save(){
          this.$validator.validateAll().then(success => {
              if(success) {
                  
                  this.input.disable = true;

                  let apontamento = { 
                    Id: this.input.id,
                    UsuarioId : "",
                    ProjetoId : this.input.projetoId,
                    Data: this.input.data,
                    Issue: this.input.issue,
                    Tempo : this.input.tempo,
                    Produtivo : this.input.produtivo,
                    Atividade: this.input.atividade,
                    Observacao: this.input.observacao,
                  };

                  if (apontamento.Id == "00000000-0000-0000-0000-000000000000"){

                    this.httpService.post('api/ApontamentoTempo/', apontamento, false).then(resolve => {
                      if (resolve.status == 200){
                        this.$toast.success({
                            title:'Success',
                            message: "Apontamento registrado com sucesso!",
                        });                                
                        
                        this.input.disable = false;

                        this.$router.replace({ path: '/Principal/Tempo' });
                      }else{
                        this.input.disable = false;

                        this.$toast.error({
                            title:'Ops!',
                            message: resolve.retorno,
                        });
                      }
                    });

                  }else{

                    this.httpService.put('api/ApontamentoTempo', this.input.id, apontamento, false).then(resolve => {
                      if (resolve.status == 200){
                        this.$toast.success({
                            title:'Success',
                            message: "Apontamento alterado com sucesso!",
                        });                                
                        
                        this.input.disable = false;

                        this.$router.replace({ path: '/Principal/Tempos' });
                      }else{
                        this.input.disable = false;

                        this.$toast.error({
                            title:'Ops!',
                            message: resolve.retorno,
                        });
                      }
                    });                  
                  }
              }
          });
      }
  },
  created() {
      this.sessionService = new SessionService();
      this.httpService = new HttpService(this.$http, this.sessionService);

      if (this.sessionService.get() === null ){
          this.$router.replace({ name: "Home" });         
      }

      if (this.id != undefined){
        this.input.id = this.id;

        this.httpService.get('api/ApontamentoTempo/' + this.input.id, false).then(resolve => {
          if (resolve.status == 200){
                //this.input.nome = resolve.retorno.nome;
                this.input.disable =  false;
          }else{
            this.$toast.error({
                title:'Ops!',
                message: resolve.retorno,
            });
          }
        });          
      }
  },
}
</script>

<style scoped>

    .erro {
        color: red;
    } 

</style>