using System.Text.Json.Serialization; // Asegúrate de agregar esta línea

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
    
    [JsonIgnore] // Ignora al serializar para no exponer la contraseña en respuestas JSON
    public string ContraseñaHash { get; set; }
    
    [JsonIgnore] // Ignora al serializar para no exponer temporalmente esta propiedad
    public string Contraseña { get; set; }
}
