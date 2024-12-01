using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using cine_web_app.back_end.Models;
using cine_web_app.back_end.Models.DTO;

namespace cine_web_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static List<Usuario> usuarios = new List<Usuario>();
        private readonly PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();
        private readonly string JwtSecretKey = "TuClaveSecretaSuperSegura"; // Mismo valor configurado en Program.cs

        [HttpPost("register")]
        public IActionResult Register([FromBody] UsuarioRegisterDto dto)
        {
            if (usuarios.Any(u => u.Correo == dto.Correo))
            {
                return BadRequest(new { mensaje = "El correo ya está en uso." });
            }

            // Crear un nuevo usuario con los datos del DTO
            var nuevoUsuario = new Usuario
            {
                Id = usuarios.Count + 1,
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                ContraseñaHash = passwordHasher.HashPassword(null, dto.Contraseña)
            };

            usuarios.Add(nuevoUsuario);

            return Ok(new { mensaje = "Usuario registrado exitosamente." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Correo) || string.IsNullOrEmpty(dto.Contraseña))
            {
                return BadRequest(new { mensaje = "Datos incompletos." });
            }

            var usuarioExistente = usuarios.FirstOrDefault(u => u.Correo == dto.Correo);
            if (usuarioExistente == null)
            {
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });
            }

            var resultado = passwordHasher.VerifyHashedPassword(usuarioExistente, usuarioExistente.ContraseñaHash, dto.Contraseña);
            if (resultado == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });
            }

            // Generar JWT
            var token = GenerarToken(usuarioExistente);

            // Crear un DTO de respuesta para devolver datos seguros
            var response = new UsuarioResponseDto
            {
                Id = usuarioExistente.Id,
                Nombre = usuarioExistente.Nombre,
                Correo = usuarioExistente.Correo
            };

            return Ok(new
            {
                mensaje = "Inicio de sesión exitoso",
                token = token,
                usuario = response
            });
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
