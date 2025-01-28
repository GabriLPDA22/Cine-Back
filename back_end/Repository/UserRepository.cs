using Npgsql;
using cine_web_app.back_end.Models;
using CineAPI.Repositories.Interfaces;

namespace CineAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(Users user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "INSERT INTO Users (Nombre, Correo, Password, Roles, GoogleId, PictureUrl) VALUES (@Nombre, @Correo, @Password, @Roles, @GoogleId, @PictureUrl)";
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@Nombre", user.Nombre);
            command.Parameters.AddWithValue("@Correo", user.Correo);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Roles", user.Roles); // Inserta Roles como un array
            command.Parameters.AddWithValue("@GoogleId", user.GoogleId); // Inserta GoogleId
            command.Parameters.AddWithValue("@PictureUrl", user.PictureUrl); // Inserta PictureUrl

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "DELETE FROM Users WHERE UserID = @UserID";
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", id);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            var users = new List<Users>();

            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT * FROM Users";
            using var command = new NpgsqlCommand(query, connection);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                users.Add(new Users
                {
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Correo = reader.GetString(reader.GetOrdinal("Correo")),
                    Password = reader.GetString(reader.GetOrdinal("Password")),
                    Roles = reader.GetFieldValue<string[]>(reader.GetOrdinal("Roles")),
                    GoogleId = reader.IsDBNull(reader.GetOrdinal("GoogleId")) ? null : reader.GetString(reader.GetOrdinal("GoogleId")),
                    PictureUrl = reader.IsDBNull(reader.GetOrdinal("PictureUrl")) ? null : reader.GetString(reader.GetOrdinal("PictureUrl"))
                });
            }

            return users;
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT * FROM Users WHERE UserID = @UserID";
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Users
                {
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Correo = reader.GetString(reader.GetOrdinal("Correo")),
                    Password = reader.GetString(reader.GetOrdinal("Password")),
                    Roles = reader.GetFieldValue<string[]>(reader.GetOrdinal("Roles")),
                    GoogleId = reader.IsDBNull(reader.GetOrdinal("GoogleId")) ? null : reader.GetString(reader.GetOrdinal("GoogleId")),
                    PictureUrl = reader.IsDBNull(reader.GetOrdinal("PictureUrl")) ? null : reader.GetString(reader.GetOrdinal("PictureUrl"))
                };
            }

            return null;
        }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT * FROM Users WHERE Correo = @Correo";
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@Correo", email);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Users
                {
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Correo = reader.GetString(reader.GetOrdinal("Correo")),
                    Password = reader.GetString(reader.GetOrdinal("Password")),
                    Roles = reader.GetFieldValue<string[]>(reader.GetOrdinal("Roles")),
                    GoogleId = reader.IsDBNull(reader.GetOrdinal("GoogleId")) ? null : reader.GetString(reader.GetOrdinal("GoogleId")),
                    PictureUrl = reader.IsDBNull(reader.GetOrdinal("PictureUrl")) ? null : reader.GetString(reader.GetOrdinal("PictureUrl"))
                };
            }

            return null;
        }

        public async Task UpdateAsync(Users user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "UPDATE Users SET Nombre = @Nombre, Correo = @Correo, Password = @Password, Roles = @Roles, GoogleId = @GoogleId, PictureUrl = @PictureUrl WHERE UserID = @UserID";
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", user.UserID);
            command.Parameters.AddWithValue("@Nombre", user.Nombre);
            command.Parameters.AddWithValue("@Correo", user.Correo);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Roles", user.Roles);
            command.Parameters.AddWithValue("@GoogleId", user.GoogleId); // Actualiza GoogleId
            command.Parameters.AddWithValue("@PictureUrl", user.PictureUrl); // Actualiza PictureUrl

            await command.ExecuteNonQueryAsync();
        }

        public Task<Users?> GetByGoogleIdAsync(string googleId)
        {
            throw new NotImplementedException();
        }
    }
}
