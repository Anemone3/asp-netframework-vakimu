using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Response;
using System.Threading.Tasks;

namespace com.pe.vakimu.application.Models.Repositories{
    public class DepartementoRepositoryImpl
    {
        private readonly VakimuDbContext _context;
        public DepartementoRepositoryImpl(VakimuDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<DepartamentoResponse>> GetAll()
        {
            var departamento = await _context.Departamentos
                .AsNoTracking()
                .ToListAsync();

            if (!departamento.Any())
            {
                return new List<DepartamentoResponse>();
            }

            return departamento.Select(d => new DepartamentoResponse
            {
                Id = d.Id,
                Nombre = d.Nombre
            }).ToList();
        }


    }
}