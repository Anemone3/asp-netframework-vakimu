using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Models.Entities;
using com.pe.vakimu.application.Models.Response;

namespace com.pe.vakimu.application.Models.Repositories{
    public class DetalleOrdenRepositoryImpl 
    {

        private readonly VakimuDbContext _context;
        public DetalleOrdenRepositoryImpl(VakimuDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DetalleOrdenResponse> Add(DetalleOrdenDto dto)
        {
            var detailOrden = new DetalleOrden
            {
                OrdenId = dto.OrdenId,
                ProductoId = dto.ProductoId,
                Descripcion = dto.Descripcion,
                Descuento = dto.Descuento,
                Subtotal = dto.Subtotal,
                Total = dto.Total,
            };

            _context.DetallesOrden.Add(detailOrden);

            await _context.SaveChangesAsync();

            return new DetalleOrdenResponse
            {
                Id = detailOrden.Id,
                ProductoId = detailOrden.ProductoId,
                Descripcion = detailOrden.Descripcion,
                Descuento = detailOrden.Descuento,
                Cantidad = detailOrden.Cantidad,
                Subtotal = detailOrden.Subtotal,
                Total = detailOrden.Total
            };
        }



        // Seria mejor tener una propiedad y manejarlo como deshabilitado
        public async Task<bool> Delete(int id)
        {
      
            var detailOrden = await _context.DetallesOrden.FindAsync(id);
            if (detailOrden == null)
                return false;
            _context.DetallesOrden.Remove(detailOrden);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DetalleOrdenResponse>> GetAll()
        {
            var detalles = await _context.DetallesOrden
                .Include(d => d.Producto)
                .AsNoTracking()
                .ToListAsync();

            return detalles.Select(d => new DetalleOrdenResponse
            {
                Id = d.Id,
                ProductoId = d.ProductoId,
                Descripcion = d.Descripcion,
                Cantidad = d.Cantidad,
                Descuento = d.Descuento,
                Subtotal = d.Subtotal,
                Total = d.Total
            }).ToList();
        }


        public async Task<IEnumerable<DetalleOrdenResponse>> GetByOrderId(int orderId)
        {
            var detalles = await _context.DetallesOrden
                .Include(p => p.Producto)
                .AsNoTracking()
                .Where(x => x.OrdenId == orderId)
                .ToListAsync();

            if (!detalles.Any())
                throw new KeyNotFoundException($"No se encontraron detalles para la orden con ID {orderId}.");

            return detalles.Select(d => new DetalleOrdenResponse
            {
                Id = d.Id,
                ProductoId = d.ProductoId,
                Descripcion = d.Descripcion,
                Descuento = d.Descuento,
                Cantidad = d.Cantidad,
                Subtotal = d.Subtotal,
                Total = d.Total
            });
        }



        public async Task<bool> Update(int id, DetalleOrdenUpdateDto dto)
        {
            var detalle = await _context.DetallesOrden.FindAsync(id);
            if (detalle == null) return false;

            if (dto.OrdenId.HasValue) detalle.OrdenId = dto.OrdenId.Value;
            if (dto.ProductoId.HasValue) detalle.ProductoId = dto.ProductoId.Value;
            if (dto.Descripcion != null) detalle.Descripcion = dto.Descripcion;
            if(dto.Cantidad.HasValue) detalle.Cantidad = dto.Cantidad.Value;
            if (dto.Descuento.HasValue) detalle.Descuento = dto.Descuento.Value;
            if (dto.Subtotal.HasValue) detalle.Subtotal = dto.Subtotal.Value;
            if (dto.Total.HasValue) detalle.Total = dto.Total.Value;

            await _context.SaveChangesAsync();
            return true;
        }


    }
}