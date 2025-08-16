using System.Collections.Generic;

namespace com.pe.vakimu.application.Models.Response
{
    public class CheckoutView
    {
        public List<DepartamentoResponse> Departamentos { get; set; }
        public List<SucursalResponse> Sucursales { get; set; }
        public List<MetodoPagoResponse> MetodosPago { get; set; }
    }
}