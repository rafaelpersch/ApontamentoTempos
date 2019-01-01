<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <nav aria-label="breadcrumb">
          <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item active" aria-current="page">Projetos</li>
          </ol>
        </nav>
      </div>
    </div>    
     <v-server-table 
          url="api/values/"
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
      columns: ['c1', 'c2'],
      options: {
        headings: {
          name: 'Country Name',
          code: 'Country Code'
        },
        sortable: ['c1', 'c2'],
        filterable: ['c1', 'c2'],
        getData(){
          this.sessionService = new SessionService();

          this.$http.get('api/values/', { headers: { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken }}).then(res => {
            alert(res.body[0].c1);
            return res.body; 
          }, res =>{
            alert(res.body);
            return res.body; 
          }); 
        },  
        responseAdapter : function(resp) {
          var data = this.getResponseData(resp); 
          return { data: data, count: 20 };
        }     
      }
    }
  },
  created() {
    this.sessionService = new SessionService();
  }
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