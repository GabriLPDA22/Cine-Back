using System;
using System.Collections.Generic;

namespace cine_web_app.back_end.Models
public enum CategoriaAsiento
{
    Estandar,
    VIP
}

public class Butaca
{
    public int Id { get; set; } // Identificador único
    public CategoriaAsiento Categoria { get; set; } // Categoría del asiento
    public bool EstaOcupado { get; set; } // Indica si está ocupado
    public string Descripcion { get; set; } // Descripción de la butaca
    public int Suplemento { get; set; } // Suplemento asignado según la categoría
}

    
