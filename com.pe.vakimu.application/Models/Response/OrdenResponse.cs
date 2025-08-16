using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Response
{
    public class OrdenResponse
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int? SucursalId { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string Ruc { get; set; }
        public int? MetodoPagoId { get; set; }
    }

    public class OrdenDetalleConCliente
    {
        public int Id { get; set; }
        public ClienteResponse Cliente { get; set; }
        public SucursalResponse Sucursal { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string Ruc { get; set; }
        public int? MetodoPagoId { get; set; }
    }

    public class OrdenConDetalleResponse
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int? SucursalId { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string Ruc { get; set; }
        public int? MetodoPagoId { get; set; }
        public List<DetalleOrdenResponse> DetalleOrdenes { get; set; } = new List<DetalleOrdenResponse>();
    }
}