using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.pe.vakimu.application.Models.Dto
{
    public class ClienteDto
    {
        [Required(ErrorMessage = "El Departamento es obligatorio")]
        public int DepartamentoId { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(40, ErrorMessage = "Máximo 40 caracteres")]
        public string Nombre { get; set; } = null;

        [Required(ErrorMessage = "Los Apellidos son obligatorios")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        public string Apellidos { get; set; } = null;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        [StringLength(60)]
        public string Correo { get; set; } = null;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Contrasena { get; set; } = null;

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Direccion { get; set; }

        public string DescripcionCasa { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El teléfono debe tener exactamente 9 dígitos")]
        public string Telefono { get; set; } = null;

        [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe tener 8 dígitos")]
        public string Dni { get; set; }

        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string Distrito { get; set; }
    }

    public class ClienteUpdateDto
    {
        public int? DepartamentoId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public string Direccion { get; set; }
        public string DescripcionCasa { get; set; }
        public string Telefono { get; set; }
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe tener 8 dígitos")]

        public string Dni { get; set; }
        public string Distrito { get; set; }
    }
}