using Microsoft.AspNetCore.Mvc;

namespace SM_WEB.Controllers
{
    public class HomeController : Controller
    {
        //Registro, Recuperar Contraseña

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

    }
}
