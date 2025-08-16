using System;
using System.Threading.Tasks;
using System.Web.Http;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Services;

namespace com.pe.vakimu.application.Controllers.Api
{
    
    [RoutePrefix("api/v1/departamentos")]
    public class DepartamentoApiController  : ApiController
    {
        private readonly DepartamentoService _service;

        public DepartamentoApiController()
        {
            var context = new VakimuDbContext();
            _service = new DepartamentoService(context);
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetDepartamentos()
        {
            try
            {
                var departamentos = await _service.ObtenerDepartamentos();


                return Ok(departamentos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}