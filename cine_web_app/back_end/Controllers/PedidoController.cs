using Microsoft.AspNetCore.Mvc;
using cine_web_app.back_end.Models;
using cine_web_app.back_end.Services;
using System;
using System.Collections.Generic;

namespace cine_web_app.back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;

        // Inyectar PedidoService en el controlador
        public PedidoController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // Endpoint para obtener todos los pedidos
        [HttpGet("GetPedidos")]
        public IActionResult GetPedidos()
        {
            var pedidos = _pedidoService.ObtenerPedidos();
            return Ok(pedidos);
        }

        // Endpoint para obtener un pedido por Id
        [HttpGet("GetPedidoPorId")]
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

            // Agregar el pedido al servicio
            _pedidoService.AgregarPedido(pedido);

            // Retornar el ID del pedido recién creado
            return Ok(new { Message = "Pedido creado correctamente", PedidoId = pedido.Id });
        }

    }
}
