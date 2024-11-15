using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using cine_web_app.back_end.Models;

namespace cine_web_app.back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CineController : ControllerBase
    {
        private readonly List<Cine> _cines;

        // Constructor que recibe la lista de cines como parámetro
        public CineController(List<Cine> cines)
        {
            _cines = cines;
        }

        // Endpoint para obtener información de la selección de asientos
        [HttpGet("GetSeatSelectionInfo")]
        public IActionResult GetSeatSelectionInfo(int cineId, int movieId, string sessionDate, string sessionTime)
        {
            // Buscar el cine en la lista en memoria
            var cine = _cines.FirstOrDefault(c => c.Id == cineId);
            if (cine == null)
                return NotFound("Cine no encontrado");

            // Buscar la película en la lista de películas del cine
            var pelicula = cine.Peliculas.FirstOrDefault(p => p.Id == movieId);
            if (pelicula == null)
                return NotFound("Película no encontrada en este cine");

            // Buscar la sesión específica por fecha y hora
            if (!pelicula.Sesiones.TryGetValue(sessionDate, out var sesiones))
            {
                return NotFound("No hay sesiones para esta fecha");
            }

            var sesion = sesiones.FirstOrDefault(s => s.Hora == sessionTime);
            if (sesion == null)
            {
                return NotFound("Sesión no encontrada en este horario");
            }

            // Preparar el objeto de respuesta
           var seatSelectionInfo = new
            {
            MovieTitle = pelicula.Titulo,
            CineName = cine.Nombre,
            SessionDate = sessionDate,
            SessionTime = sesion.Hora,
            Room = sesion.Sala,
            EsISense = sesion.EsISense,
            EsVOSE = sesion.EsVOSE,
            BannerImage = pelicula.Imagen // Asegúrate de que `Imagen` sea la propiedad del banner en la clase `Pelicula`
            };

            return Ok(seatSelectionInfo);
        }
    }
}