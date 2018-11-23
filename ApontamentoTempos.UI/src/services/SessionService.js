export default class SessionService {

    get() {
        return JSON.parse(localStorage.getItem('ApontamentoTemposNene') || '[]')
    }  

    set(token) {
        localStorage.setItem('ApontamentoTemposNene', JSON.stringify(token));
    }
}