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

        /// <summary>
        /// Obtener todas las butacas.
        /// </summary>
        /// <returns>Lista de butacas.</returns>
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

        /// <summary>
        /// Inicializar las butacas desde el front-end.
        /// </summary>
        /// <param name="butacasIniciales">Lista de butacas iniciales.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("InicializarButacas")]
        public IActionResult InicializarButacas([FromBody] List<Butaca> butacasIniciales)
        {
            if (butacasIniciales == null || !butacasIniciales.Any())
            {
                return BadRequest(new { mensaje = "La lista de butacas no puede estar vacía." });
            }

            foreach (var butaca in butacasIniciales)
            {
                if (string.IsNullOrEmpty(butaca.Categoria))
                {
                    return BadRequest(new { mensaje = "Cada butaca debe tener una categoría definida." });
                }
            }

            _butacaService.InicializarButacas(butacasIniciales);

            return Ok(new { mensaje = "Butacas inicializadas con éxito." });
        }

        /// <summary>
        /// Reservar una lista de butacas.
        /// </summary>
        /// <param name="coordenadasButacas">Lista de coordenadas de las butacas a reservar.</param>
        /// <returns>Resultado de la operación.</returns>
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

        /// <summary>
        /// Reestablecer todas las butacas a su estado inicial.
        /// </summary>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("ReestablecerButacas")]
        public IActionResult ReestablecerButacas()
        {
            _butacaService.ReestablecerButacas();
            return Ok(new { mensaje = "Butacas reestablecidas al estado inicial." });
        }

        /// <summary>
        /// Obtener butacas VIP.
        /// </summary>
        /// <returns>Lista de butacas VIP.</returns>
        [HttpGet("ObtenerButacasVIP")]
        public ActionResult<List<Butaca>> ObtenerButacasVIP()
        {
            var butacasVIP = _butacaService.ObtenerButacas().Where(b => b.Categoria == "VIP").ToList();

            if (!butacasVIP.Any())
            {
                return NotFound(new { mensaje = "No se encontraron butacas VIP." });
            }

            return Ok(butacasVIP);
        }

        /// <summary>
        /// Obtener el suplemento total de las butacas seleccionadas.
        /// </summary>
        /// <param name="coordenadasButacas">Lista de coordenadas de las butacas.</param>
        /// <returns>Suplemento total.</returns>
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
