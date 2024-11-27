using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using cine_web_app.back_end.Models;
using cine_web_app.back_end.Services;

namespace cine_web_app.back_end.Controllers
{
    [ApiController]
    [Route("api/butacas")]
    public class ButacaController : ControllerBase
    {
        private readonly ButacaService _butacaService;

        public ButacaController(ButacaService butacaService)
        {
            _butacaService = butacaService;
        }

        [HttpGet("ObtenerButacas")]
        public ActionResult<List<Butaca>> ObtenerButacas()
        {
            var butacas = _butacaService.ObtenerButacas();
            if (butacas == null || !butacas.Any())
            {
                return NotFound(new { mensaje = "No se encontraron butacas." });
            }

            return Ok(butacas);
        }

        [HttpPost("InicializarButacas")]
        public IActionResult InicializarButacas([FromBody] List<Butaca> butacasIniciales)
        {
            if (butacasIniciales == null || !butacasIniciales.Any())
            {
                return BadRequest(new { mensaje = "La lista de butacas no puede estar vacía." });
            }

            _butacaService.InicializarButacas(butacasIniciales);
            return Ok(new { mensaje = "Butacas inicializadas con éxito." });
        }

        [HttpPost("ReservarButacas")]
        public IActionResult ReservarButacas([FromBody] List<string> coordenadasButacas)
        {
            if (coordenadasButacas == null || !coordenadasButacas.Any())
            {
                return BadRequest(new { mensaje = "La lista de coordenadas no puede estar vacía." });
            }

            var resultado = _butacaService.ReservarButacas(coordenadasButacas);
            if (!resultado)
            {
                return BadRequest(new { mensaje = "Alguna de las butacas ya está ocupada o no existe." });
            }

            return Ok(new { mensaje = "Butacas reservadas con éxito." });
        }

        [HttpPost("LiberarButacas")]
        public IActionResult LiberarButacas([FromBody] List<string> coordenadasButacas)
        {
            if (coordenadasButacas == null || !coordenadasButacas.Any())
            {
                return BadRequest(new { mensaje = "La lista de coordenadas no puede estar vacía." });
            }

            var resultado = _butacaService.LiberarButacas(coordenadasButacas);
            if (!resultado)
            {
                return BadRequest(new { mensaje = "Alguna de las butacas no está ocupada o no existe." });
            }

            return Ok(new { mensaje = "Butacas liberadas con éxito." });
        }

        [HttpPost("ReestablecerButacas")]
        public IActionResult ReestablecerButacas()
        {
            _butacaService.ReestablecerButacas();
            return Ok(new { mensaje = "Butacas reestablecidas al estado inicial." });
        }

        [HttpGet("ObtenerButacasVIP")]
        public ActionResult<List<Butaca>> ObtenerButacasVIP()
        {
            var butacasVIP = _butacaService.ObtenerButacas()
                                           .Where(b => b.Categoria == "VIP")
                                           .ToList();

            if (!butacasVIP.Any())
            {
                return NotFound(new { mensaje = "No se encontraron butacas VIP." });
            }

            return Ok(butacasVIP);
        }

        [HttpPost("CalcularSuplemento")]
        public ActionResult CalcularSuplemento([FromBody] List<string> coordenadasButacas)
        {
            if (coordenadasButacas == null || !coordenadasButacas.Any())
            {
                return BadRequest(new { mensaje = "La lista de coordenadas no puede estar vacía." });
            }

            var suplementoTotal = _butacaService.CalcularSuplemento(coordenadasButacas);
            return Ok(new { suplementoTotal });
        }
    }
}
