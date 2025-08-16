using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Entities
{
    [Table("detalle_orden")]
    public class DetalleOrden
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("orden_id")]
        public int OrdenId { get; set; }
        public virtual Orden Orden { get; set; }

        [Column("producto_id")]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("descuento")]
        public decimal Descuento { get; set; }

        [Column("subtotal")]
        public decimal Subtotal { get; set; }

        [Column("total")]
        public decimal Total { get; set; }
    }


}