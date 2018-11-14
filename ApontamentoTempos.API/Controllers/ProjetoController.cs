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
    public class ProjetoController : Controller
    {
        private readonly MyDbContext context;

        public ProjetoController(IConfiguration config)
        {
            this.context = new MyDbContext(config["ConnectionString"]);
        }

        // GET: api/Projetos
        [HttpGet]
        public IEnumerable<Projeto> GetProjetos()
        {
            try
            {
                return context.Projetos;
            }
            catch
            {
                return new List<Projeto>();
            }
        }

        // GET: api/Projetos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjeto([FromRoute] Guid id)
        {
            try
            {
                var projeto = await context.Projetos.FindAsync(id);

                if (projeto == null)
                {
                    return BadRequest("Projeto não encontrado!");
                }

                return Ok(projeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Projetos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjeto([FromRoute] Guid id, [FromBody] Projeto projeto)
        {
            try
            {
                projeto.Validar();

                if (id != projeto.Id)
                {
                    return BadRequest("Ids não conferem!");
                }

                context.Entry(projeto).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return Ok("Alteração efetuada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Projetos
        [HttpPost]
        public async Task<IActionResult> PostProjeto([FromBody] Projeto projeto)
        {
            try
            {
                projeto.Id = Guid.NewGuid();
                projeto.Validar();

                context.Projetos.Add(projeto);
                await context.SaveChangesAsync();

                return CreatedAtAction("GetProjeto", new { id = projeto.Id }, projeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Projetos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjeto([FromRoute] Guid id)
        {
            try
            {
                var projeto = await context.Projetos.FindAsync(id);
                if (projeto == null)
                {
                    return BadRequest("Projeto não encontrado!");
                }

                if (context.ApontamentosTempo.Where(b => b.ProjetoId == id).FirstOrDefault() != null)
                {
                    return BadRequest("Projeto com tempos já apontados!");
                }

                context.Projetos.Remove(projeto);
                await context.SaveChangesAsync();

                return Ok(projeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}