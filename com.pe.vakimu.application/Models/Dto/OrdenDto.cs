using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Dto{
    public class OrdenDto
    {
        [Required]
        public int ClienteId { get; set; }
        
        public int? SucursalId { get; set; }

        [Required]
        [RegularExpression("^(delivery|retiro)$", ErrorMessage = "Tipo debe ser 'delivery' o 'retiro'")]
        public string Tipo { get; set; } = null;

        [Required]
        [RegularExpression("^(pendiente|pagado|entregado|cancelado)$", ErrorMessage = "Estado inválido")]
        public string Estado { get; set; } = "pendiente";

        public string Descripcion { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "El RUC debe tener 11 dígitos")]
        public string Ruc { get; set; }

        [Required]
        public int MetodoPagoId { get; set; }
    }

    public class OrdenUpdateDto
    {
        public int? ClienteId { get; set; }
        public int? SucursalId { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string Ruc { get; set; }
        public int? MetodoPagoId { get; set; }
    }

    public class OrdenDetalleDto
    {
        public OrdenDto Orden { get; set; }
        
        public DireccionEntregaDto Direccion { get; set; }
        public List<DetalleOrdenDto> Detalles { get; set; }
    }
}