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


// Un DTO (Data Transfer Object, por sus siglas en inglés) es un objeto simple utilizado para transferir datos entre diferentes partes de un sistema o aplicación, especialmente entre el cliente (frontend) y el servidor (backend), o entre diferentes capas de una aplicación.
// ¿Por qué usar un DTO?
// Los DTO se usan principalmente para estructurar y simplificar los datos que se envían o reciben, evitando exponer información sensible o innecesaria del modelo principal (como contraseñas, IDs internos, etc.). 
// También ayudan a mantener una separación clara entre la lógica del negocio (modelos internos) y los datos que se exponen a través de la API o las vistas.