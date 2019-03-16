using System;
using System.Linq;
using System.Collections.Generic;
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
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration config;

        public LoginController(IConfiguration config)
        {
            this.config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostLogin([FromBody]Login login, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            try
            {
                if (login != null)
                {
                    using (var context = new MyDbContext(config["ConnectionString"]))
                    {
                        Usuario usuarioCadatrado = await context.Usuarios.Where(x => x.Email == login.Email && x.Senha == Cryptography.Encrypt(login.Email + login.Senha)).FirstOrDefaultAsync();

                        if (usuarioCadatrado != null)
                        {
                            Token token = TokenConfigurations.GenerateToken(usuarioCadatrado, signingConfigurations, tokenConfigurations);

                            await context.RefreshTokens.AddAsync(new RefreshToken()
                            {
                                Id = token.RefreshToken,
                                UsuarioId = usuarioCadatrado.Id,
                            });

                            await context.SaveChangesAsync();

                            return Ok(token);
                        }
                    }
                }

                return BadRequest("Falha de login!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}