using System;
using System.Collections.Generic;

namespace cine_web_app.back_end.Models
{
        public class Pedido
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        public string EmailCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string TituloPelicula { get; set; }
        public string Cine { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Sala { get; set; }
        public List<string> ButacasReservadas { get; set; }
        public decimal TotalPago { get; set; }

        public List<Entrada> Entradas { get; set; } // Agregamos esta propiedad
        public List<Producto> Productos { get; set; }
    }
}
