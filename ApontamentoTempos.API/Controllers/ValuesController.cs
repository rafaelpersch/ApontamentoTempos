using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApontamentoTempos.API.Tools;
using Microsoft.AspNetCore.Mvc;

namespace ApontamentoTempos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        {
            List<object> objs = new List<object>();
            objs.Add(new { c1 = "value1", c2 = "value2" });

            var dest = new List<string>();
            dest.Add("rafael.persch@softbyte.com.br");

            //await EmailHelper.EnvioEmail(dest, "teste", "mimimim");

            return new JsonResult(objs);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
