<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <nav aria-label="breadcrumb">
          <ol class="breadcrumb">
            <li class="breadcrumb-item"><router-link :to="{ path: '/Principal'}"><a>Home</a></router-link></li>
            <li class="breadcrumb-item active" aria-current="page">Projetos</li>
          </ol>
        </nav>
      </div>
    </div>  
    <div class="row">
      <div class="col-md-12 text-right">
        <router-link :to="{ path: '/Principal/Projeto'}">
          <button class="btn btn-primary mt-2" type="button"><font-awesome-icon icon="plus-square" /> Novo</button>
        </router-link>
      </div>
    </div>  
     <v-server-table ref="table" 
          :columns="columns" 
          :options="options">
          
        <router-link class="btn btn-primary mt-2" slot="uriEdit" slot-scope="props" :to="{ path: props.row.uriEdit}">
          <font-awesome-icon icon="pencil-alt" />
        </router-link>        
        <button class="btn btn-danger mt-2" slot="uriDelete" slot-scope="props" v-on:click="deleteItem(props.row.uriDelete)">
          <font-awesome-icon icon="trash" />
        </button>                

      </v-server-table>
  </div>
</template>

<script>

import SessionService from '../services/SessionService';
import HttpService from '../services/HttpService.js';

export default {
  data () {
    return {
      columns: ['id', 'nome', 'uriEdit', 'uriDelete'],
      options: {
        headings: {
          uriEdit: '', 
          uriDelete: '',
        },
        requestFunction: function (data) {

          this.sessionService = new SessionService();
          this.httpService = new HttpService(this.$http, this.sessionService);     

          return this.httpService.get('api/Projeto', false).then(resolve => {

              if (resolve.status == 200){
                  return resolve.retorno; 
              }else{
                  console.log(res);
              }
          });
        },        
        responseAdapter : function(resp) {
          var data = this.getResponseData(resp); 
          var data2 = [];
          for (var key in data) {
            data2.push({id:data[key].id, nome:data[key].nome, uriEdit: '/Principal/Projeto/' + data[key].id, uriDelete: data[key].id});
          }          

          return { data: data2, count: 20 };
        },            
      }
    }
  },
  methods: {
    deleteItem(id){

      this.httpService.delete('api/Projeto', id, false).then(resolve => {
        if (resolve.status == 200){
          this.$toast.success({
              title:'Success',
              message: "Projeto deletado!",
          });                                
          
          this.$refs.table.refresh();
        }else{

          this.$toast.error({
              title:'Ops!',
              message: resolve.retorno,
          });
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
  }, 
}
</script>

<style scoped>
.form-inline label{
  display: initial;
}

.VueTables__search{
  text-align: left;
  display: none;
}

.VueTables__limit{
  text-align: left;
  display: initial;
}

th:nth-child(3) {
  text-align: left;
}
</style>