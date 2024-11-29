using System.Collections.Generic;
using System.Linq;
using cine_web_app.back_end.Models;

namespace cine_web_app.back_end.Services
{
    public class PedidoService
    {
        private readonly List<Pedido> _pedidos = new List<Pedido>(); // Lista en memoria para pedidos

        public List<Pedido> ObtenerPedidos()
        {
            return _pedidos; // Devuelve todos los pedidos
        }

        public Pedido ObtenerPedidoPorId(int id)
        {
            return _pedidos.FirstOrDefault(p => p.Id == id); // Busca un pedido por ID
        }

        public void AgregarPedido(Pedido pedido)
        {
            // Validar los datos del pedido
            if (pedido == null)
                throw new ArgumentNullException(nameof(pedido), "El pedido no puede ser nulo.");

            if (string.IsNullOrEmpty(pedido.NombreCliente) ||
                string.IsNullOrEmpty(pedido.TituloPelicula) ||
                string.IsNullOrEmpty(pedido.Cine))
                throw new ArgumentException("Faltan datos obligatorios en el pedido.");

            // Asignar un ID único al pedido
            pedido.Id = _pedidos.Any() ? _pedidos.Max(p => p.Id) + 1 : 1;

            // Agregar el pedido a la lista
            _pedidos.Add(pedido);
        }
    }
}
