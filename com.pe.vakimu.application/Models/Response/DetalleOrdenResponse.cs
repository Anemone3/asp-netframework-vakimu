

namespace com.pe.vakimu.application.Models.Response
{
    public class DetalleOrdenResponse
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}