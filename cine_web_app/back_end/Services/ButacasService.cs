using System.Collections.Generic;
using System.Linq;
using cine_web_app.back_end.Models;

namespace cine_web_app.back_end.Services
{
    public class ButacaService
    {
        private readonly List<Butaca> _butacas;

        public ButacaService()
        {
            // Lista vacía al inicio, será llenada por el front-end
            _butacas = new List<Butaca>();
        }

        /// <summary>
        /// Obtener todas las butacas.
        /// </summary>
        public List<Butaca> ObtenerButacas()
        {
            return _butacas;
        }

        /// <summary>
        /// Obtener una butaca por su descripción.
        /// </summary>
        /// <param name="descripcion">Descripción de la butaca.</param>
        public Butaca ObtenerButacaPorDescripcion(string descripcion)
        {
            return _butacas.FirstOrDefault(b => b.Descripcion == descripcion);
        }

        /// <summary>
        /// Reservar una lista de butacas.
        /// </summary>
        /// <param name="coordenadas">Lista de coordenadas de las butacas a reservar.</param>
        public bool ReservarButacas(List<string> coordenadas)
        {
            var butacasReservadas = new List<Butaca>();

            foreach (var coord in coordenadas)
            {
                var butaca = ObtenerButacaPorDescripcion(coord);
                if (butaca == null || butaca.EstaOcupado)
                {
                    return false; // Una butaca no está disponible
                }

                butacasReservadas.Add(butaca);
            }

            // Marcar como ocupadas todas las butacas disponibles
            foreach (var butaca in butacasReservadas)
            {
                butaca.EstaOcupado = true;
            }

            return true;
        }

        /// <summary>
        /// Inicializar la lista de butacas.
        /// </summary>
        /// <param name="butacasIniciales">Lista de butacas iniciales.</param>
        public void InicializarButacas(List<Butaca> butacasIniciales)
        {
            // Validar que las categorías estén bien definidas
            foreach (var butaca in butacasIniciales)
            {
                if (string.IsNullOrEmpty(butaca.Categoria))
                {
                    butaca.Categoria = "Estandar"; // Categoría por defecto
                }

                if (butaca.Categoria == "VIP" && butaca.Suplemento == 0)
                {
                    butaca.Suplemento = 5; // Suplemento por defecto para VIP
                }
            }

            _butacas.Clear();
            _butacas.AddRange(butacasIniciales);
        }

        /// <summary>
        /// Reestablecer todas las butacas a su estado inicial.
        /// </summary>
        public void ReestablecerButacas()
        {
            foreach (var butaca in _butacas)
            {
                butaca.EstaOcupado = false; // Reiniciar todas las butacas a desocupado
            }
        }

        /// <summary>
        /// Calcular el suplemento total de una lista de butacas.
        /// </summary>
        /// <param name="coordenadas">Lista de coordenadas de las butacas.</param>
        /// <returns>El suplemento total.</returns>
        public decimal CalcularSuplemento(List<string> coordenadas)
        {
            return _butacas
                .Where(b => coordenadas.Contains(b.Descripcion))
                .Sum(b => b.Suplemento);
        }
    }
}
