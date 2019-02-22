using System;
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
    public class ApontamentoTempoController : Controller
    {
        private IConfiguration config;

        public ApontamentoTempoController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApontamentoTempo>>> GetAll(string query, string limit, string page, string orderBy, string ascending, string byColumn)
        {
            // TODO: Implementar filtros

            try
            {
                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    return await context.ApontamentoTempos.ToListAsync();
                }
            }
            catch
            {
                return new List<ApontamentoTempo>();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApontamentoTempo([FromRoute] Guid id)
        {
            try
            {
                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    return Ok(await context.ApontamentoTempos.FindAsync(id));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutApontamentoTempo([FromRoute] Guid id, [FromBody] ApontamentoTempo apontamentoTempo)
        {
            try
            {
                if (id != apontamentoTempo.Id)
                {
                    return BadRequest("Ids não conferem!");
                }

                apontamentoTempo.Validar();

                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    context.Entry(apontamentoTempo).State = EntityState.Modified;

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
        public async Task<IActionResult> PostApontamentoTempo([FromBody] ApontamentoTempo apontamentoTempo)
        {
            try
            {
                apontamentoTempo.Id = Guid.NewGuid();
                apontamentoTempo.Validar();

                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    context.ApontamentoTempos.Add(apontamentoTempo);
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
        public async Task<IActionResult> DeleteApontamentoTempo([FromRoute] Guid id)
        {
            try
            {
                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    var apontamentoTempo = await context.ApontamentoTempos.FindAsync(id);

                    if (apontamentoTempo == null)
                    {
                        throw new ApplicationException("ApontamentoTempo não encontrado!");
                    }

                    context.ApontamentoTempos.Remove(apontamentoTempo);
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