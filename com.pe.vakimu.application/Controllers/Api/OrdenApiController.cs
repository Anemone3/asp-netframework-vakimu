using System;
using System.Threading.Tasks;
using System.Web.Http;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Services;

namespace com.pe.vakimu.application.Controllers.Api
{
    [RoutePrefix("api/v1/ordenes")]
    public class OrdenApiController : ApiController
    {
        private readonly OrdenService _service;

        public OrdenApiController()
        {
            var context = new VakimuDbContext();
            _service = new OrdenService(context);
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var ordenes = await _service.ObtenerOrdenes();

                return Ok(ordenes);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        
        [HttpGet]
        [Route("{id:int}/detalle")]
        public async Task<IHttpActionResult> GetDetallesByOrderId(int id)
        {
            try
            {
                var detallesOrdenes = await _service.ObtenerDetallesPorOrdenId(id);
                
                return Ok(detallesOrdenes);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            } 
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] OrdenDetalleDto dto)
        {
            try
            {
                if (dto.Detalles == null || dto.Detalles.Count <= 0) return BadRequest("Se necesita al menos un detalle de orden");
                
                var orden = await _service.CrearOrdenConDetalles(dto.Orden,dto.Detalles,dto.Direccion);
                
                return Ok(orden);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("cliente")]
        public async Task<IHttpActionResult> GetOrdersByClientId([FromUri] int id)
        {
            try
            {
                var orden = await _service.ObtenerOrdenesPorClienteId(id);
                
                return Ok(orden);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }            
        }
        
        [HttpPost]
        [Route("{id:int}/status")]
        public async Task<IHttpActionResult> Patch(int id,[FromBody] string status)
        {
            try
            {
                var orden = await _service.CambiarOrdenStatus(id,status);
                
                if(!orden) return BadRequest($"Error al cambiar la orden {status}");
                
                return Ok(new { message = $"Orden cambiado a {status}" });
                
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        
     
    }
}