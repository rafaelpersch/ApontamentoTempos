using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public RecuperacaoSenhaController(IConfiguration config)
        {
            this.config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostRecuperacaoSenha([FromBody] RecuperacaoSenhaEmail email)
        {
            try
            {
                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    Usuario user = context.Usuarios.Where(b => b.Email == email.Email).FirstOrDefault();

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

                    StringBuilder url = new StringBuilder();
                    url.Append(config["UrlWebSite"]);
                    url.Append("/RecuperacaoSenha?id=");
                    url.Append(reset.Id.ToString());

                    StringBuilder mensagem = new StringBuilder();
                    mensagem.AppendLine("[Apontamento de Tempos]");
                    mensagem.AppendLine();
                    mensagem.Append("Olá ");
                    mensagem.Append(user.Nome);
                    mensagem.Append(", ");
                    mensagem.AppendLine();
                    mensagem.AppendLine();
                    mensagem.AppendLine("Ops, você esqueceu sua senha. :(");
                    mensagem.AppendLine("Não se preocupe, a gente pode te ajudar. Basta acessar o link abaixo e redefinir uma nova senha. ;)");
                    mensagem.AppendLine();
                    mensagem.Append("<span>");
                    mensagem.Append("  <a href='");
                    mensagem.Append(url.ToString());
                    mensagem.Append("' target='_blank'>Clique aqui!</a>");
                    mensagem.Append("</span>");
                    mensagem.AppendLine();
                    mensagem.AppendLine();

                    List<string> destinatarios = new List<string>();
                    destinatarios.Add(email.Email);

                    await EmailHelper.EnvioEmail(EmailHelper.GetEmail(config), destinatarios, "Recuperação de Senha [Apontamento de Tempos]", mensagem.ToString());
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

    public class RecuperacaoSenhaEmail
    {
        public string Email { get; set; }
    }
}