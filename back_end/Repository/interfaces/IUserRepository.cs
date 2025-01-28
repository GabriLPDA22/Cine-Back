using cine_web_app.back_end.Models;
using System.Threading.Tasks;

namespace CineAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        // Obtener usuario por GoogleId
        Task<Users?> GetByGoogleIdAsync(string googleId);
        
        // Obtener usuario por email
        Task<Users?> GetByEmailAsync(string email);

        // Crear un nuevo usuario
        Task AddAsync(Users user);

        // Actualizar un usuario existente
        Task UpdateAsync(Users user);

        // Eliminar un usuario
        Task DeleteAsync(int userId);

        // Obtener todos los usuarios
        Task<IEnumerable<Users>> GetAllAsync();

        // Obtener un usuario por su ID
        Task<Users?> GetByIdAsync(int userId);
    }
}
