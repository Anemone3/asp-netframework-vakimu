using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Entities
{
    [Table("orden")]
    public class Orden
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("cliente_id")]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Column("sucursal_id")]
        public int? SucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        [Required, MaxLength(10)]
        [Column("tipo")]
        public string Tipo { get; set; }

        [Required, MaxLength(20)]
        [Column("estado")]
        public string Estado { get; set; } = "pendiente";

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [MaxLength(11)]
        [Column("ruc")]
        public string Ruc { get; set; }

        [Column("metodo_pago_id")]
        public int MetodoPagoId { get; set; }
        public virtual MetodoPago MetodoPago { get; set; }

        public virtual List<DetalleOrden> Detalles { get; set; }
    }

}