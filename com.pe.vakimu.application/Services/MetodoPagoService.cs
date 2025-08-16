using System.Collections.Generic;
using System.Threading.Tasks;
using com.pe.vakimu.application.Models.Context;
using com.pe.vakimu.application.Models.Repositories;
using com.pe.vakimu.application.Models.Response;

namespace com.pe.vakimu.application.Services
{
    public class MetodoPagoService
    {
        private readonly MetodoPagoRepositoryImpl _metodoPagoRep;

        public MetodoPagoService(VakimuDbContext context)
        {
            _metodoPagoRep = new MetodoPagoRepositoryImpl(context);
        }

        public async Task<IEnumerable<MetodoPagoResponse>> ObtenerMetodosPago()
        {
            var payments = await _metodoPagoRep.GetAll();
            
            return payments;
        }

        public async Task<MetodoPagoResponse> ObtenerMetodoPagoPorId(int id)
        {
            var payment = await _metodoPagoRep.GetById(id);

            return payment;
        }
    }
}