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
            Console.WriteLine($"Parámetros recibidos: cineName={cineName}, movieTitle={movieTitle}, sessionDate={sessionDate}, sessionTime={sessionTime}");

            // Buscar el cine por nombre
            var cine = _cineService.ObtenerCines().FirstOrDefault(c => c.Nombre == cineName);
            if (cine == null)
            {
                Console.WriteLine("Cine no encontrado");
                return NotFound("Cine no encontrado");
            }

            // Buscar la película por título
            var pelicula = cine.Peliculas.FirstOrDefault(p => p.Titulo == movieTitle);
            if (pelicula == null)
            {
                Console.WriteLine("Película no encontrada");
                return NotFound("Película no encontrada en este cine");
            }

            // Buscar las sesiones para este cine en la fecha dada
            if (!pelicula.Sesiones.TryGetValue(cineName, out var sesionesPorFecha))
            {
                Console.WriteLine("No hay sesiones para este cine");
                return NotFound("No hay sesiones para este cine");
            }

            // Buscar las sesiones en la fecha específica
            if (!sesionesPorFecha.TryGetValue(sessionDate, out var sesiones))
            {
                Console.WriteLine("No hay sesiones para esta fecha");
                return NotFound("No hay sesiones para esta fecha");
            }

            // Buscar la sesión específica por hora
            var sesion = sesiones.FirstOrDefault(s => s.Hora == sessionTime);
            if (sesion == null)
            {
                Console.WriteLine("Sesión no encontrada");
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

            Console.WriteLine("Información de la sesión encontrada:", seatSelectionInfo);

            return Ok(seatSelectionInfo);
        }

    }
}
