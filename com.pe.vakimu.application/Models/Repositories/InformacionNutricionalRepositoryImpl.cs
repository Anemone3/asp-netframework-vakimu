using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Dto;
using com.pe.vakimu.application.Models.Entities;
using com.pe.vakimu.application.Models.Response;


namespace com.pe.vakimu.application.Models.Repositories{
    public class InformacionNutricionalRepositoryImpl 
    {

        private readonly VakimuDbContext _context;
        public InformacionNutricionalRepositoryImpl(VakimuDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<InfoNutricion> Add(InformacionNutricionalDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.IdProducto <= 0)
                throw new ArgumentException("El ID del producto debe ser mayor a cero.");

            var yaExiste = await _context.InformacionNutricional
                .AnyAsync(i => i.IdProducto == dto.IdProducto);

            if (yaExiste)
                throw new InvalidOperationException("Ya existe información nutricional para este producto.");

            var infoNutricional = new InformacionNutricional
            {
                IdProducto = dto.IdProducto,
                EnergiaKcal = dto.EnergiaKcal ?? 0,
                GrasaTotal = dto.GrasaTotal ?? 0,
                GrasaSaturada = dto.GrasaSaturada ?? 0,
                GrasaTrans = dto.GrasaTrans ?? 0,
                ColesterolMg = dto.ColesterolMg ?? 0,
                SodioMg = dto.SodioMg ?? 0,
                CarbohidratosTotales = dto.CarbohidratosTotales ?? 0,
                FibraDietaria = dto.FibraDietaria ?? 0,
                AzucaresTotales = dto.AzucaresTotales ?? 0,
                AzucaresAnhadidos = dto.AzucaresAnhadidos ?? 0,
                Proteina = dto.Proteina ?? 0
            };

            _context.InformacionNutricional.Add(infoNutricional);
            await _context.SaveChangesAsync();

            // Retornar el DTO mapeado
            return new InfoNutricion
            {
                EnergiaKcal = infoNutricional.EnergiaKcal,
                GrasaTotal = infoNutricional.GrasaTotal,
                GrasaSaturada = infoNutricional.GrasaSaturada,
                GrasaTrans = infoNutricional.GrasaTrans,
                ColesterolMg = infoNutricional.ColesterolMg,
                SodioMg = infoNutricional.SodioMg,
                CarbohidratosTotales = infoNutricional.CarbohidratosTotales,
                FibraDietaria = infoNutricional.FibraDietaria,
                AzucaresTotales = infoNutricional.AzucaresTotales,
                AzucaresAnhadidos = infoNutricional.AzucaresAnhadidos,
                Proteina = infoNutricional.Proteina
            };
        }



        public async Task<bool> Delete(int id)
        {
            var infoNutricional = await _context.InformacionNutricional.FindAsync(id);
            if (infoNutricional == null)
            {
                return false;
            }
            _context.InformacionNutricional.Remove(infoNutricional);
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<IEnumerable<InfoNutricion>> GetAll()
        {
            var infoNutri = await _context.InformacionNutricional
                .Include("Producto") // Si deseas usar la información del producto
                .ToListAsync();

            return infoNutri.Select(c => new InfoNutricion
            {
                EnergiaKcal = c.EnergiaKcal,
                GrasaTotal = c.GrasaTotal,
                GrasaSaturada = c.GrasaSaturada,
                GrasaTrans = c.GrasaTrans,
                ColesterolMg = c.ColesterolMg,
                SodioMg = c.SodioMg,
                CarbohidratosTotales = c.CarbohidratosTotales,
                FibraDietaria = c.FibraDietaria,
                AzucaresTotales = c.AzucaresTotales,
                AzucaresAnhadidos = c.AzucaresAnhadidos,
                Proteina = c.Proteina
            });
        }


        public async Task<InfoNutricion> GetByProductId(int productId)
        {
            var info = await _context.InformacionNutricional
                .FirstOrDefaultAsync(i => i.IdProducto == productId);

            if (info == null)
                return null;

            return new InfoNutricion
            {
                EnergiaKcal = info.EnergiaKcal,
                GrasaTotal = info.GrasaTotal,
                GrasaSaturada = info.GrasaSaturada,
                GrasaTrans = info.GrasaTrans,
                ColesterolMg = info.ColesterolMg,
                SodioMg = info.SodioMg,
                CarbohidratosTotales = info.CarbohidratosTotales,
                FibraDietaria = info.FibraDietaria,
                AzucaresTotales = info.AzucaresTotales,
                AzucaresAnhadidos = info.AzucaresAnhadidos,
                Proteina = info.Proteina
            };
        }


        public async Task<bool> Update(int productId, InformacionNutricionalUpdateDto dto)
        {
            var infoNutricional = await _context.InformacionNutricional
                .FirstOrDefaultAsync(i => i.IdProducto == productId);

            if (infoNutricional == null)
                return false;

            if (dto.EnergiaKcal.HasValue) infoNutricional.EnergiaKcal = dto.EnergiaKcal.Value;
            if (dto.GrasaTotal.HasValue) infoNutricional.GrasaTotal = dto.GrasaTotal.Value;
            if (dto.GrasaSaturada.HasValue) infoNutricional.GrasaSaturada = dto.GrasaSaturada.Value;
            if (dto.GrasaTrans.HasValue) infoNutricional.GrasaTrans = dto.GrasaTrans.Value;
            if (dto.ColesterolMg.HasValue) infoNutricional.ColesterolMg = dto.ColesterolMg.Value;
            if (dto.SodioMg.HasValue) infoNutricional.SodioMg = dto.SodioMg.Value;
            if (dto.CarbohidratosTotales.HasValue) infoNutricional.CarbohidratosTotales = dto.CarbohidratosTotales.Value;
            if (dto.FibraDietaria.HasValue) infoNutricional.FibraDietaria = dto.FibraDietaria.Value;
            if (dto.AzucaresTotales.HasValue) infoNutricional.AzucaresTotales = dto.AzucaresTotales.Value;
            if (dto.AzucaresAnhadidos.HasValue) infoNutricional.AzucaresAnhadidos = dto.AzucaresAnhadidos.Value;
            if (dto.Proteina.HasValue) infoNutricional.Proteina = dto.Proteina.Value;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}