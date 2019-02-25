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

namespace ApontamentoTempos.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : Controller
    {
        private IConfiguration config;

        public ProjetoController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projeto>>> GetAll(string query, int limit, int page, string orderBy, string ascending, string byColumn)
        {
            // TODO: Implementar filtros

            try
            {
                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    /*var result = from s in context.Projetos
                                   where EF.Functions.Like(s.Nome, "%" + query + "%")
                                select s;

                    return Ok(await result.ToListAsync());*/

                    return Ok(await context.Projetos.Where(x => x.Nome.Contains(query)).OrderBy(x => x.Nome).Skip(page).Take(limit).ToListAsync());

                    // return Ok(await context.Projetos.ToListAsync());
                }
            }
            catch
            {
                return new List<Projeto>();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjeto([FromRoute] Guid id)
        {
            try
            {
                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    return Ok(await context.Projetos.FindAsync(id));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjeto([FromRoute] Guid id, [FromBody] Projeto projeto)
        {
            try
            {
                if (id != projeto.Id)
                {
                    return BadRequest("Ids não conferem!");
                }

                projeto.Validar();

                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    context.Entry(projeto).State = EntityState.Modified;

                    await context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostProjeto([FromBody] Projeto projeto)
        {
            try
            {
                projeto.Id = Guid.NewGuid();
                projeto.Validar();

                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    context.Projetos.Add(projeto);
                    await context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjeto([FromRoute] Guid id)
        {
            try
            {
                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    var projeto = await context.Projetos.FindAsync(id);

                    if (projeto == null)
                    {
                        throw new ApplicationException("Projeto não encontrado!");
                    }

                    if (context.ApontamentoTempos.Where(b => b.ProjetoId == id).FirstOrDefault() != null)
                    {
                        throw new ApplicationException("Projeto com tempos já apontados!");
                    }

                    context.Projetos.Remove(projeto);
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