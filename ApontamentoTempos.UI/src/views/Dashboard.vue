<template>
    <div>
    <div class="row">
      <div class="col-md-12">
        <nav aria-label="breadcrumb">
          <ol class="breadcrumb">
            <li class="breadcrumb-item"><router-link :to="{ path: '/Principal/Dashboard'}"><a>Home</a></router-link></li>
          </ol>
        </nav>
      </div>
    </div>        
  <GChart
    type="ColumnChart"
    :data="chartData"
    :options="chartOptions"
  />
    </div>
    
</template>

<script>
import SessionService from '../services/SessionService';
import HttpService from '../services/HttpService.js';

export default {
  data () {
    return {
      // Array will be automatically processed with visualization.arrayToDataTable function
      chartData: [[]],
      chartOptions: {
        chart: {
          title: '',
          subtitle: '',
        }
      }
    }
  },
  created() {
    this.sessionService = new SessionService();
    this.httpService = new HttpService(this.$http, this.sessionService);

    if (this.sessionService.get() === null ){
        this.$router.replace({ name: "Home" });         
    }

    this.httpService.get('api/Dashboard', false).then(resolve => {

        if (resolve.status == 200){

          this.chartData = [
            ['', 'Tempos'],
            [resolve.retorno[0].dia, resolve.retorno[0].tempo],
            [resolve.retorno[1].dia, resolve.retorno[1].tempo],
            [resolve.retorno[2].dia, resolve.retorno[2].tempo],
            [resolve.retorno[3].dia, resolve.retorno[3].tempo],
            [resolve.retorno[4].dia, resolve.retorno[4].tempo],
            [resolve.retorno[5].dia, resolve.retorno[5].tempo],
            [resolve.retorno[6].dia, resolve.retorno[6].tempo]
          ];          
        }else{
          this.chartData = [
                  ['', 'Tempos']
          ];
        }
    });         
  },   
}
</script>
<style scoped>

</style>