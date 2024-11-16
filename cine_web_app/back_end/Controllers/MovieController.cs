using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using cine_web_app.back_end.Models;
using cine_web_app.back_end.Services;

namespace cine_web_app.back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly CineService _cineService;

        // Inyectar CineService en el controlador
        public MovieController(CineService cineService)
        {
            _cineService = cineService;
        }

        // Método para obtener todos los cines con sus películas
        [HttpGet("GetCinesConPeliculas")]
        public IActionResult GetCinesConPeliculas()
        {
            var cines = _cineService.ObtenerCines();
            return Ok(cines);
        }

        // Método para obtener un cine específico con sus películas
        [HttpGet("GetCineConPeliculas")]
        public IActionResult GetCineConPeliculas(int cineId)
        {
            var cine = _cineService.ObtenerCines().FirstOrDefault(c => c.Id == cineId);
            if (cine == null)
            {
                return NotFound("Cine no encontrado");
            }
            return Ok(cine);
        }

        // Método para obtener una película específica en un cine
        [HttpGet("GetPeliculaPorCine")]
        public IActionResult GetPeliculaPorCine(int cineId, int peliculaId)
        {
            var cine = _cineService.ObtenerCines().FirstOrDefault(c => c.Id == cineId);
            if (cine == null)
            {
                return NotFound("Cine no encontrado");
            }

            var pelicula = cine.Peliculas.FirstOrDefault(p => p.Id == peliculaId);
            if (pelicula == null)
            {
                return NotFound("Película no encontrada en este cine");
            }

            return Ok(pelicula);
        }
    }
}
