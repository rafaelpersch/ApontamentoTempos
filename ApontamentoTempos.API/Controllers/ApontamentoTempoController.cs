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
    public class ApontamentoTempoController : Controller
    {
        private readonly MyDbContext context;

        public ApontamentoTempoController(IConfiguration config)
        {
            this.context = new MyDbContext(config["ConnectionString"]);
        }

        [HttpGet]
        public IEnumerable<ApontamentoTempo> GetApontamentoTempos()
        {
            try
            {
                return context.ApontamentosTempo;
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
                var apontamento = await context.ApontamentosTempo.FindAsync(id);

                if (apontamento == null)
                {
                    return BadRequest("Apontamento de Tempo não encontrado!");
                }

                return Ok(apontamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostApontamentoTempo([FromBody] ApontamentoTempo apontamento)
        {
            try
            {
                apontamento.Id = Guid.NewGuid();
                apontamento.Validar();

                context.ApontamentosTempo.Add(apontamento);
                await context.SaveChangesAsync();

                return CreatedAtAction("GetApontamentoTempo", new { id = apontamento.Id }, apontamento);
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
                var apontamento = await context.ApontamentosTempo.FindAsync(id);

                if (apontamento == null)
                {
                    return BadRequest("Apontamento de Tempo não encontrado!");
                }

                context.ApontamentosTempo.Remove(apontamento);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}