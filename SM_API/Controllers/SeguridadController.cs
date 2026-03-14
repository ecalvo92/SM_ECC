using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SM_API.Models;

namespace SM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController(IConfiguration _config) : ControllerBase
    {

        [HttpPut("CambiarAcceso")]
        public IActionResult CambiarAcceso(SeguridadRequest modelo)
        {
            using var context = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
            var parametros = new DynamicParameters();
            parametros.Add("@Contrasenna", modelo.NuevaContrasenna);
            parametros.Add("@Consecutivo", modelo.Consecutivo);

            var result = context.Execute("ActualizarContrasenna", parametros);

            if (result <= 0)
                return BadRequest("Su información no se actualizó correctamente");

            return Ok("Su información se actualizó correctamente");
        }
    }
}
