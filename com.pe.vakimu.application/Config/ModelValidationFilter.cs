using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace com.pe.vakimu.application.Config
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var errores = actionContext.ModelState
                    .Where(ms => ms.Value.Errors.Any())
                    .Select(ms => new
                    {
                        Campo = ms.Key,
                        Errores = ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    });

                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new
                    {
                        mensaje = "Datos inválidos",
                        errores
                    });
            }
        }
    }
}