using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApontamentoTempos.API.Data;
using ApontamentoTempos.API.Model;
using ApontamentoTempos.API.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApontamentoTempos.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private IConfiguration config;

        public UsuarioController(IConfiguration config)
        {
            this.config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            try
            {
                usuario.Id = Guid.NewGuid();
                usuario.Validar();

                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    List<Usuario> users = context.Usuarios.Where(x => x.Email.Trim().ToLower() == usuario.Email.Trim().ToLower()).ToList();

                    if (users != null)
                    {
                        if (users.Count > 0)
                        {
                            throw new ApplicationException("Já existe um usuário com esse e-mail!");
                        }
                    }

                    usuario.Senha = Cryptography.Encrypt(usuario.Email + usuario.Senha);

                    context.Usuarios.Add(usuario);
                    await context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}