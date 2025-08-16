using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Entities
{
    [Table("cliente")] 
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Column("departamento_id")]
        [ForeignKey("Departamento")]
        public int? DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }

        [Required, MaxLength(40)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required, MaxLength(60)]
        [Column("apellidos")]
        public string Apellidos { get; set; }

        [Required, MaxLength(60)]
        [Column("correo")]
        public string Correo { get; set; }

        [MaxLength(40)]
        [Column("contrasena")]
        public string Contrasena { get; set; }

        [MaxLength(100)]
        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("descripcion_casa")]
        public string DescripcionCasa { get; set; }

        [MaxLength(20)]
        [Column("telefono")]
        public string Telefono { get; set; }

        [MaxLength(10)]
        [Column("dni")]
        public string Dni { get; set; }

        [MaxLength(20)]
        [Column("distrito")]
        public string Distrito { get; set; }
    }



}