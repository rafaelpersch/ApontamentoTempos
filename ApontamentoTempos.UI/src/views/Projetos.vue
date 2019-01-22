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
     <v-server-table 
          :columns="columns" 
          :options="options">
      </v-server-table>
  </div>
</template>

<script>

import SessionService from '../services/SessionService';

export default {
  data () {
    return {
      columns: ['id', 'nome', 'botoes'],
      options: {
        headings: {
          botoes: 'Botões'
        },
        requestFunction: function (data) {

          this.sessionService = new SessionService();

          return this.$http.get('api/Projeto/', { headers: { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken }}).then(res => {
            return res.body; 
          }, res =>{
            console.log(res);
          }).bind(this); 

        },        
        responseAdapter : function(resp) {
          var data = this.getResponseData(resp); 
          var data2 = [];
          for (var key in data) {
            data2.push({id:data[key].id, nome:data[key].nome, botoes:<div><button class="btn btn-primary mt-2" type="button" onclick="alert('vsdvds');" ><font-awesome-icon icon="pencil-alt" /></button> <button class="btn btn-danger mt-2" type="button"><font-awesome-icon icon="trash" /></button></div>})
          }          

//data = [{id:<div><button class="btn btn-primary mt-2" type="button" onclick="alert('vsdvds');" ><font-awesome-icon icon="pencil-alt" /></button> <button class="btn btn-danger mt-2" type="button"><font-awesome-icon icon="trash" /></button></div>, nome:'teste'}];

          /*var result = [];

          for (var d in data) {
              result.push({id:data[d].c1, nome:data[d].c2}); 
          }*/

          return { data: data2, count: 20 };
        },            
      }
    }
  },
  created() {
    this.sessionService = new SessionService();
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