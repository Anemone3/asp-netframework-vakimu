using System;
using System.Threading.Tasks;
using System.Web.Http;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Services;

namespace com.pe.vakimu.application.Controllers.Api
{
    [RoutePrefix("api/v1/metodo-pago")]
    public class MetodoPagoApiController: ApiController
    {
        private readonly MetodoPagoService _service;

        public MetodoPagoApiController()
        {
            var context = new VakimuDbContext();
            
            _service = new MetodoPagoService(context);
        }
        
           
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var payments = await _service.ObtenerMetodosPago();
                
                
                return Ok(payments);    
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var payments = await _service.ObtenerMetodoPagoPorId(id);
                
                if(payments == null) return NotFound();
                
                return Ok(payments);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}