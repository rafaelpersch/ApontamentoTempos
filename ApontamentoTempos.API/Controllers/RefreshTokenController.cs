using System;
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
    public class RefreshTokenController : Controller
    {
        private IConfiguration config;

        public RefreshTokenController(IConfiguration config)
        {
            this.config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostRefreshToken([FromBody]RefreshToken refreshToken, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            try
            {
                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    RefreshToken refreshTokenBase = await context.RefreshTokens.FindAsync(refreshToken.Id);

                    if (refreshTokenBase != null)
                    {
                        Token token = TokenConfigurations.GenerateToken(refreshTokenBase.Usuario, signingConfigurations, tokenConfigurations);

                        await context.RefreshTokens.AddAsync(new RefreshToken()
                        {
                            Id = token.RefreshToken,
                            UsuarioId = refreshTokenBase.UsuarioId,
                        });

                        context.RefreshTokens.Remove(refreshTokenBase);

                        await context.SaveChangesAsync();

                        return Ok(token);
                    }
                    else
                    {
                        return BadRequest("Falha de RefreshToken não encontrado!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}