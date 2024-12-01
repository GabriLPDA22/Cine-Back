using System.Text.Json.Serialization; // Importante para JsonIgnore

namespace cine_web_app.back_end.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }

        [JsonIgnore] // Ignorar esta propiedad al serializar
        public string ContraseñaHash { get; set; }
    }
}



// Un DTO (Data Transfer Object, por sus siglas en inglés) es un objeto simple utilizado para transferir datos entre diferentes partes de un sistema o aplicación, especialmente entre el cliente (frontend) y el servidor (backend), o entre diferentes capas de una aplicación.
// ¿Por qué usar un DTO?
// Los DTO se usan principalmente para estructurar y simplificar los datos que se envían o reciben, evitando exponer información sensible o innecesaria del modelo principal (como contraseñas, IDs internos, etc.). 
// También ayudan a mantener una separación clara entre la lógica del negocio (modelos internos) y los datos que se exponen a través de la API o las vistas.