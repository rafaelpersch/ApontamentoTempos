export default class HttpService {

    constructor(resource, sessionService) {
        this.resource = resource;
        this.sessionService = sessionService;
    }

    post(url, obj, anonymous){

        return new Promise((resolve)=> {
            let myHeaders = {};

            if (!anonymous){
                if (this.sessionService.get() === null){
                    resolve({ sucesso: false, retorno: "Usuário Desconectado", status: 401 });
                }else{
                    myHeaders = { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken };
                }
            }

            this.resource.post(url, obj, { headers: myHeaders }).then(res => {

                if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    if (res.status == 401) {
                        this.sessionService.remove();
                    }                       
                    resolve({ sucesso: false, retorno: res.body, status: res.status });
                }            
            }, err => {
                if (err.status == 401) {
                    this.sessionService.remove();
                }   
                resolve({ sucesso: false, retorno: err.body, status: err.status });
            });
        });
    }
    
    get(url, anonymous){

        return new Promise((resolve)=> {

            let myHeaders = {};

            if (!anonymous){
                if (this.sessionService.get() === null){
                    resolve({ sucesso: false, retorno: "Usuário Desconectado", status: 401 });
                }else{
                    myHeaders = { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken };
                }
            }
    
            this.resource.get(url, { headers: myHeaders }).then(res => {
                if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    if (res.status == 401) {
                        this.sessionService.remove();
                    }                       
                    resolve({ sucesso: false, retorno: res.body, status: res.status });
                }
            }, err => {
                if (err.status == 401) {
                    this.sessionService.remove();
                }   
                resolve({ sucesso: false, retorno: err.body, status: err.status });             
            });             
        });
    }

    put(url, id, obj, anonymous){

        return new Promise((resolve)=> {
            let myHeaders = {};

            if (!anonymous){
                if (this.sessionService.get() === null){
                    resolve({ sucesso: false, retorno: "Usuário Desconectado", status: 401 });
                }else{
                    myHeaders = { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken };
                }
            }

            this.resource.put(url + "/" + id, obj, { headers: myHeaders }).then(res => {
                if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    if (res.status == 401) {
                        this.sessionService.remove();
                    }                       
                    resolve({ sucesso: false, retorno: res.body, status: res.status });
                }
            }, err => {
                if (err.status == 401) {
                    this.sessionService.remove();
                }   
                resolve({ sucesso: false, retorno: err.body, status: err.status });          
            });            
        });
    }

    delete(url, id, anonymous){

        return new Promise((resolve)=> {
            let myHeaders = {};

            if (!anonymous){
                if (this.sessionService.get() === null){
                    resolve({ sucesso: false, retorno: "Usuário Desconectado", status: 401 });
                }else{
                    myHeaders = { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken };
                }
            }
    
            this.resource.delete(url + "/" + id, { headers: myHeaders}).then(res => {
                if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    if (res.status == 401) {
                        this.sessionService.remove();
                    }                       
                    resolve({ sucesso: false, retorno: res.body, status: res.status });
                }
            }, err => {
                if (err.status == 401) {
                    this.sessionService.remove();
                }   
                resolve({ sucesso: false, retorno: err.body, status: err.status });          
            });             
        });
    }
}