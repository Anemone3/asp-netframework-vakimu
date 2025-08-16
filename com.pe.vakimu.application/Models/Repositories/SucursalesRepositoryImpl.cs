using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Response;

namespace com.pe.vakimu.application.Models.Repositories
{
    public class SucursalesRepositoryImpl
    {
        private readonly VakimuDbContext _context;
        public SucursalesRepositoryImpl(VakimuDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<SucursalResponse>> GetAll()
        {
            var sucursal = await _context.Sucursales
                .AsNoTracking()
                .ToListAsync();

            return sucursal.Select(s => new SucursalResponse
            {
                Id = s.Id,
                Ubicacion = s.Ubicacion,
                Longitud = s.Longitud,
                Latitud = s.Latitud,
                DepartamentoId = s.DepartamentoId,
                Sede = s.Sede,
                Calle = s.Calle
            });
        }
        public async Task<SucursalResponse> GetById(int id)
        {
            var sucursal = await _context.Sucursales.Include(s => s.Departamento)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            return sucursal == null ? throw new KeyNotFoundException($"Sucursal with id {id} not found.") :
                new SucursalResponse { 
                    Id = sucursal.Id,
                    Ubicacion = sucursal.Ubicacion,
                    Longitud = sucursal.Longitud,
                    Latitud = sucursal.Latitud,
                    DepartamentoId = sucursal.DepartamentoId,
                    Sede = sucursal.Sede,
                    Calle = sucursal.Calle
                };
        }
    }
}