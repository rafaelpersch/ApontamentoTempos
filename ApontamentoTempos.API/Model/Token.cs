using System;

namespace ApontamentoTempos.API.Model
{
    public class Token
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public string Message { get; set; }
    }
}
