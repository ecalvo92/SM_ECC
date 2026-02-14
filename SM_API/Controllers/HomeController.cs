using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SM_API.Models;

namespace SM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController(IConfiguration _config) : ControllerBase
    {
        [HttpPost("RegistrarCuenta")]
        public IActionResult RegistrarCuenta(Usuario modelo)
        {
            using (var context = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection")))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Identificacion", modelo.Identificacion);
                parametros.Add("@Nombre", modelo.Nombre);
                parametros.Add("@CorreoElectronico", modelo.CorreoElectronico);
                parametros.Add("@Contrasenna", modelo.Contrasenna);

                var result = context.Execute("RegistrarCuenta", parametros);

                if(result <= 0)
                    return BadRequest("Su información no se registró correctamente");

                return Ok("Su información se registró correctamente");
            }
        }
    }
}
