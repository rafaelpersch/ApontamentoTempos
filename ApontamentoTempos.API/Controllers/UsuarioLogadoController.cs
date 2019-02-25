using System;
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
                return Ok(User.Identity.Name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}