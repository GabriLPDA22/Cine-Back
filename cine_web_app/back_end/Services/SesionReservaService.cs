namespace cine_web_app.back_end.Services
{
    public class SesionReservaService
    {
        // Mapeo de SesionId a una lista de butacas reservadas
        private readonly Dictionary<string, List<string>> _reservasPorSesion;

        public SesionReservaService()
        {
            _reservasPorSesion = new Dictionary<string, List<string>>();
        }

        // Reservar butacas para una sesión
        public bool ReservarButacas(string sesionId, List<string> butacas)
        {
            if (_reservasPorSesion.ContainsKey(sesionId))
            {
                // Si ya tiene reservas, verificar si las butacas están disponibles
                var butacasReservadas = _reservasPorSesion[sesionId];
                foreach (var butaca in butacas)
                {
                    if (butacasReservadas.Contains(butaca))
                    {
                        return false; // Una de las butacas ya está reservada
                    }
                }
                butacasReservadas.AddRange(butacas); // Añadir nuevas butacas reservadas
            }
            else
            {
                // Si no tiene reservas previas, agregar la sesión con las butacas
                _reservasPorSesion[sesionId] = new List<string>(butacas);
            }
            return true;
        }

        // Obtener las butacas reservadas para una sesión
        public List<string> ObtenerButacasReservadas(string sesionId)
        {
            return _reservasPorSesion.ContainsKey(sesionId) ? _reservasPorSesion[sesionId] : new List<string>();
        }

        // Liberar butacas para una sesión
        public bool LiberarButacas(string sesionId, List<string> butacas)
        {
            if (_reservasPorSesion.ContainsKey(sesionId))
            {
                var butacasReservadas = _reservasPorSesion[sesionId];
                foreach (var butaca in butacas)
                {
                    if (!butacasReservadas.Contains(butaca))
                    {
                        return false; // La butaca no está reservada en esta sesión
                    }
                    butacasReservadas.Remove(butaca); // Liberar la butaca
                }
                return true;
            }
            return false;
        }
    }
}
