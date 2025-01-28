namespace CineAPI.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task AddAsync(Movies movie);
        Task DeleteAsync(int peliculaID);
        Task<IEnumerable<Movies>> GetAllAsync();
        Task<Movies?> GetByIdAsync(int peliculaID);
        Task UpdateAsync(Movies movie);
    }
}
