using CineAPI.Repositories.Interfaces;
using cine_web_app.back_end.Models;
using System.Security.Cryptography;
using System.Text;
using CineAPI.Services.Interfaces;
using Google.Apis.Auth;

namespace CineAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(Users user)
        {
            // Validar si el correo ya existe
            var existingUser = await _userRepository.GetByEmailAsync(user.Correo);
            if (existingUser != null)
            {
                throw new System.Exception("El correo ya está registrado.");
            }

            // Hashear la contraseña antes de agregar
            user.Password = HashPassword(user.Password);

            // Agregar el usuario al repositorio
            await _userRepository.AddAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            // Verificar si el usuario existe
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new System.Exception("Usuario no encontrado.");
            }

            // Eliminar el usuario
            await _userRepository.DeleteAsync(userId);
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<Users?> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task UpdateUserAsync(Users user)
        {
            // Verificar si el usuario existe
            var existingUser = await _userRepository.GetByIdAsync(user.UserID);
            if (existingUser == null)
            {
                throw new System.Exception("Usuario no encontrado.");
            }

            // Hashear la contraseña si se proporciona una nueva
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = HashPassword(user.Password);
            }

            // Actualizar el usuario
            await _userRepository.UpdateAsync(user);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        // Implementación de GetOrCreateUserAsync para Google
        public async Task<Users> GetOrCreateUserAsync(GoogleJsonWebSignature.Payload payload)
        {
            // Buscar el usuario por GoogleId o Correo
            var user = await _userRepository.GetByGoogleIdAsync(payload.Subject)
                       ?? await _userRepository.GetByEmailAsync(payload.Email);

            // Si el usuario no existe, crearlo
            if (user == null)
            {
                // Determinar el rol basado en el correo
                string[] roles = new string[] { "User" };  // Rol por defecto

                // Asignar rol Admin si el correo pertenece a un dominio específico (ejemplo)
                if (payload.Email.EndsWith("@admin.com")) // Cambia esto a tu lógica de asignación de Admin
                {
                    roles = new string[] { "Admin" };
                }

                user = new Users
                {
                    Nombre = payload.Name,
                    Correo = payload.Email,
                    GoogleId = payload.Subject,  // GoogleId único para cada usuario
                    PictureUrl = payload.Picture, // Foto de perfil proporcionada por Google
                    Roles = roles // Asignar el rol correspondiente
                };

                await _userRepository.AddAsync(user); // Agregar el nuevo usuario
            }
            else
            {
                // Si ya existe, actualizar algunos detalles (si es necesario)
                user.Nombre = payload.Name;
                user.PictureUrl = payload.Picture; // Actualizar la foto si ha cambiado
                await _userRepository.UpdateAsync(user);
            }

            return user;
        }

    }
}
