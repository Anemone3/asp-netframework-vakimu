using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Entities
{
    [Table("producto")]
    public class Producto
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required, MaxLength(120)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Required, MaxLength(40)]
        [Column("sku")]
        public string Sku { get; set; }

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("precio_oferta")]
        public decimal? PrecioOferta { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("disponible")]
        public bool Disponible { get; set; } = true;

        [Column("imagen")]
        public string Imagen { get; set; }

        [Column("fechaActualizada")]
        public DateTime FechaActualizada { get; set; }

        public virtual InformacionNutricional InformacionNutricional { get; set; }
    }

}