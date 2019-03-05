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
    public class DashboardController : Controller
    {
        private IConfiguration config;

        public DashboardController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpGet]
        public async Task<List<DashboardTemposUsuario>> Get()
        {
            try
            {
                List<DashboardTemposUsuario> dashboard = new List<DashboardTemposUsuario>();

                using (var context = new MyDbContext(this.config["ConnectionString"]))
                {
                    DateTime dataInicial = new DateTime(DateTime.Now.AddDays(-7).Year, DateTime.Now.AddDays(-7).Month, DateTime.Now.AddDays(-7).Day);
                    DateTime dataFinal = DateTime.Now;
                    
                    var registros = await context.ApontamentoTempos.Where(x => x.UsuarioId == Guid.Parse(User.Claims.Where(b => b.Type == "id_user").FirstOrDefault().Value) && x.Data <= dataFinal && x.Data >= dataInicial).OrderByDescending(x => x.Data).ToListAsync();

                    dashboard.Add(new DashboardTemposUsuario() { Dia = DateTime.Now.ToString("dd/MM"), Tempo = registros.Where(x => x.Data.Day == DateTime.Now.Day).Sum(x => x.Tempo) });
                    dashboard.Add(new DashboardTemposUsuario() { Dia = DateTime.Now.AddDays(-1).ToString("dd/MM"), Tempo = registros.Where(x => x.Data.Day == DateTime.Now.AddDays(-1).Day).Sum(x => x.Tempo) });
                    dashboard.Add(new DashboardTemposUsuario() { Dia = DateTime.Now.AddDays(-2).ToString("dd/MM"), Tempo = registros.Where(x => x.Data.Day == DateTime.Now.AddDays(-2).Day).Sum(x => x.Tempo) });
                    dashboard.Add(new DashboardTemposUsuario() { Dia = DateTime.Now.AddDays(-3).ToString("dd/MM"), Tempo = registros.Where(x => x.Data.Day == DateTime.Now.AddDays(-3).Day).Sum(x => x.Tempo) });
                    dashboard.Add(new DashboardTemposUsuario() { Dia = DateTime.Now.AddDays(-4).ToString("dd/MM"), Tempo = registros.Where(x => x.Data.Day == DateTime.Now.AddDays(-4).Day).Sum(x => x.Tempo) });
                    dashboard.Add(new DashboardTemposUsuario() { Dia = DateTime.Now.AddDays(-5).ToString("dd/MM"), Tempo = registros.Where(x => x.Data.Day == DateTime.Now.AddDays(-5).Day).Sum(x => x.Tempo) });
                    dashboard.Add(new DashboardTemposUsuario() { Dia = DateTime.Now.AddDays(-6).ToString("dd/MM"), Tempo = registros.Where(x => x.Data.Day == DateTime.Now.AddDays(-6).Day).Sum(x => x.Tempo) });
                }

                return dashboard;
            }
            catch
            {
                return new List<DashboardTemposUsuario>();
            }
        }
    }

    public class DashboardTemposUsuario
    {
        public string Dia { get; set; }
        public decimal Tempo { get; set; }
    }
}