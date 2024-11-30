using System;

namespace cine_web_app.back_end.Models
{
    public class Entrada
    {
        public int Id { get; set; } // Opcional, si necesitas un identificador único
        public string Tipo { get; set; } // 'Normal' o 'VIP'
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
