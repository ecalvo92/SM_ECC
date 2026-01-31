using Microsoft.AspNetCore.Mvc;
using SM_WEB.Models;

namespace SM_WEB.Controllers
{
    public class HomeController : Controller
    {
        //Registro, Recuperar Contraseña

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario modelo)
        {
            return View();
        }

    }
}
