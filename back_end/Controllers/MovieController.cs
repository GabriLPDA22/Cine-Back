using Microsoft.AspNetCore.Mvc;
using cine_web_app.back_end.Models;
using CineAPI.Services.Interfaces;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        /// <summary>
        /// Constructor para inyectar el servicio de películas.
        /// </summary>
        /// <param name="movieService">Servicio de películas.</param>
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Obtiene todas las películas.
        /// </summary>
        /// <returns>Lista de películas.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> GetAllMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        /// <summary>
        /// Obtiene una película por su ID.
        /// </summary>
        /// <param name="id">ID de la película.</param>
        /// <returns>Película encontrada.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Movies>> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
                return NotFound("La película no fue encontrada.");

            return Ok(movie);
        }

        /// <summary>
        /// Crea una nueva película.
        /// </summary>
        /// <param name="movie">Película a crear.</param>
        /// <returns>Película creada.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateMovie(Movies movie)
        {
            await _movieService.AddMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.PeliculaID }, movie);
        }

        /// <summary>
        /// Actualiza una película existente.
        /// </summary>
        /// <param name="id">ID de la película a actualizar.</param>
        /// <param name="movie">Datos actualizados de la película.</param>
        /// <returns>No content si se actualizó correctamente.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, Movies movie)
        {
            if (id != movie.PeliculaID)
                return BadRequest("El ID de la película no coincide.");

            await _movieService.UpdateMovieAsync(movie);
            return NoContent();
        }

        /// <summary>
        /// Elimina una película por su ID.
        /// </summary>
        /// <param name="id">ID de la película a eliminar.</param>
        /// <returns>No content si se eliminó correctamente.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
                return NotFound("La película no fue encontrada.");

            await _movieService.DeleteMovieAsync(id);
            return NoContent();
        }
    }
}
