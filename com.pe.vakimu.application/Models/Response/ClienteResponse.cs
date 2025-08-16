
namespace com.pe.vakimu.application.Models.Response
{
    public class ClienteResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        public string Correo { get; set; }
        public DepartamentoResponse Departamento { get; set; }

        public string Direccion { get; set; }
        public string DescripcionCasa { get; set; }
        public string Telefono { get; set; }
        public string Dni { get; set; }
        public string Distrito { get; set; }
    }
}