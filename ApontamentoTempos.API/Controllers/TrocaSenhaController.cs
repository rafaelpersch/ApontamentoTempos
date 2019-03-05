using System;
using System.Linq;
using System.Threading.Tasks;
using ApontamentoTempos.API.Model;
using ApontamentoTempos.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ApontamentoTempos.API.Tools;

namespace ApontamentoTempos.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TrocaSenhaController : Controller
    {
        private IConfiguration config;

        public TrocaSenhaController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpPost]
        public async Task<IActionResult> PostProjeto([FromBody] TrocaSenha trocaSenha)
        {
            try
            {
                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    Usuario usuario = null;

                    try
                    {
                        usuario = await context.Usuarios.FindAsync(Guid.Parse(User.Claims.Where(x => x.Type == "id_user").FirstOrDefault().Value));
                    }
                    catch
                    {
                        usuario = null;
                    }

                    if (usuario == null)
                    {
                        return BadRequest("Usuário inválido!");
                    }

                    usuario.Senha = Cryptography.Encrypt(usuario.Email + trocaSenha.Senha);

                    context.Entry(usuario).State = EntityState.Modified;

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

    public class TrocaSenha
    {
        public TrocaSenha()
        {
            this.Senha = string.Empty;
        }

        public string Senha { get; set; }
    }
}