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
            <div class="col-md-6" id="produtivo2"  style="text-align: rigth;">
              <label for="firstName">Produtivo</label><br>
              <input id="produtivo" type="checkbox" class="checkboxn" name="produtivo" v-model="input.produtivo">
            </div>            
          </div>
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Data</label>
              <input id="data" type="date" class="form-control" placeholder="Data" required name="data" v-model="input.data" v-validate data-vv-rules="required">
              <span class="erro" v-show="errors.has('data')">{{ errors.first('data') }}</span>              
            </div>
            <div class="col-md-6">
              <label for="firstName">Issue</label>
              <input id="issue" class="form-control" placeholder="Issue" required name="issue" v-model="input.issue" v-validate data-vv-rules="required">
              <span class="erro" v-show="errors.has('issue')">{{ errors.first('issue') }}</span>              
            </div>            
          </div>
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Tempo</label>
              <input id="tempo" type="number" step="0.01" class="form-control" placeholder="Tempo" required name="tempo" v-model="input.tempo" v-validate data-vv-rules="required">
              <span class="erro" v-show="errors.has('tempo')">{{ errors.first('tempo') }}</span>              
            </div>
            <div class="col-md-6">
              <label for="firstName">Atividade</label>
              <select id="atividade" class="form-control" placeholder="atividade" required name="atividade" v-model="input.atividade">           
                <option value="0" selected>Desenvolvimento</option>
                <option value="1">E-mail</option>
                <option value="2">Planejamento</option>
                <option value="3">Ler/Escrever Documento</option>
                <option value="4">Comunicação Formal</option>
                <option value="5">Comunicação Informal</option>
                <option value="6">Navegação Formal</option>
                <option value="7">Navegação Informal</option>
                <option value="8">Outros</option>
              </select>
            </div>            
          </div>
          <div class="row">
            <div class="col-md-12">
              <label for="firstName">Observações</label>
              <textarea id="observacao" class="form-control" placeholder="Observações" name="observacao" v-model="input.observacao"/>
            </div>
          </div>          
          <div class="row">
            <div class="col-md-12 text-right">
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
              data: "",
              issue: "",
              tempo : 0,
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

    .checkboxn[type=checkbox] {
      transform: scale(1.5);
      margin-left: 10px;
    } 

</style>