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
            // Inicialización de una lista vacía de butacas
            _butacas = new List<Butaca>();
        }

        public List<Butaca> ObtenerButacas()
        {
            return _butacas;
        }

        public Butaca ObtenerButacaPorDescripcion(string descripcion)
        {
            // Buscar una butaca por su descripción (coordenadas como string)
            return _butacas.FirstOrDefault(b => b.Descripcion == descripcion);
        }

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

            // Marcar todas como ocupadas si todas están disponibles
            foreach (var butaca in butacasReservadas)
            {
                butaca.EstaOcupado = true;
            }

            return true;
        }

        // Método para inicializar las butacas
        public void InicializarButacas(List<Butaca> butacasIniciales)
        {
            _butacas.Clear();
            _butacas.AddRange(butacasIniciales);
        }
    }
}
