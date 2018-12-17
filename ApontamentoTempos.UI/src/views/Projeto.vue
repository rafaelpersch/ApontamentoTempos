<template>
<!--<b-container fluid>
    <b-row class="my-1">
      <b-col sm="3"><label>Type</label></b-col>
      <b-col sm="9"><b-form-input></b-form-input></b-col>
    </b-row>
  </b-container>-->

    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <h4 class="mb-3">Cadastro de Projeto</h4>
          <form class="needs-validation" novalidate="">
            <div class="row">
              <div class="col-md-6 mb-3">
                <label for="firstName">First name</label>
                <input type="text" class="form-control" id="firstName" placeholder="" required="">
              </div>
              <div class="col-md-6 mb-3">
                <label for="lastName">Last name</label>
                <input type="text" class="form-control" id="lastName" placeholder="" required="">
              </div>
            </div>
            <button class="btn btn-primary btn-block" type="submit">Gravar</button>
          </form>
        </div>
      </div>
    </div>

</template>

<script>
export default {
  data () {
    return {
      types: [
        'text', 'password', 'email', 'number', 'url',
        'tel', 'date', `time`, 'range', 'color'
      ]
    }
  }
}
</script>