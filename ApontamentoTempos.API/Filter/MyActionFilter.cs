using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ApontamentoTempos.API.Filter
{
    public class MyActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // faz algo antes da execução da ação

            try
            {
                string cookie = context.HttpContext.Request.Cookies[""];

                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            catch
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // faz alguma coisa depois que a ação é executada
        }
    }
}
