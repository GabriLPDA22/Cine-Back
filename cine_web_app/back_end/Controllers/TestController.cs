using Microsoft.AspNetCore.Mvc;

namespace cine_web_app.back_end.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("El servidor está funcionando correctamente.");
        }
    }
}