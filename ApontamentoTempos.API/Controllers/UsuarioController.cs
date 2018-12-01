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
    public class UsuarioController : Controller
    {
        private readonly MyDbContext context;

        public UsuarioController(IConfiguration config)
        {
            this.context = new MyDbContext(config["ConnectionString"]);
        }

        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            try
            {
                usuario.Id = Guid.NewGuid();
                usuario.Validar();

                context.Usuarios.Add(usuario);
                await context.SaveChangesAsync();

                return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
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

        //Token?
        //Login
        //Logout
        //Registre-se
        //Trocar senha
        //Get(logado)
    }
}