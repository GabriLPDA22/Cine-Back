
using CineAPI.Repositories.Interfaces;
using CineAPI.Services.Interfaces;


namespace CineAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public async Task AddMovieAsync(Movies movie)
        {
            // Validaciones o lógica adicional si es necesario
            ValidateMovie(movie);

            await _movieRepository.AddAsync(movie);
        }

        public async Task DeleteMovieAsync(int peliculaID)
        {
            var movie = await _movieRepository.GetByIdAsync(peliculaID);
            if (movie == null)
            {
                throw new KeyNotFoundException($"No se encontró la película con ID {peliculaID}");
            }

            await _movieRepository.DeleteAsync(peliculaID);
        }

        public async Task<IEnumerable<Movies>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllAsync();
        }

        public async Task<Movies?> GetMovieByIdAsync(int peliculaID)
        {
            var movie = await _movieRepository.GetByIdAsync(peliculaID);
            if (movie == null)
            {
                throw new KeyNotFoundException($"No se encontró la película con ID {peliculaID}");
            }

            return movie;
        }

        public async Task UpdateMovieAsync(Movies movie)
        {
            var existingMovie = await _movieRepository.GetByIdAsync(movie.PeliculaID);
            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"No se encontró la película con ID {movie.PeliculaID}");
            }

            ValidateMovie(movie);

            await _movieRepository.UpdateAsync(movie);
        }

        internal object GetMovies()
        {
            throw new NotImplementedException();
        }

        private void ValidateMovie(Movies movie)
        {
            if (string.IsNullOrWhiteSpace(movie.Titulo))
            {
                throw new ArgumentException("El título de la película no puede estar vacío.");
            }

            if (movie.Duracion <= 0)
            {
                throw new ArgumentException("La duración de la película debe ser mayor a 0.");
            }

            if (movie.FechaEstreno == default)
            {
                throw new ArgumentException("La fecha de estreno de la película no es válida.");
            }
        }
    }
}
