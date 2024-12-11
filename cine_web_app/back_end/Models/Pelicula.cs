using System;
using System.Collections.Generic;

namespace cine_web_app.back_end.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string FechaEstreno { get; set; }
        public string Genero { get; set; }
        public string Duracion { get; set; }
        public double Calificacion { get; set; }
        public string Imagen { get; set; }
        public string Cartel { get; set; }
        public string Director { get; set; }
        public string Actores { get; set; }
        public int EdadRecomendada { get; set; }
        public string ImagenEdadRecomendada { get; set; }
        public bool EnCartelera { get; set; } // Campo para identificar si está en cartelera
        public bool EnVentaAnticipada { get; set; } // Campo para identificar si está en venta anticipada
        public string opiniones {get;set;}
        public int puntuacion {get; set;}

        // Cambiar la propiedad Sesiones para soportar cines, fechas y sesiones
        public Dictionary<string, Dictionary<string, List<Sesion>>> Sesiones { get; set; }

    }
}
