namespace cine_web_app.back_end.Models
{
    public class Users
    {
        public int UserID { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string GoogleId { get; set; } // ID único de Google
        public string PictureUrl { get; set; } // Foto de perfil de Google
        public string[] Roles { get; set; } 

        // Constructor por defecto
        public Users()
        {
            Roles = Array.Empty<string>(); // Inicializa el array vacío
        }

        // Constructor opcional para inicialización rápida
        public Users(string nombre, string correo, string password, string googleId, string pictureUrl, string[] roles)
        {
            Nombre = nombre;
            Correo = correo;
            Password = password;
            GoogleId = googleId;
            PictureUrl = pictureUrl;
            Roles = roles;
        }

        // Método opcional para depuración (ToString)
        public override string ToString()
        {
            return $"{Nombre} ({Correo}) - GoogleID: {GoogleId}, Roles: {string.Join(", ", Roles)}";
        }
    }
}
