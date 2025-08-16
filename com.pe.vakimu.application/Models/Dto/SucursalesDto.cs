using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Dto
{
    public class SucursalesDto
    {
        public string Ubicacion { get; set; } = null;
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public int DepartamentoId { get; set; }
        public string Sede { get; set; }
        public string Calle { get; set; }
    }

    public class SucursalesUpdateDto
    {
        public string Ubicacion { get; set; }
        public decimal? Longitud { get; set; }
        public decimal? Latitud { get; set; }
        public int? DepartamentoId { get; set; }
        public string Sede { get; set; }
        public string Calle { get; set; }
    }
}