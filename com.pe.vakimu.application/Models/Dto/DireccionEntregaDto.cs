using System.ComponentModel.DataAnnotations;


namespace com.pe.vakimu.application.Models.Dto{
    public class DireccionEntregaDto
    {

        public int OrdenId { get; set; }
   
        public int ClienteId { get; set; }

        [Required]
        public string DireccionLinea1 { get; set; } = null;
        public string DireccionLinea2 { get; set; }
        [Required]
        public string Ciudad { get; set; } = null;
        [Required]
        public string Distrito { get; set; } = null;

        public string CodigoPostal { get; set; }

        public string Referencia { get; set; }
    }

    public class DireccionEntregaUpdateDto
    {
        public int? OrdenId { get; set; }
        public int? ClienteId { get; set; }
        public string DireccionLinea1 { get; set; }
        public string DireccionLinea2 { get; set; }
        public string Ciudad { get; set; }
        public string Distrito { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencia { get; set; }
    }
}