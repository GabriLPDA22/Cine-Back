using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

            if (butacas == null || butacas.Count == 0)
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
    }
}
