using System;
using System.Threading.Tasks;
using System.Web.Http;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Services;

namespace com.pe.vakimu.application.Controllers.Api
{
    
    [RoutePrefix("api/v1/detalles")]
    public class DetalleOrdenApiController : ApiController
    {
        private readonly OrdenService _service;

        public DetalleOrdenApiController()
        {
            var context = new VakimuDbContext();
            _service = new OrdenService(context);
        }
        
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetDetalles()
        {
            try
            {
                var detallesOrdenes = await _service.ObtenerDetallesOrdenesTodo();
                
                return Ok(detallesOrdenes);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            } 
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetDetalle(int id)
        {
            try
            {
                var detalle = await _service.ObtenerOrdenesConDetalles(id);

                return Ok(detalle);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}