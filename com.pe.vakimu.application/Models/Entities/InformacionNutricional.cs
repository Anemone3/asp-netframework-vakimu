using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Entities
{

    [Table("informacion_nutricional")]
    public class InformacionNutricional
    {
        [Key, ForeignKey("Producto")]
        [Column("id_producto")]
        public int IdProducto { get; set; }  // Aquí es clave primaria y foránea a la vez

        public virtual Producto Producto { get; set; }

        [Column("energia_kcal")]
        public decimal EnergiaKcal { get; set; }

        [Column("grasa_total")]
        public decimal GrasaTotal { get; set; }

        [Column("grasa_saturada")]
        public decimal GrasaSaturada { get; set; }

        [Column("grasa_trans")]
        public decimal GrasaTrans { get; set; }

        [Column("colesterol_mg")]
        public decimal ColesterolMg { get; set; }

        [Column("sodio_mg")]
        public decimal SodioMg { get; set; }

        [Column("carbohidratos_totales")]
        public decimal CarbohidratosTotales { get; set; }

        [Column("fibra_dietaria")]
        public decimal FibraDietaria { get; set; }

        [Column("azucares_totales")]
        public decimal AzucaresTotales { get; set; }

        [Column("azucares_anhadidos")]
        public decimal AzucaresAnhadidos { get; set; }

        [Column("proteina")]
        public decimal Proteina { get; set; }
    }


}