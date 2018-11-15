using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApontamentoTempos.API.Data;
using ApontamentoTempos.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApontamentoTempos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecuperacaoSenhaController : Controller
    {
        private readonly MyDbContext context;

        public RecuperacaoSenhaController(IConfiguration config)
        {
            this.context = new MyDbContext(config["ConnectionString"]);
        }

        [HttpPost]
        public async Task<IActionResult> PostRecuperacaoSenha([FromBody] string email)
        {
            try
            {
                Usuario user = context.Usuarios.Where(b => b.Email == email).FirstOrDefault();

                if (user == null)
                {
                    throw new ApplicationException("Email não encontrado!");
                }

                RecuperacaoSenha reset = new RecuperacaoSenha()
                {
                    Id = Guid.NewGuid(),
                    Data = DateTime.Now,
                    Usuario = null,
                    UsuarioId = user.Id,
                };

                reset.Validar();

                context.RecuperacoesSenha.Add(reset);

                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecuperacaoSenha([FromRoute] Guid id)
        {
            try
            {
                var solicitacao = await context.RecuperacoesSenha.FindAsync(id);

                if (solicitacao == null)
                {
                    return BadRequest("Solicitação não encontrado!");
                }

                return Ok(solicitacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecuperacaoSenha([FromRoute] Guid id, [FromBody] Guid idRecuperacao, [FromBody] string novaSenha)
        {
            try
            {
                if (id != idRecuperacao)
                {
                    return BadRequest("Ids não conferem!");
                }

                var reset = await context.RecuperacoesSenha.FindAsync(id);

                if (reset == null)
                {
                    return BadRequest("Solicitação não encontrada!");
                }

                context.RecuperacoesSenha.Remove(reset);

                //fazer o mexe no usuário

                //context.Entry(projeto).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return Ok("Alteração efetuada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //RecoveryPassword(Guid id, string newPassword)
    }
}