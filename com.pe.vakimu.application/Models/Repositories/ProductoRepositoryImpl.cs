using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Models.Entities;
using com.pe.vakimu.application.Models.Response;

namespace com.pe.vakimu.application.Models.Repositories
{
    public class ProductoRepositoryImpl 
    {

        private readonly VakimuDbContext _context;

        public ProductoRepositoryImpl(VakimuDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ProductoConInfoNutricional> Add(ProductoDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Sku = dto.Sku,
                Precio = dto.Precio,
                Stock = dto.Stock,
                PrecioOferta = dto.PrecioOferta,
                Disponible = dto.Disponible,
                Imagen = dto.Imagen,
                FechaActualizada = DateTime.Now

            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync(); 

            return new ProductoConInfoNutricional
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Sku = producto.Sku,
                Precio = producto.Precio,
                Stock = producto.Stock,
                PrecioOferta = producto.PrecioOferta ?? 0.00m,
                Disponible = producto.Disponible,
                Imagen = producto.Imagen,
                InfoNutricional = null
            };

        }

        public async Task<bool> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return false;
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductoConInfoNutricional>> GetAll()
        {
            var productos = await _context.Productos
                .Include(p => p.InformacionNutricional)
                .AsNoTracking()
                .ToListAsync();

            return productos.Select(p => new ProductoConInfoNutricional
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Sku = p.Sku,
                Precio = p.Precio,
                Stock = p.Stock,
                PrecioOferta = p.PrecioOferta ?? 0.00m,
                Disponible = p.Disponible,
                Imagen = p.Imagen,
                InfoNutricional = p.InformacionNutricional == null ? null : new InfoNutricion
                {
                    EnergiaKcal = p.InformacionNutricional.EnergiaKcal,
                    GrasaTotal = p.InformacionNutricional.GrasaTotal,
                    GrasaSaturada = p.InformacionNutricional.GrasaSaturada,
                    GrasaTrans = p.InformacionNutricional.GrasaTrans,
                    ColesterolMg = p.InformacionNutricional.ColesterolMg,
                    SodioMg = p.InformacionNutricional.SodioMg,
                    CarbohidratosTotales = p.InformacionNutricional.CarbohidratosTotales,
                    FibraDietaria = p.InformacionNutricional.FibraDietaria,
                    AzucaresTotales = p.InformacionNutricional.AzucaresTotales,
                    AzucaresAnhadidos = p.InformacionNutricional.AzucaresAnhadidos,
                    Proteina = p.InformacionNutricional.Proteina
                }
            }).ToList();
        }


        public async Task<ProductoConInfoNutricional> GetById(int id)
        {
            var p = await _context.Productos
                .Include(x => x.InformacionNutricional)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (p == null)
                throw new KeyNotFoundException($"Producto with id {id} not found.");

            return new ProductoConInfoNutricional
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Sku = p.Sku,
                Precio = p.Precio,
                PrecioOferta = p.PrecioOferta ?? 0.00m,
                Stock = p.Stock,
                Disponible = p.Disponible,
                Imagen = p.Imagen,
                InfoNutricional = p.InformacionNutricional == null ? null : new InfoNutricion
                {
                    EnergiaKcal = p.InformacionNutricional.EnergiaKcal,
                    GrasaTotal = p.InformacionNutricional.GrasaTotal,
                    GrasaSaturada = p.InformacionNutricional.GrasaSaturada,
                    GrasaTrans = p.InformacionNutricional.GrasaTrans,
                    ColesterolMg = p.InformacionNutricional.ColesterolMg,
                    SodioMg = p.InformacionNutricional.SodioMg,
                    CarbohidratosTotales = p.InformacionNutricional.CarbohidratosTotales,
                    FibraDietaria = p.InformacionNutricional.FibraDietaria,
                    AzucaresTotales = p.InformacionNutricional.AzucaresTotales,
                    AzucaresAnhadidos = p.InformacionNutricional.AzucaresAnhadidos,
                    Proteina = p.InformacionNutricional.Proteina
                }
            };
        }


        public async Task<bool> Update(int id, ProductoUpdateDto dto)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                throw new KeyNotFoundException($"Producto with id {id} not found.");
            }

            if(dto.Nombre != null) producto.Nombre = dto.Nombre;
            if(dto.Imagen != null) producto.Imagen = dto.Imagen;
            if (dto.Descripcion != null) producto.Descripcion = dto.Descripcion;
            if (dto.Sku != null) producto.Sku = dto.Sku;
            if (dto.Precio.HasValue) producto.Precio = dto.Precio.Value;
            if(dto.Stock.HasValue) producto.Stock = dto.Stock.Value;
            if (dto.PrecioOferta.HasValue) producto.PrecioOferta = dto.PrecioOferta.Value;
            if (dto.Disponible.HasValue) producto.Disponible = dto.Disponible.Value;
            producto.FechaActualizada = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;


        }
    }
}