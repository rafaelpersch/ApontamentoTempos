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
        public async Task<ActionResult<IEnumerable<Projeto>>> GetAll(string query, int limit, int page, string orderBy, int ascending, string byColumn)
        {
            try
            {
                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    var count = await context.Projetos.Where(x => x.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).CountAsync();

                    if (ascending == 1)
                    {
                        if (limit > 0 || page > 0)
                        {
                            var registros = await context.Projetos.Where(x => x.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderBy(x => x.Nome).Skip((page - 1) * limit).Take(limit).ToListAsync();
                            return Ok(new { registros = registros, count = count });
                        }
                        else
                        {
                            var registros = await context.Projetos.Where(x => x.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderBy(x => x.Nome).ToListAsync();
                            return Ok(new { registros = registros, count = count });
                        }
                    }
                    else
                    {
                        if (limit > 0 || page > 0)
                        {
                            var registros = await context.Projetos.Where(x => x.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderByDescending(x => x.Nome).Skip((page - 1) * limit).Take(limit).ToListAsync();
                            return Ok(new { registros = registros, count = count });
                        }
                        else
                        {
                            var registros = await context.Projetos.Where(x => x.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderByDescending(x => x.Nome).ToListAsync();
                            return Ok(new { registros = registros, count = count });
                        }
                    }
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