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
            var cine = _cines.FirstOrDefault(c => c.Id == cineId);
            if (cine == null)
                return NotFound("Cine no encontrado");

            var pelicula = cine.Peliculas.FirstOrDefault(p => p.Id == movieId);
            if (pelicula == null)
                return NotFound("Película no encontrada en este cine");

            if (!pelicula.Sesiones.TryGetValue(sessionDate, out var sesiones))
            {
                return NotFound("No hay sesiones para esta fecha");
            }

            var sesion = sesiones.FirstOrDefault(s => s.Hora == sessionTime);
            if (sesion == null)
            {
                return NotFound("Sesión no encontrada en este horario");
            }

            var seatSelectionInfo = new
            {
                MovieTitle = pelicula.Titulo,
                CineName = cine.Nombre,
                SessionDate = sessionDate,
                SessionTime = sesion.Hora,
                Room = sesion.Sala,
                EsISense = sesion.EsISense,
                EsVOSE = sesion.EsVOSE,
                BannerImage = pelicula.Imagen  // Aquí usamos la propiedad correcta: "Imagen"
            };

            return Ok(seatSelectionInfo);
        }
    }
}