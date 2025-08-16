

using Newtonsoft.Json;

namespace com.pe.vakimu.application.Models.Response
{
    public class ProductoResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Sku { get; set; }
        
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioOferta { get; set; }
        public bool Disponible { get; set; }
        public string Imagen { get; set; }
    }

    public class ProductoConInfoNutricional : ProductoResponse
    {  
        [JsonProperty(PropertyName = "informacion_nutricional",NullValueHandling = NullValueHandling.Ignore, Order = 99)]
        public InfoNutricion InfoNutricional { get; set; }
    }
}