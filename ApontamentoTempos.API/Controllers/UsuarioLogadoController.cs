using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApontamentoTempos.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioLogadoController : Controller
    {
        public UsuarioLogadoController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(User.Claims.Where(x => x.Type == "name_user").FirstOrDefault().Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}