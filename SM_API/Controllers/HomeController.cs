using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using SM_API.Models;
using SM_API.Services;

namespace SM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController(IConfiguration _config, IUtilitario _util) : ControllerBase
    {
        [HttpPost("RegistrarCuenta")]
        public IActionResult RegistrarCuenta(RegistroUsuarioRequest modelo)
        {
            using var context = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
            var parametros = new DynamicParameters();
            parametros.Add("@Identificacion", modelo.Identificacion);
            parametros.Add("@Nombre", modelo.Nombre);
            parametros.Add("@CorreoElectronico", modelo.CorreoElectronico);
            parametros.Add("@Contrasenna", modelo.Contrasenna);

            var result = context.Execute("RegistrarCuenta", parametros);

            if (result <= 0)
                return BadRequest("Su información no se registró correctamente");

            return Ok("Su información se registró correctamente");
        }

        [HttpPost("IniciarSesion")]
        public IActionResult IniciarSesion(IniciarSesionRequest modelo)
        {
            using var context = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));
            var parametros = new DynamicParameters();
            parametros.Add("@CorreoElectronico", modelo.CorreoElectronico);
            parametros.Add("@Contrasenna", modelo.Contrasenna);

            var result = context.QueryFirstOrDefault<UsuarioResponse>("IniciarSesion", parametros);

            if (result == null)
                return BadRequest("Su información no se autenticó correctamente");

            return Ok(result);
        }

        [HttpPut("RecuperarAcceso")]
        public IActionResult RecuperarAcceso(RecuperarAccesoRequest modelo)
        {
            using var context = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));

            //validar que el correo electronico exista en la base de datos
            var parametros = new DynamicParameters();
            parametros.Add("@CorreoElectronico", modelo.CorreoElectronico);
            var result = context.QueryFirstOrDefault<UsuarioResponse>("ValidarCorreo", parametros);

            if (result == null)
                return BadRequest("Su información no se validó correctamente");

            //generar una nueva contraseña
            var nuevaContrasenna = GenerarContrasenna();

            //actualizar la contraseña en la base de datos
            var parametrosActualizacion = new DynamicParameters();
            parametrosActualizacion.Add("@Contrasenna", _util.Encrypt(nuevaContrasenna));
            parametrosActualizacion.Add("@Consecutivo", result.Consecutivo);
            var resultActualizacion = context.Execute("ActualizarContrasenna", parametrosActualizacion);

            if (resultActualizacion <= 0)
                return BadRequest("Su información no se actualizó correctamente");

            //enviar un correo electronico con la nueva contraseña
            var contenido = CargarPlantilla("RecuperarAcceso.html")
                .Replace("{{Nombre}}", result.Nombre)
                .Replace("{{Contrasenna}}", nuevaContrasenna);

            _util.EnviarCorreo(modelo.CorreoElectronico, "Recuperación de acceso", contenido);
            return Ok(result);
        }

        private static string GenerarContrasenna()
        {
            const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var r = new Random();
            return new string([.. Enumerable.Range(0, 8).Select(x => letras[r.Next(letras.Length)])]);
        }

        private static string CargarPlantilla(string nombreArchivo)
        {
            var ruta = Path.Combine(AppContext.BaseDirectory, "Templates", nombreArchivo);
            return System.IO.File.ReadAllText(ruta);
        }

    }
}
