using System.ComponentModel.DataAnnotations;

namespace SM_API.Models
{
    public class RegistroUsuarioRequest
    {
        [Required]
        public string Identificacion { get; set; } = string.Empty;
        [Required]
        public string Contrasenna { get; set; } = string.Empty;
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
