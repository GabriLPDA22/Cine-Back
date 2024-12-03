using System;

namespace cine_web_app.back_end.Models
{
public class Sesion
    {


        public int Id { get; set; }  // Identificador de la sesión
        public DateTime Fecha { get; set; }  // Fecha de la sesión
        public string Hora { get; set; }
        public string Sala { get; set; }
        public bool EsISense { get; set; }
        public bool EsVOSE { get; set; }
    }
}
