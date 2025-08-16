using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Models.Repositories;
using com.pe.vakimu.application.Models.Response;

namespace com.pe.vakimu.application.Services
{
    public class ProductoService
    {
        private readonly ProductoRepositoryImpl _productoRep;
        private readonly InformacionNutricionalRepositoryImpl _informacionNutricionalRep;

        public ProductoService(VakimuDbContext context)
        {
            _productoRep = new ProductoRepositoryImpl(context);
            _informacionNutricionalRep =  new InformacionNutricionalRepositoryImpl(context);
        }

        
        // Crear un producto
        public async Task<ProductoConInfoNutricional> AgregarProducto(ProductoDto productoDto, InformacionNutricionalDto informacionNutricionalDto)
        {
            var producto = await _productoRep.Add(productoDto);

            if (informacionNutricionalDto != null)
            {
                informacionNutricionalDto.IdProducto = producto.Id;
                await _informacionNutricionalRep.Add(informacionNutricionalDto);
            }

            return await this.ObtenerProductoPorId(producto.Id);
            
        }

        
        // Actualizar datos del producto, y la info nutricional si se envia o crearla alli mismo
        public async Task<ProductoConInfoNutricional> ActualizarProducto(int productId,
            ProductoUpdateDto productoUpdateDto, InformacionNutricionalUpdateDto infoNutroUpdateDto)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("El ID del producto debe ser mayor que cero.");
            }

            var productExists = await _productoRep.GetById(productId);
            
            if (productExists == null)
            {
                throw new KeyNotFoundException($"No se encontró el producto con ID {productId}.");
            }

            if (productoUpdateDto != null)
            {
                await _productoRep.Update(productId, productoUpdateDto);
            }

            if (infoNutroUpdateDto != null)
            {
                var infoActual = await _informacionNutricionalRep.GetByProductId(productId);
                if (infoActual == null)
                {
                    infoNutroUpdateDto.IdProducto = productId;

                    await _informacionNutricionalRep.Add(new InformacionNutricionalDto
                    {
                        EnergiaKcal = infoNutroUpdateDto.EnergiaKcal,
                        GrasaTotal = infoNutroUpdateDto.GrasaTotal,
                        GrasaSaturada = infoNutroUpdateDto.GrasaSaturada,
                        GrasaTrans = infoNutroUpdateDto.GrasaTrans,
                        ColesterolMg = infoNutroUpdateDto.ColesterolMg,
                        SodioMg = infoNutroUpdateDto.SodioMg,
                        CarbohidratosTotales = infoNutroUpdateDto.CarbohidratosTotales,
                        FibraDietaria = infoNutroUpdateDto.FibraDietaria,
                        AzucaresTotales = infoNutroUpdateDto.AzucaresTotales,
                        AzucaresAnhadidos = infoNutroUpdateDto.AzucaresAnhadidos,
                        Proteina = infoNutroUpdateDto.Proteina
                    });
                }
                else
                {
                    var actualizadoInfo = await _informacionNutricionalRep.Update(productId, infoNutroUpdateDto);
                    if (!actualizadoInfo)
                        throw new Exception("Error al actualizar la información nutricional.");
                }
            }


   
            return await ObtenerProductoPorId(productId);
        }

        // Lista de todos los productos con su info nutricional de cada uno
        public async Task<IEnumerable<ProductoConInfoNutricional>> ObtenerProductos()
        {
            return await _productoRep.GetAll();
        }
        
        // Detalle del producto segun su id
        public async Task<ProductoConInfoNutricional> ObtenerProductoPorId(int productId)
        {
            return await _productoRep.GetById(productId);
        }

        // Deshabilita el producto
        public async Task<bool> EliminarProducto(int productId)
        {
            return await _productoRep.Update(productId,new ProductoUpdateDto
            {
                Disponible = false
            });
        }
    
        
        // Validar stock, retorna un boolean TRUE si aun esta para uso.
        public async Task<bool> ValidarStock(int productId)
        {
            var product = await this.ObtenerProductoPorId(productId);
            
            if (!product.Disponible) return product.Disponible;

            return product.Stock >= 1;
        }
    }
}