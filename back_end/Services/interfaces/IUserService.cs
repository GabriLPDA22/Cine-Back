using cine_web_app.back_end.Models;
using Google.Apis.Auth;

namespace CineAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(Users user);
        Task DeleteUserAsync(int userId);
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users?> GetUserByIdAsync(int userId);
        Task<Users?> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(Users user);

        // Modificado para aceptar un payload de Google
        Task<Users> GetOrCreateUserAsync(GoogleJsonWebSignature.Payload payload); 
    }
}
