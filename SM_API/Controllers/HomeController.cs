using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SM_API.Models;

namespace SM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost("RegistrarCuenta")]
        public IActionResult RegistrarCuenta(Usuario modelo)
        {
            using (var context = new SqlConnection("Server=localhost\\MSSQLSERVER01;Database=SM_DB;Integrated Security=True;TrustServerCertificate=True;"))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Identificacion", modelo.Identificacion);
                parametros.Add("@Nombre", modelo.Nombre);
                parametros.Add("@CorreoElectronico", modelo.CorreoElectronico);
                parametros.Add("@Contrasenna", modelo.Contrasenna);

                context.Execute("RegistrarCuenta", parametros);
            }

            return Ok();
        }
    }
}
