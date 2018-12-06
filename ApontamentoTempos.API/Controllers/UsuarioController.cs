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
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly MyDbContext context;

        public UsuarioController(IConfiguration config)
        {
            this.context = new MyDbContext(config["ConnectionString"]);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            try
            {
                usuario.Id = Guid.NewGuid();
                usuario.Validar();

                List<Usuario> users = context.Usuarios.Where(x => x.Email.Trim().ToLower() == usuario.Email.Trim().ToLower()).ToList();

                if (users != null)
                {
                    if (users.Count > 0)
                    {
                        throw new ApplicationException("Já existe um usuário com esse e-mail!");
                    }
                }

                usuario.Senha = Cryptography.Encrypt(usuario.Senha);

                context.Usuarios.Add(usuario);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario([FromRoute] Guid id)
        {
            try
            {
                var usuario = await context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return BadRequest("Usuário não encontrado!");
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]Usuario usuario, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;

            Usuario usuarioCadatrado = context.Usuarios.Where(x => x.Email == usuario.Email && x.Senha == Cryptography.Encrypt(usuario.Senha)).FirstOrDefault();

            if (usuarioCadatrado != null)
            {
                credenciaisValidas = true;
            }

            if (credenciaisValidas)
            {
                return Ok(TokenConfigurations.GenerateToken(usuario.Id.ToString(), signingConfigurations, tokenConfigurations));
            }
            else
            {
                return BadRequest("Falha de login!");
            }
        }
    }
}