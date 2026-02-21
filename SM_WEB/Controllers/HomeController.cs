using Microsoft.AspNetCore.Mvc;
using SM_WEB.Models;
using System.Net;

namespace SM_WEB.Controllers
{
    public class HomeController(IHttpClientFactory _http, IConfiguration _config) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region Iniciar Sesión

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario modelo)
        {
            using var client = _http.CreateClient();
            var url = _config.GetValue<string>("Valores:UrlAPI") + "Home/IniciarSesion";
            var result = client.PostAsJsonAsync(url, modelo).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (result.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception();
            }

            ViewBag.Mensaje = result.Content.ReadAsStringAsync().Result;
            return View();
        }

        #endregion

        #region Registrar Cuenta

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(Usuario modelo)
        {
            using var client = _http.CreateClient();
            var url = _config.GetValue<string>("Valores:UrlAPI") + "Home/RegistrarCuenta";
            var result = client.PostAsJsonAsync(url, modelo).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (result.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception();
            }

            ViewBag.Mensaje = result.Content.ReadAsStringAsync().Result;
            return View();
        }

        #endregion

        #region Recuperar Acceso

        [HttpGet]
        public IActionResult RecuperarAcceso()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarAcceso(Usuario modelo)
        {
            return View();
        }

        #endregion

    }
}
