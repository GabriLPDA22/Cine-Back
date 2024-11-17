using System;
using cine_web_app.back_end.Models;



namespace cine_web_app.back_end.Models
{
public class Sesion
    {
        public DateTime Fecha { get; set; }  // Fecha de la sesión
        public string Hora { get; set; }
        public string Sala { get; set; }
        public bool EsISense { get; set; }
        public bool EsVOSE { get; set; }
    }
}
