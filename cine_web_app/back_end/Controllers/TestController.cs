using Microsoft.AspNetCore.Mvc;

namespace El_Butacazo_Back.Controllers
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