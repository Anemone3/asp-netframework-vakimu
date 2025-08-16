namespace com.pe.vakimu.application.Models.Dto
{
    public class CarritoRequest
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
    public class CheckoutRequest
    {
        public ClienteDto Cliente { get; set; }
        public OrdenDetalleDto OrdenDetalle { get; set; }
    }

}