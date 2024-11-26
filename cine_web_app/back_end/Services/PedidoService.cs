using System.Collections.Generic;
using System.Linq;
using cine_web_app.back_end.Models;

namespace cine_web_app.back_end.Services
{
    public class PedidoService
    {
        private readonly List<Pedido> _pedidos;

        public PedidoService()
        {
            _pedidos = new List<Pedido>();
        }

        // Método para obtener todos los pedidos
        public List<Pedido> ObtenerPedidos()
        {
            return _pedidos;
        }

        // Método para agregar un nuevo pedido
        public void AgregarPedido(Pedido pedido)
        {
            // Asignar un Id único al pedido
            pedido.Id = _pedidos.Count > 0 ? _pedidos.Max(p => p.Id) + 1 : 1;
            _pedidos.Add(pedido);
        }

        // Método para obtener un pedido por Id
        public Pedido ObtenerPedidoPorId(int id)
        {
            return _pedidos.FirstOrDefault(p => p.Id == id);
        }
    }
}
