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
    }
}