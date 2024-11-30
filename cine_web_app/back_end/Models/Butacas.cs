using System;

namespace cine_web_app.back_end.Models
{
    public enum CategoriaAsiento
    {
        Estandar,
        VIP
    }

    public class Butaca
    {
        public string Id { get; set; } // Asegúrate de que sea un string si envías IDs como "0-1"
        public string Categoria { get; set; } // "Estandar" o "VIP"
        public bool EstaOcupado { get; set; } // true o false
        public string Descripcion { get; set; } // Coordenadas o detalles adicionales
        public int Suplemento { get; set; } // 0 para Estandar, 5 para VIP
        public string Sala {get; set;} // sala para cada butacas
    }
}
