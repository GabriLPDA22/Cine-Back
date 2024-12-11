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
        // Método para eliminar un pedido por su ID
        public bool EliminarPedido(int id)
        {
            var pedido = _pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido == null)
            {
                return false; // No se encontró el pedido
            }

            _pedidos.Remove(pedido); // Eliminar el pedido de la lista
            return true; // Pedido eliminado
        }

        public List<string> ObtenerButacasReservadas(string cineName, string date, int sesionId)
        {
            // Validar los parámetros
            if (string.IsNullOrEmpty(cineName) || string.IsNullOrEmpty(date) || sesionId <= 0)
                throw new ArgumentException("Los parámetros cineName, date y sesionId son obligatorios.");

            // Filtrar las butacas reservadas basándose en los parámetros
            return _pedidos
                .Where(p => p.Cine == cineName && p.Fecha == date && p.SesionId == sesionId)
                .SelectMany(p => p.ButacasReservadas)
                .Distinct() // Eliminar duplicados, en caso de que existan
                .ToList();
        }
    }
}
