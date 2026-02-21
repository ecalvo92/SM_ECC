using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpPost("CapturarError")]
        public IActionResult CapturarError()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return StatusCode(500, "Se presentó un error en el servicio. Por favor intenta nuevamente más tarde.");
        }
    }
}
