using System.ComponentModel.DataAnnotations;


namespace com.pe.vakimu.application.Models.Dto{
    public class InformacionNutricionalDto
    {
        public int IdProducto { get; set; }
        public decimal? EnergiaKcal { get; set; }
        public decimal? GrasaTotal { get; set; }
        public decimal? GrasaSaturada { get; set; }
        public decimal? GrasaTrans { get; set; }
        public decimal? ColesterolMg { get; set; }
        public decimal? SodioMg { get; set; }
        public decimal? CarbohidratosTotales { get; set; }
        public decimal? FibraDietaria { get; set; }
        public decimal? AzucaresTotales { get; set; }
        public decimal? AzucaresAnhadidos { get; set; }
        public decimal? Proteina { get; set; }
    }

    public class InformacionNutricionalUpdateDto
    {
        public int? IdProducto { get; set; }
        public decimal? EnergiaKcal { get; set; }
        public decimal? GrasaTotal { get; set; }
        public decimal? GrasaSaturada { get; set; }
        public decimal? GrasaTrans { get; set; }
        public decimal? ColesterolMg { get; set; }
        public decimal? SodioMg { get; set; }
        public decimal? CarbohidratosTotales { get; set; }
        public decimal? FibraDietaria { get; set; }
        public decimal? AzucaresTotales { get; set; }
        public decimal? AzucaresAnhadidos { get; set; }
        public decimal? Proteina { get; set; }
    }

}