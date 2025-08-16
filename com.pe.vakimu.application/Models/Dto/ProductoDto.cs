

using System.ComponentModel.DataAnnotations;

namespace com.pe.vakimu.application.Models.Dto
{
    public class ProductoDto
    {
        [Required]
        public string Nombre { get; set; } = null;
        public string Descripcion { get; set; }
        [Required]
        public string Sku { get; set; } = null;
        [Required]
        public decimal Precio { get; set; }
        
        [Required]
        public int Stock { get; set; }
        public decimal? PrecioOferta { get; set; }
        public bool Disponible { get; set; } = true;
        public string Imagen { get; set; }
    }

    public class ProductoUpdateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Sku { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        public decimal? PrecioOferta { get; set; }
        public bool? Disponible { get; set; }
        public string Imagen { get; set; }
    }
    
    public class ProductoCompletoDto
    {
        public ProductoDto Producto { get; set; }
        public InformacionNutricionalDto InformacionNutricional { get; set; }
    }

}