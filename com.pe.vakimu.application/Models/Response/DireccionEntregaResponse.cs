using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Response
{
    public class DireccionEntregaResponse
    {
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int ClienteId { get; set; }
        public string DireccionLinea1 { get; set; }
        public string DireccionLinea2 { get; set; }
        public string Ciudad { get; set; }
        public string Distrito { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencia { get; set; }
    }
}