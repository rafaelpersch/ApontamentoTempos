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
    public class ApontamentoTempoController : Controller
    {
        private IConfiguration config;

        public ApontamentoTempoController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApontamentoTempo>>> GetAll(string query, int limit, int page, string orderBy, int ascending, string byColumn)
        {
            try
            {
                var registros = new List<ApontamentoTempo>();

                using (var context = new MyDbContext(config["ConnectionString"]))
                {
                    var count = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).CountAsync();

                    if (ascending == 1)
                    {
                        if (limit > 0 || page > 0)
                        {
                            if (orderBy == "projeto")
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderBy(x => x.Projeto.Nome).Skip((page - 1) * limit).Take(limit).ToListAsync();
                            }
                            else if (orderBy == "issue")
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderBy(x => x.Issue).Skip((page - 1) * limit).Take(limit).ToListAsync();
                            }
                            else
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderBy(x => x.Data).Skip((page - 1) * limit).Take(limit).ToListAsync();
                            }
                            
                            return Ok(new { registros = ApontamentoTempo.GerarListaEstatica(registros), count = count });
                        }
                        else
                        {
                            if (orderBy == "projeto")
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderBy(x => x.Projeto.Nome).ToListAsync();
                            }
                            else if (orderBy == "issue")
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderBy(x => x.Issue).ToListAsync();
                            }
                            else
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderBy(x => x.Data).ToListAsync();
                            }
                            
                            return Ok(new { registros = ApontamentoTempo.GerarListaEstatica(registros), count = count });
                        }
                    }
                    else
                    {
                        if (limit > 0 || page > 0)
                        {
                            if (orderBy == "projeto")
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderByDescending(x => x.Projeto.Nome).Skip((page - 1) * limit).Take(limit).ToListAsync();
                            }
                            else if (orderBy == "issue")
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderByDescending(x => x.Issue).Skip((page - 1) * limit).Take(limit).ToListAsync();
                            }
                            else
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderByDescending(x => x.Data).Skip((page - 1) * limit).Take(limit).ToListAsync();
                            }
                            
                            return Ok(new { registros = ApontamentoTempo.GerarListaEstatica(registros), count = count });
                        }
                        else
                        {
                            if (orderBy == "projeto")
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderByDescending(x => x.Projeto.Nome).ToListAsync();
                            }
                            else if (orderBy == "issue")
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderByDescending(x => x.Issue).ToListAsync();
                            }
                            else
                            {
                                registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Projeto.Nome.Contains((string.IsNullOrEmpty(query)) ? string.Empty : query)).OrderByDescending(x => x.Data).ToListAsync();
                            }
                            
                            return Ok(new { registros = ApontamentoTempo.GerarListaEstatica(registros), count = count });
                        }
                    }
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
                    var tempo = await context.ApontamentoTempos.FindAsync(id);

                    if (tempo == null)
                    {
                        return Ok(null);
                    }

                    var tempos = new List<ApontamentoTempo>();
                    tempos.Add(tempo);

                    return Ok(ApontamentoTempo.GerarListaEstatica(tempos)[0]);
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

                apontamentoTempo.UsuarioId = Guid.Parse(User.Claims.Where(x => x.Type == "id_user").FirstOrDefault().Value);
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
                apontamentoTempo.UsuarioId = Guid.Parse(User.Claims.Where(x => x.Type == "id_user").FirstOrDefault().Value);
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