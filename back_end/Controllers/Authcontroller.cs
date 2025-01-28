using Google.Apis.Auth;
using CineAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cine_web_app.back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        // Inyección de dependencia de IUserService
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Endpoint para login con Google
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            // Verificar el token recibido con Google
            var payload = await VerifyGoogleToken(request.Token);
            
            if (payload == null)
            {
                return Unauthorized("Invalid Google token.");
            }

            // Usar el servicio de usuario para crear o autenticar al usuario
            var user = await _userService.GetOrCreateUserAsync(payload);

            // Devuelve la información del usuario
            return Ok(new
            {
                user.UserID,
                user.Nombre,
                user.Correo,
                user.PictureUrl,
                user.Roles
            });
        }

        // Método para verificar el token de Google
        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string token)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { "YOUR_GOOGLE_CLIENT_ID" } // Reemplaza con tu Client ID de Google
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
                return payload;
            }
            catch
            {
                return null;
            }
        }
    }

    // Modelo de solicitud de login con Google
    public class GoogleLoginRequest
    {
        public string Token { get; set; }
    }
}
