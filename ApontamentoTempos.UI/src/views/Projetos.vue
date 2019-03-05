<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <nav aria-label="breadcrumb">
          <ol class="breadcrumb">
            <li class="breadcrumb-item"><router-link :to="{ path: '/Principal/Dashboard'}"><a>Home</a></router-link></li>
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

      <template slot="id" scope="props">
        <div>
          <router-link class="btn btn-primary mt-2" :to="{ path: '/Principal/Projeto/' + props.row.id }">
            <font-awesome-icon icon="pencil-alt" />
          </router-link>        
          <button class="btn btn-danger mt-2" v-on:click="deleteItem(props.row.id)">
            <font-awesome-icon icon="trash" />
          </button>                          
        </div>  
      </template>

      </v-server-table>
  </div>
</template>

<script>

import SessionService from '../services/SessionService';
import HttpService from '../services/HttpService.js';

export default {
  data () {
    return {
      columns: ['nome', 'id'],
      options: {
        headings: {
          id: ''
        },
        columnsClasses: {
          id: 'columnsbotoes'
        },
        requestFunction: function (data) {

          this.sessionService = new SessionService();
          this.httpService = new HttpService(this.$http, this.sessionService);     

          return this.httpService.get('api/Projeto?query=' + data.query + 
                                                 '&ascending=' + data.ascending + 
                                                 '&byColumn=' + data.byColumn + 
                                                 '&limit=' + data.limit + 
                                                 '&orderBy=' + data.orderBy + 
                                                 '&page=' + data.page, 
                                                 false).then(resolve => {

            if (resolve.status == 200){
                return resolve.retorno; 
            }else{
                console.log(resolve);
            }
          });
        },        
        responseAdapter : function(resp) {
          var data = this.getResponseData(resp.registros); 
          var data2 = [];
          for (var key in data) {
            data2.push({nome:data[key].nome, id: data[key].id});
          }          

          return { data: data2, count: resp.count };
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

<style>
  .columnsbotoes {
    width: 110px;
  }
</style>