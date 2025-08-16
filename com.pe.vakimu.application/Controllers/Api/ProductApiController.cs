using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Services;

namespace com.pe.vakimu.application.Controllers.Api
{
    [RoutePrefix("api/v1/productos")]
    public class ProductApiController: ApiController
    {

        private readonly ProductoService _service;

        public ProductApiController()
        {
            var context = new VakimuDbContext();
            _service = new ProductoService(context);
        }
        
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var products = await _service.ObtenerProductos();
                
                
                return Ok(products);    
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
                var product = await _service.ObtenerProductoPorId(id);
                return Ok(product);
            }
            catch (KeyNotFoundException e)
            {
                return Content(HttpStatusCode.NotFound, new {
                    message = e.Message
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] ProductoCompletoDto dto)
        {
            try
            {
                var product = await _service.AgregarProducto(dto.Producto,dto.InformacionNutricional);
                return Ok(product);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
     
            try
            {
                var product = await _service.ObtenerProductoPorId(id);
                if (product == null) return NotFound();
                
                var isDeleted = await _service.EliminarProducto(id);

                if (!isDeleted) return NotFound();
                
                return Ok(new { message = "Producto deshabilitado correctamente" });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("{id:int}/stock")]
        public async Task<IHttpActionResult> GetStock(int id)
        {
            try
            {

                var product = await _service.ObtenerProductoPorId(id);
                var isAvaliable = await _service.ValidarStock(product.Id);
                
                if(!isAvaliable) return Ok(new {message= "No disponible o stock agotado"});
                return Ok(new { productId = product.Id, message = $"Producto {product.Nombre} tiene {product.Stock} stock disponible", stock = product.Stock });
            }
            catch (KeyNotFoundException e)
            {
                return Content(HttpStatusCode.NotFound, new {
                    message = e.Message
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        
    }
}