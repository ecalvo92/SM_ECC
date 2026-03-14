using Microsoft.AspNetCore.Mvc;
using SM_WEB.Filters;
using SM_WEB.Models;
using SM_WEB.Services;
using System.Net;

namespace SM_WEB.Controllers
{
    [ValidarSesion]
    public class SeguridadController(IHttpClientFactory _http, IConfiguration _config, IUtilitario _util) : Controller
    {
        [HttpGet]
        public IActionResult CambiarAcceso()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CambiarAcceso(Seguridad modelo)
        {
            modelo.Consecutivo = HttpContext.Session.GetInt32("Consecutivo")!.Value;
            modelo.NuevaContrasenna = _util.Encrypt(modelo.NuevaContrasenna);
            modelo.ConfirmarContrasenna = _util.Encrypt(modelo.ConfirmarContrasenna);

            using var client = _http.CreateClient();
            var url = _config.GetValue<string>("Valores:UrlAPI") + "Seguridad/CambiarAcceso";
            var result = client.PutAsJsonAsync(url, modelo).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Home");
            }
            else if (result.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception();
            }

            ViewBag.Mensaje = result.Content.ReadAsStringAsync().Result;
            return View();
        }

    }
}
