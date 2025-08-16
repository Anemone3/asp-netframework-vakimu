using System.Collections.Generic;
using System.Threading.Tasks;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Repositories;
using com.pe.vakimu.application.Models.Response;

namespace com.pe.vakimu.application.Services
{
    public class DepartamentoService
    {
        private readonly DepartementoRepositoryImpl _departementoRep;

        public DepartamentoService(VakimuDbContext context)
        {
            _departementoRep = new DepartementoRepositoryImpl(context);
        }

        
        // Lista de departamento
        public async Task<IEnumerable<DepartamentoResponse>> ObtenerDepartamentos()
        {
            return await _departementoRep.GetAll();
        }
        
    }
}