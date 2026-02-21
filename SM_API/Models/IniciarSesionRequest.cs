using System.ComponentModel.DataAnnotations;

namespace SM_API.Models
{
    public class IniciarSesionRequest
    {
        [Required]
        public string Contrasenna { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
