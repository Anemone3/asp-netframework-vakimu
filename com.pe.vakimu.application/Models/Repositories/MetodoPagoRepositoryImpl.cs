using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Entities;
using com.pe.vakimu.application.Models.Response;

namespace com.pe.vakimu.application.Models.Repositories
{
    public class MetodoPagoRepositoryImpl
    {
        private readonly VakimuDbContext _context;
        public MetodoPagoRepositoryImpl(VakimuDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<MetodoPagoResponse>> GetAll()
        {
            var payments = await _context.MetodosPago
                .AsNoTracking()
                .ToListAsync();

            return payments.Select(c => new MetodoPagoResponse
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Instrucciones = c.Instrucciones
            }).ToList();
        }

        public async Task<MetodoPagoResponse> GetById(int id)
        {
            var payment = await _context.MetodosPago
                .AsNoTracking()
                .FirstOrDefaultAsync(mp => mp.Id == id);


            if (payment == null) return null;
            
            return new MetodoPagoResponse
            {
                Id = payment.Id,
                Nombre = payment.Nombre,
                Instrucciones = payment.Instrucciones
            };

        }
    }
}