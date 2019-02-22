using Microsoft.AspNetCore.Mvc;

namespace ApontamentoTempos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        [HttpGet]
        public ActionResult<string> GetAsync()
        {            
            return new JsonResult(new { Message = "Software para Apontamento de Tempos" });
        }
    }
}