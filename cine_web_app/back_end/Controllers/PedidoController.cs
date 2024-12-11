using Microsoft.AspNetCore.Mvc;
using cine_web_app.back_end.Models;
using cine_web_app.back_end.Services;

namespace cine_web_app.back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;

        public PedidoController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet("GetPedidos")]
        public IActionResult GetPedidos()
        {
            var pedidos = _pedidoService.ObtenerPedidos();
            return Ok(pedidos);
        }

        [HttpGet("GetPedidoPorId/{id}")]
        public IActionResult GetPedidoPorId(int id)
        {
            var pedido = _pedidoService.ObtenerPedidoPorId(id);
            if (pedido == null)
            {
                return NotFound("Pedido no encontrado");
            }
            return Ok(pedido);
        }

        [HttpPost("CreatePedido")]
        public IActionResult CreatePedido([FromBody] Pedido pedido)
        {
            if (pedido == null)
            {
                return BadRequest("El pedido no puede ser nulo.");
            }

            // Validación de campos obligatorios
            if (string.IsNullOrEmpty(pedido.NombreCliente) ||
                string.IsNullOrEmpty(pedido.TituloPelicula) ||
                string.IsNullOrEmpty(pedido.Cine))
            {
                return BadRequest("Faltan datos obligatorios en el pedido.");
            }

            _pedidoService.AgregarPedido(pedido);

            return Ok(new { Message = "Pedido creado correctamente", PedidoId = pedido.Id });
        }
       [HttpDelete("EliminarPedido")]
        public IActionResult EliminarPedido([FromBody] Pedido pedido)
        {
            return Ok(new { Message = "Pedido borrado correctamente", PedidoId = pedido.Id });
        }

        [HttpGet("GetButacasReservadas")]
        public IActionResult GetButacasReservadas(string cineName, string date, int sesionId)
        {
            try
            {
                var butacasReservadas = _pedidoService.ObtenerButacasReservadas(cineName, date, sesionId);

                if (!butacasReservadas.Any())
                {
                    return NotFound("No se encontraron butacas reservadas para los parámetros proporcionados.");
                }

                return Ok(butacasReservadas);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
