using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Dto
{
    public class DetalleOrdenDto
    {
        public int OrdenId { get; set; }
        [Required]
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        
        public int Cantidad { get; set; }
        public decimal Descuento { get; set; }
        [Required]
        public decimal Subtotal { get; set; }
        [Required]
        public decimal Total { get; set; }
    }

    public class DetalleOrdenUpdateDto
    {
        public int? OrdenId { get; set; }
        public int? ProductoId { get; set; }
        public string Descripcion { get; set; }
        
        public int? Cantidad { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }
    }
}