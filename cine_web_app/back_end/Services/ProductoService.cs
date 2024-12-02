public class ProductoService
{
    private readonly List<Producto> _productos;
    private readonly List<string> _categoriasValidas = new List<string>
    {
        "Top Ventas", "Menus", "Palomitas", "Bebidas", "Hot Food", "Merchandising", "Snacks Dulces", "Infantil"
    };

    public ProductoService()
    {
 _productos = new List<Producto>
        {
            // Productos para Top Ventas
            new Producto { Id = 1, Nombre = "Menu Halloween Funko", Categorias = new List<string> { "Top Ventas", "Menus" }, Precio = 24.90M, ImagenUrl = "/../images/menu_halloween_funko.jpg" },
            new Producto { Id = 2, Nombre = "Menu Joker Funko", Categorias = new List<string> { "Top Ventas", "Menus" }, Precio = 23.90M, ImagenUrl = "/../images/Menu-Funko-Joker.jpg" },
            new Producto { Id = 5, Nombre = "Menu Haribo UP CH", Categorias = new List<string> { "Top Ventas", "Snacks Dulces" }, Precio = 22.30M, ImagenUrl = "/../images/menu_haribo.jpg" },
            new Producto { Id = 7, Nombre = "Menu Taylor Swift The Eras Tour", Categorias = new List<string> { "Top Ventas", "Hot Food", "Menus" }, Precio = 12.50M, ImagenUrl = "/../images/menu_taylor_swift.jpg" },
            
            // Productos para Bebidas
            new Producto { Id = 14, Nombre = "Vaso Taylor Swift Bebida CH", Categorias = new List<string> { "Bebidas","Top Ventas" }, Precio = 5.90M, ImagenUrl = "/../images/vaso_taylor_swift.jpg" },
            new Producto { Id = 15, Nombre = "Refrescos 1L", Categorias = new List<string> { "Bebidas" }, Precio = 5.70M, ImagenUrl = "/../images/refresco_1l.jpg" },
            new Producto { Id = 16, Nombre = "Refrescos 75CL", Categorias = new List<string> { "Bebidas" }, Precio = 5.20M, ImagenUrl = "/../images/refresco_75cl.jpg" },
            new Producto { Id = 17, Nombre = "Lata Cerveza", Categorias = new List<string> { "Bebidas" }, Precio = 3.50M, ImagenUrl = "/../images/lata_cerveza.jpg" },
            new Producto { Id = 18, Nombre = "Agua 75CL Stock", Categorias = new List<string> { "Bebidas" }, Precio = 2.60M, ImagenUrl = "/../images/agua_75cl.jpg" },
            new Producto { Id = 19, Nombre = "Agua 1L Stock", Categorias = new List<string> { "Bebidas" }, Precio = 3.30M, ImagenUrl = "/../images/agua_1l.jpg" },

            // Productos para Merchandising
            new Producto { Id = 20, Nombre = "Funko Freddy Stock", Categorias = new List<string> { "Merchandising","Top Ventas" }, Precio = 12.00M, ImagenUrl = "/../images/funko_freddy.jpg" },
            new Producto { Id = 21, Nombre = "Funko Foxy Stock", Categorias = new List<string> { "Merchandising" }, Precio = 12.00M, ImagenUrl = "/../images/funko_foxy.jpg" },
            new Producto { Id = 22, Nombre = "Funko Ghostface Stock", Categorias = new List<string> { "Merchandising" }, Precio = 12.00M, ImagenUrl = "/../images/funko_ghostface.jpg" },
            new Producto { Id = 23, Nombre = "Peluche Freddy Stock", Categorias = new List<string> { "Merchandising" }, Precio = 15.00M, ImagenUrl = "/../images/peluche_freddy.jpg" },
            new Producto { Id = 24, Nombre = "Peluche Foxy Stock", Categorias = new List<string> { "Merchandising" }, Precio = 15.00M, ImagenUrl = "/../images/peluche_foxy.jpg" },

            // Productos para Infantil
            new Producto { Id = 10, Nombre = "Infantil Bitty Pop CH", Categorias = new List<string> { "Infantil", "Snacks Dulces","Menus" }, Precio = 10.90M, ImagenUrl = "/../images/infantil_bitty_pop.jpg" },

            // Productos para Menus (que no están en Top Ventas, Bebidas, o Merchandising)
            new Producto { Id = 3, Nombre = "Menu Bucket Topuria", Categorias = new List<string> { "Menus", "Palomitas" }, Precio = 14.20M, ImagenUrl = "/../images/menu_bucket_topuria.jpg" },
            new Producto { Id = 4, Nombre = "Menu Transformers CH", Categorias = new List<string> { "Menus", "Palomitas" }, Precio = 15.20M, ImagenUrl = "/../images/menu_transformers.jpg" },
            new Producto { Id = 8, Nombre = "Menu Duo Taylor Swift The Eras Tour", Categorias = new List<string> { "Menus", "Hot Food" }, Precio = 16.40M, ImagenUrl = "/../images/menu_duo_taylor_swift.jpg" },
            new Producto { Id = 12, Nombre = "Menu Gladiator Vaso CH", Categorias = new List<string> { "Menus", "Merchandising" }, Precio = 29.50M, ImagenUrl = "/../images/menu_gladiator.jpg" }
        };
    }

    // Método para obtener todos los productos
    public IEnumerable<Producto> ObtenerProductos()
    {
        return _productos;
    }

    public IEnumerable<string> ObtenerCategorias()
    {
        return _categoriasValidas;
    }

    public IEnumerable<Producto> ObtenerProductosPorCategoria(string categoria)
    {
        if (string.IsNullOrEmpty(categoria))
            throw new ArgumentException("La categoría no puede estar vacía.");

        if (!_categoriasValidas.Contains(categoria))
            throw new ArgumentException($"La categoría '{categoria}' no es válida.");

        return _productos.Where(p => p.Categorias.Contains(categoria, StringComparer.OrdinalIgnoreCase));
    }
}
