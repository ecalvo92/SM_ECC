using Microsoft.AspNetCore.Mvc;
using SM_WEB.Filters;

namespace SM_WEB.Controllers
{
    [ValidarSesion]
    public class SeguridadController : Controller
    {
        [HttpGet]
        public IActionResult CambiarAcceso()
        {
            return View();
        }
    }
}
