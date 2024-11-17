using Microsoft.AspNetCore.Mvc;
using cine_web_app.back_end.Services; // Importar el servicio correcto
using System.Linq;

namespace cine_web_app.back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CineController : ControllerBase
    {
        private readonly CineService _cineService;

        // Inyección de dependencia del CineService
        public CineController(CineService cineService)
        {
            _cineService = cineService;
        }

        [HttpGet("GetSeatSelectionInfo")]
        public IActionResult GetSeatSelectionInfo(string cineName, string movieTitle, string sessionDate, string sessionTime)
        {
            // Buscar el cine por nombre
            var cine = _cineService.ObtenerCines().FirstOrDefault(c => c.Nombre == cineName);
            if (cine == null)
            {
                return NotFound("Cine no encontrado");
            }

            // Buscar la película por título
            var pelicula = cine.Peliculas.FirstOrDefault(p => p.Titulo == movieTitle);
            if (pelicula == null)
            {
                return NotFound("Película no encontrada en este cine");
            }

            // Buscar las sesiones para este cine en la fecha dada
            if (!pelicula.Sesiones.TryGetValue(cineName, out var sesionesPorFecha))
            {
                return NotFound("No hay sesiones para este cine");
            }

            // Buscar las sesiones en la fecha específica
            if (!sesionesPorFecha.TryGetValue(sessionDate, out var sesiones))
            {
                return NotFound("No hay sesiones para esta fecha");
            }

            // Buscar la sesión específica por hora
            var sesion = sesiones.FirstOrDefault(s => s.Hora == sessionTime);
            if (sesion == null)
            {
                return NotFound("Sesión no encontrada en este horario");
            }

            // Devolver la información para la selección de asientos
            var seatSelectionInfo = new
            {
                MovieTitle = pelicula.Titulo,
                CineName = cine.Nombre,
                SessionDate = sessionDate,
                SessionTime = sesion.Hora,
                Room = sesion.Sala,
                EsISense = sesion.EsISense,
                EsVOSE = sesion.EsVOSE,
                BannerImage = pelicula.Imagen
            };

            return Ok(seatSelectionInfo);
        }

    }
}
