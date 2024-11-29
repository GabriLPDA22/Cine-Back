using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using cine_web_app.back_end.Services;

namespace cine_web_app.back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservasController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet("ButacasReservadas")]
        public async Task<IActionResult> GetButacasReservadas()
        {
            string url = "http://localhost:5006/api/Reserva/GetReservas"; // Cambiar la URL si es necesario
            var butacasArray = await _reservaService.ObtenerButacasReservadasAsync(url);
            return Ok(butacasArray);
        }
    }
}
