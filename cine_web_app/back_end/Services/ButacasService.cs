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

        public List<Butaca> ObtenerButacas()
        {
            return _butacas;
        }

        public Butaca ObtenerButacaPorDescripcion(string descripcion)
        {
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

            // Marcar como ocupadas todas las butacas disponibles
            foreach (var butaca in butacasReservadas)
            {
                butaca.EstaOcupado = true; // Marca como ocupado
            }

            return true;
        }


        public void InicializarButacas(List<Butaca> butacasIniciales)
        {
            _butacas.Clear();
            _butacas.AddRange(butacasIniciales);
        }

        public void ReestablecerButacas()
        {
            foreach (var butaca in _butacas)
            {
                butaca.EstaOcupado = false; // Reiniciar todas las butacas a desocupado
            }
        }
    }
}
