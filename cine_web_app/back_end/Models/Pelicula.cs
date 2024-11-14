using System;
using System.Collections.Generic;

namespace cine_web_app.back_end.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Genero { get; set; }
        public string Duracion { get; set; }
        public double Calificacion { get; set; }
        public string Imagen { get; set; }
        public string Cartel { get; set; }
        public string Director { get; set; }
        public string Actores { get; set; }
        public int EdadRecomendada { get; set; }
        public string ImagenEdadRecomendada { get; set; }
        
        // Agregar la propiedad Sesiones para almacenar las sesiones por fecha
        public Dictionary<string, List<Sesion>> Sesiones { get; set; }
    }
}
