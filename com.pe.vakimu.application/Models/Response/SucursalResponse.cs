

namespace com.pe.vakimu.application.Models.Response
{
    public class SucursalResponse
    {
        public int Id { get; set; }
        public string Ubicacion { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public int DepartamentoId { get; set; }
        public string Sede { get; set; }
        public string Calle { get; set; }
    }
}