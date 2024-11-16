public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<string> Categorias { get; set; } // Cambiado de string a List<string>
    public decimal Precio { get; set; }
    public string ImagenUrl { get; set; }
}
