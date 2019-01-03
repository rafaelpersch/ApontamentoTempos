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
      columns: ['id', 'nome'],
      options: {
        headings: {
          name: 'Country Name',
          code: 'Country Code'
        },
        sortable: ['id', 'nome'],
        filterable: ['id', 'nome'],
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
          /*var result = [];

          for (var d in data) {
              result.push({id:data[d].c1, nome:data[d].c2}); 
          }*/

          return { data: data, count: 20 };
        },            
      }
    }
  },
  created() {
    this.sessionService = new SessionService();
  }, 
}
</script>

<style>
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