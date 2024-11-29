using System;
using System.Collections.Generic;
using System.Linq;
using cine_web_app.back_end.Models;

namespace cine_web_app.back_end.Services
{
    public class PedidoService
    {
        private readonly ButacaService _butacaService;  // Inyectamos el servicio de Butaca

        // Constructor de PedidoService con ButacaService inyectado
        public PedidoService(ButacaService butacaService)
        {
            _butacaService = butacaService;  // Asignamos la instancia de ButacaService
        }

        // Método para obtener todos los pedidos
        public List<Pedido> ObtenerPedidos()
        {
            // Aquí se debe implementar la lógica real para obtener los pedidos
            // Este es solo un ejemplo simple
            return new List<Pedido>();
        }

        // Método para agregar un nuevo pedido
        public void AgregarPedido(Pedido pedido)
        {
            // Validación básica de campos del pedido
            if (pedido == null)
            {
                throw new ArgumentNullException(nameof(pedido), "El pedido no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(pedido.NombreCliente) ||
                string.IsNullOrEmpty(pedido.TituloPelicula) ||
                string.IsNullOrEmpty(pedido.Cine))
            {
                throw new ArgumentException("Faltan datos obligatorios en el pedido.");
            }

            // Lógica para reservar las butacas del pedido
            bool butacasReservadasExitosamente = _butacaService.ReservarButacas(pedido.ButacasReservadas);

            if (!butacasReservadasExitosamente)
            {
                throw new Exception("No se pudieron reservar las butacas.");
            }

            // Aquí puedes agregar el pedido a tu base de datos o lista interna
            // Ejemplo simple, agregar el pedido a una lista (deberías reemplazarlo con una base de datos real)
            pedido.Id = new Random().Next(1, 1000);  // Asignamos un ID aleatorio (esto se debe cambiar por una asignación de ID real)
            // Agregar el pedido a la lista interna (esto también debería ser una base de datos real)
            _pedidos.Add(pedido);  // _pedidos sería una lista de pedidos internos

            // Si deseas realizar algún otro procesamiento adicional, como calcular el total o añadir entradas y productos, puedes hacerlo aquí
        }

        // Método para obtener un pedido por ID
        public Pedido ObtenerPedidoPorId(int id)
        {
            // Aquí deberías implementar la lógica para obtener un pedido desde una base de datos o lista interna
            return _pedidos.FirstOrDefault(p => p.Id == id);  // Esto busca el pedido por ID en una lista interna (_pedidos)
        }

        // Aquí puedes añadir otros métodos, como eliminar un pedido o actualizarlo

        // Lista interna de pedidos (esto debería ser reemplazado por una base de datos en un entorno real)
        private List<Pedido> _pedidos = new List<Pedido>();
    }
}
