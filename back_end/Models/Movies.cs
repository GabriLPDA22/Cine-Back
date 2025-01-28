using cine_web_app.back_end.Models;

public class Movies
{
    public int PeliculaID { get; set; } // Identificador único de la película
    public string Titulo { get; set; } // Título de la película
    public string Genero { get; set; } // Género principal
    public int Duracion { get; set; } // Duración en minutos
    public string Clasificacion { get; set; } // Clasificación por edad
    public string Idioma { get; set; } // Idioma principal
    public string Sinopsis { get; set; } // Descripción breve de la película
    public DateTime FechaEstreno { get; set; } // Fecha de estreno
    public string Director { get; set; } // Nombre del director
    public string Actores { get; set; } // Lista de actores (puede mejorarse con una relación)
    public string Portada { get; set; } // URL del cartel
    public string Banner { get; set; } // URL del banner
    public double Calificacion { get; set; } // Promedio de calificación de usuarios
    public int EdadRecomendada { get; set; } // Edad recomendada
    public string ImagenEdadRecomendada { get; set; } // Imagen asociada a la edad recomendada
    public bool EnCartelera { get; set; } // Indica si está en cartelera
    public bool EnVentaAnticipada { get; set; } // Indica si está en venta anticipada
    public int Puntuacion { get; set; } // Puntuación interna
    public Dictionary<string, Dictionary<string, List<Sesion>>> Sesiones { get; set; } 
    // Diccionario para gestionar las sesiones por cine y fecha
}
