using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApontamentoTempos.API.Tools;
using ApontamentoTempos.API.Data;
using ApontamentoTempos.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ApontamentoTempos.API.Security;

namespace ApontamentoTempos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly MyDbContext context;

        public LoginController(IConfiguration config)
        {
            this.context = new MyDbContext(config["ConnectionString"]);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult PostLogin([FromBody]Usuario usuario, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;

            Usuario usuarioCadatrado = context.Usuarios.Where(x => x.Email == usuario.Email && x.Senha == Cryptography.Encrypt(usuario.Senha)).FirstOrDefault();

            if (usuarioCadatrado != null)
            {
                credenciaisValidas = true;
            }

            if (credenciaisValidas)
            {
                return Ok(TokenConfigurations.GenerateToken(usuarioCadatrado.Id.ToString(), signingConfigurations, tokenConfigurations));
            }
            else
            {
                return BadRequest("Falha de login!");
            }
        }
    }
}