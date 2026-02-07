using Microsoft.AspNetCore.Mvc;
using SM_API.Models;

namespace SM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost("RegistrarCuenta")]
        public IActionResult RegistrarCuenta(Usuario modelo)
        {
            /* Cálculos, Reglas, Validaciones, Llamados a la Base de Datos */

            return Ok();
        }
    }
}
