export default class HttpService {

    constructor(resource, sessionService) {
        this.resource = resource;
        this.sessionService = sessionService;
    }

    post(url, obj, anonymous){

        return new Promise((resolve)=> {
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
                            resolve({ sucesso: false, retorno: "401 Unauthorized", status: 401 });
                        }            
                    }, err => {
                        resolve({ sucesso: false, retorno: "401 Unauthorized: " + err, status: 401 });
                    });
                }
                else if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    resolve({ sucesso: false, retorno: res.body, status: res.status });
                }            
            }, err => {
                console.log("Erro POST: " + err);
                resolve({ sucesso: false, retorno: "Erro", status: 0 });
            })
            .catch((err) => {
                console.log("Erro POST catch: " + err);
                resolve({ sucesso: false, retorno: "Erro", status: 0 });
            });
        });
    }
    
    get(url, anonymous){

        return new Promise((resolve)=> {
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
                            resolve({ sucesso: false, retorno: "401 Unauthorized", status: 401 });
                        }            
                    }, err => {
                        resolve({ sucesso: false, retorno: "401 Unauthorized: " + err, status: 401 });
                    });

                }
                else if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    resolve({ sucesso: false, retorno: res.body, status: res.status });
                }
            }, err => {
                console.log("Erro GET: " + err);
                resolve({ sucesso: false, retorno: "Erro", status: 0 });
            })
            .catch((err) => {
                console.log("Erro GET catch: " + err);
                resolve({ sucesso: false, retorno: "Erro", status: 0 });
            });             
        });
    }

    put(url, id, obj, anonymous){

        return new Promise((resolve)=> {
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
                            resolve({ sucesso: false, retorno: "401 Unauthorized", status: 401 });
                        }            
                    }, err => {
                        resolve({ sucesso: false, retorno: "401 Unauthorized: " + err, status: 401 });
                    });
                }
                else if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    resolve({ sucesso: false, retorno: res.body, status: res.status });
                }
            }, err => {
                console.log("Erro PUT: " + err);
                resolve({ sucesso: false, retorno: "Erro", status: 0 });
            })
            .catch((err) => {
                console.log("Erro PUT catch: " + err);
                resolve({ sucesso: false, retorno: "Erro", status: 0 });
            });            
        });
    }

    delete(url, id, anonymous){

        return new Promise((resolve)=> {
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
                            resolve({ sucesso: false, retorno: "401 Unauthorized", status: 401 });
                        }            
                    }, err => {
                        resolve({ sucesso: false, retorno: "401 Unauthorized: " + err, status: 401 });
                    });

                }
                else if (res.status == 200){
                    resolve({ sucesso: true, retorno: res.body, status: res.status });
                }else{
                    resolve({ sucesso: false, retorno: res.body, status: res.status });
                }
            }, err => {
                console.log("Erro DELETE: " + err);
                resolve({ sucesso: false, retorno: "Erro", status: 0 });
            })
            .catch((err) => {
                console.log("Erro DELETE catch: " + err);
                resolve({ sucesso: false, retorno: "Erro", status: 0 });
            });             
        });
    }
}