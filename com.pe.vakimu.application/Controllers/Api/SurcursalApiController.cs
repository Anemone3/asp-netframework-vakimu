using System;
using System.Threading.Tasks;
using System.Web.Http;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Services;

namespace com.pe.vakimu.application.Controllers.Api
{
    
    [RoutePrefix("api/v1/surcursales")]
    public class SurcursalApiController: ApiController
    {

        private readonly SucursalService _service;
        
        public SurcursalApiController()
        {
            var context = new VakimuDbContext();
            _service = new SucursalService(context);
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var sucursales = await _service.ObtenerSucursales();

                return Ok(sucursales);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}