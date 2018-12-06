using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

                List<Usuario> users = context.Usuarios.Where(x => x.Email.Trim().ToLower() == usuario.Email.Trim().ToLower()).ToList();

                if (users != null)
                {
                    if (users.Count > 0)
                    {
                        throw new ApplicationException("Já existe um usuário com esse e-mail!");
                    }
                }

                usuario.Senha = Cryptography(usuario.Senha);

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

        private static string Cryptography(string password)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
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