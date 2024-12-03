using System;

namespace cine_web_app.back_end.Models
{


    public class Sala

    {
        public int id { get; set; }
        public string nombre { get; set; }
        public Butaca[,] Butacas { get; set; } = new Butaca[17, 20];

    }

}