using cine_web_app.back_end.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineAPI.Services.Interfaces
{
    public interface IMovieService
    {
        Task AddMovieAsync(Movies movie);
        Task DeleteMovieAsync(int peliculaID);
        Task<IEnumerable<Movies>> GetAllMoviesAsync();
        Task<Movies?> GetMovieByIdAsync(int peliculaID);
        Task UpdateMovieAsync(Movies movie);
    }
}
