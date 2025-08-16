using System.Collections.Generic;
using System.Threading.Tasks;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Repositories;
using com.pe.vakimu.application.Models.Response;

namespace com.pe.vakimu.application.Services
{
    public class SucursalService
    {
        private readonly SucursalesRepositoryImpl _sucursalesRepository;

        public SucursalService(VakimuDbContext context)
        {
            _sucursalesRepository = new SucursalesRepositoryImpl(context);
        }

        public async Task<IEnumerable<SucursalResponse>> ObtenerSucursales()
        {
            var sucursales = await _sucursalesRepository.GetAll();
            
            return sucursales;
        }
    }
}