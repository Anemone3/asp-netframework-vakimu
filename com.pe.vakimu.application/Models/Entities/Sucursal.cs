using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Entities
{
    [Table("sucursales")]
    public class Sucursal
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required, MaxLength(80)]
        [Column("ubicacion")]
        public string Ubicacion { get; set; }

        [Column("longitud")]
        public decimal Longitud { get; set; }

        [Column("latitud")]
        public decimal Latitud { get; set; }

        [Column("departamento_id")]
        [ForeignKey("Departamento")]
        public int DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }

        [Column("sede")]
        public string Sede { get; set; }

        [Column("calle")]
        public string Calle { get; set; }
    }

}