export default class SessionService {

    get() {
        //return JSON.parse(localStorage.getItem('ApontamentoTempos') || '[]');
        return JSON.parse(localStorage.getItem('ApontamentoTempos'));
    }  

    set(token) {
        localStorage.setItem('ApontamentoTempos', JSON.stringify(token));
    }
}