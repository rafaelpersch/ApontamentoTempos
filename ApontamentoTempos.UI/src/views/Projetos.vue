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
    <div class="row">
      <div class="col-md-12">
        <h4 class="mb-3">Projeto</h4>
        <form class="needs-validation" novalidate="">
          <div class="row">
            <div class="col-md-6">
              <label for="firstName">Nome</label>
              <input type="text" class="form-control" id="firstName" placeholder="" required="">
            </div>
          </div>
          <div class="row">
            <div class="col-md-6 text-right">
              <button class="btn btn-primary mt-2" type="submit"><font-awesome-icon icon="save" /> Gravar</button>
            </div>
          </div>
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