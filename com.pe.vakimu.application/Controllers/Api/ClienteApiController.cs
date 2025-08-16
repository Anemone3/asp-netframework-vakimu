using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Services;

namespace com.pe.vakimu.application.Controllers.Api
{
    [RoutePrefix("api/v1/clientes")]
    public class ClienteApiController: ApiController
    {

        private ClienteService _service;

        public ClienteApiController()
        {
            var context = new VakimuDbContext();
            _service = new ClienteService(context);
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
  
            try
            {
                var clients = (await _service.ObtenerClientes()).ToList();
                
                return Ok(clients);
            
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] ClienteDto dto)
        {
            try
            {
                var client = await _service.CrearCliente(dto);
                
                return Ok(client);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            
            
        }
    }
}