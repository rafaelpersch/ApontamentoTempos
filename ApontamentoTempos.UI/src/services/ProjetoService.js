export default class ProjetoService {
    constructor(http) {
        this._http = http;
    }

    teste(){
        this._http.get('api/values')
            .then((response) => {
            alert(response.body);
        });
    }
}