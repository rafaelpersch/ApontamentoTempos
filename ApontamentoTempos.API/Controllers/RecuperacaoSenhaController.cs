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
    [Route("api/[controller]")]
    [ApiController]
    public class RecuperacaoSenhaController : Controller
    {
        private IConfiguration config;
        private Email remetente;

        public RecuperacaoSenhaController(IConfiguration config)
        {
            this.config = config;
            this.remetente = new Email()
            {
                EnderecoEmail = config["EnderecoEmail"],
                Senha = config["Senha"],
                PortaSmtp = Convert.ToInt32(config["PortaSmtp"]),
                ServidorSmtp = config["ServidorSmtp"],
                UtilizarSsl = config["UtilizarSsl"] == "true"
            };
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostRecuperacaoSenha([FromBody] string email)
        {
            try
            {
                using (var context = new MyDbContext(config["ConnectionString"]))
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

                    context.RecuperacaoSenhas.Add(reset);

                    await context.SaveChangesAsync();

                    // TODO: ENVIAR EMAIL

                    List<string> destinatarios = new List<string>();
                    destinatarios.Add(email);

                    await EmailHelper.EnvioEmail(remetente, destinatarios, "Recuperação de Senha", "");
                }

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
                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    var solicitacao = await context.RecuperacaoSenhas.FindAsync(id);

                    if (solicitacao == null)
                    {
                        return BadRequest("Solicitação não encontrado!");
                    }

                    return Ok(solicitacao);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecuperacaoSenha([FromRoute] Guid id, [FromBody] RecuperacaoSenha recuperacaoSenha)
        {
            try
            {
                if (recuperacaoSenha == null)
                {
                    return BadRequest("Corpo inválido!");
                }

                if (id != recuperacaoSenha.Id)
                {
                    return BadRequest("Ids não conferem!");
                }

                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    var reset = await context.RecuperacaoSenhas.FindAsync(id);

                    if (reset == null)
                    {
                        return BadRequest("Solicitação não encontrada!");
                    }

                    context.RecuperacaoSenhas.Remove(reset);

                    var usuario = await context.Usuarios.FindAsync(reset.UsuarioId);

                    usuario.Senha = Cryptography.Encrypt(usuario.Email + recuperacaoSenha.Usuario.Senha);

                    context.Entry(usuario).State = EntityState.Modified;

                    await context.SaveChangesAsync();
                }

                return Ok("Alteração efetuada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}