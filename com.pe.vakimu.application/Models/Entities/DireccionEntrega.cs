using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Entities
{


    [Table("direccion_entrega")]
    public class DireccionEntrega
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("orden_id")]
        public int OrdenId { get; set; }
        public virtual Orden Orden { get; set; }

        [Column("cliente_id")]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Column("direccion_linea_1")]
        public string DireccionLinea1 { get; set; }

        [Column("direccion_linea_2")]
        public string DireccionLinea2 { get; set; }

        [Column("ciudad")]
        public string Ciudad { get; set; }

        [Column("distrito")]
        public string Distrito { get; set; }

        [Column("codigo_postal")]
        public string CodigoPostal { get; set; }

        [Column("referencia")]
        public string Referencia { get; set; }
    }


}