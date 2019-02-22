export default class HttpService {

    constructor(resource, sessionService) {
        this.resource = resource;
        this.sessionService = sessionService;
    }

    post(url, obj, anonymous){

        return new Promise((resolve, reject)=> {
            let myHeaders = {};

            if (!anonymous){
                myHeaders = { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken };
            }

            this.resource.post(url, obj, { headers: myHeaders }).then(res => {

                if (res.status == 401) {
                    this.resource.post('/api/RefreshToken', { Id:this.sessionService.get().RefreshToken }).then(res => {
                        if (res.status == 200){
                            this.sessionService.set(res.body);
                            resolve(this.post(url, obj, anonymous));
                        }else{
                            reject({ sucesso: false, retorno: "401 Unauthorized", status: 401 });
                        }            
                    }, err => {
                        reject({ sucesso: false, retorno: "401 Unauthorized: " + err.body, status: 401 });
                    });
                }
                else if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    reject({ sucesso: false, retorno: res.body, status: res.status });
                }            
            }, err => {
                reject({ sucesso: false, retorno: err.body, status: err.status });
            });
        });
    }
    
    get(url, anonymous){

        return new Promise((resolve, reject)=> {
            let myHeaders = {};

            if (!anonymous){
                myHeaders = { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken };
            }
    
            this.resource.get(url, { headers: myHeaders }).then(res => {
                if (res.status == 401) {

                    this.resource.post('/api/RefreshToken', { Id:this.sessionService.get().RefreshToken }).then(res => {
                        if (res.status == 200){
                            this.sessionService.set(res.body);
                            resolve(this.get(url, anonymous));
                        }else{
                            reject({ sucesso: false, retorno: "401 Unauthorized", status: 401 });
                        }            
                    }, err => {
                        reject({ sucesso: false, retorno: "401 Unauthorized: " + err.body, status: 401 });
                    });

                }
                else if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    reject({ sucesso: false, retorno: res.body, status: res.status });
                }
            }, err => {
                reject({ sucesso: false, retorno: err.body, status: err.status });
            });             
        });
    }

    put(url, id, obj, anonymous){

        return new Promise((resolve, reject)=> {
            let myHeaders = {};

            if (!anonymous){
                myHeaders = { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken };
            }
    
            this.resource.put(url + "/" + id, obj, { headers: myHeaders }).then(res => {
                if (res.status == 401) {

                    this.resource.post('/api/RefreshToken', { Id:this.sessionService.get().RefreshToken }).then(res => {
                        if (res.status == 200){
                            this.sessionService.set(res.body);
                            resolve(this.put(url, id, obj, anonymous));
                        }else{
                            reject({ sucesso: false, retorno: "401 Unauthorized", status: 401 });
                        }            
                    }, err => {
                        reject({ sucesso: false, retorno: "401 Unauthorized: " + err.body, status: 401 });
                    });
                }
                else if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    reject({ sucesso: false, retorno: res.body, status: res.status });
                }
            }, err => {
                reject({ sucesso: false, retorno: err.body, status: err.status });
            });            
        });
    }

    delete(url, id, anonymous){

        return new Promise((resolve, reject)=> {
            let myHeaders = {};

            if (!anonymous){
                myHeaders = { 'Authorization': 'Bearer ' + this.sessionService.get().accessToken };
            }
    
            this.resource.delete(url + "/" + id, { headers: myHeaders}).then(res => {
                if (res.status == 401) {
                    
                    this.resource.post('/api/RefreshToken', { Id:this.sessionService.get().RefreshToken }).then(res => {
                        if (res.status == 200){
                            this.sessionService.set(res.body);
                            resolve(this.delte(url, id, anonymous));
                        }else{
                            reject({ sucesso: false, retorno: "401 Unauthorized", status: 401 });
                        }            
                    }, err => {
                        reject({ sucesso: false, retorno: "401 Unauthorized: " + err.body, status: 401 });
                    });

                }
                else if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    reject({ sucesso: false, retorno: res.body, status: res.status });
                }
            }, err => {
                reject({ sucesso: false, retorno: err.body, status: err.status });
            });             
        });
    }
}