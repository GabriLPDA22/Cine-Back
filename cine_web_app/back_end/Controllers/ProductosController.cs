using Microsoft.AspNetCore.Mvc; // Espacio de nombres para ControllerBase y ApiController
using cine_web_app.back_end.Services; // Espacio de nombres para ProductoService
using cine_web_app.back_end.Models; // Espacio de nombres para Producto

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly ProductoService _productoService;

    public ProductosController(ProductoService productoService)
    {
        _productoService = productoService;
    }

    // Obtener productos
    [HttpGet("GetProductos")]
    public IActionResult GetProductos()
    {
        var productos = _productoService.ObtenerProductos();
        return Ok(productos);
    }

    // Obtener categorías
    [HttpGet("GetCategorias")]
    public IActionResult GetCategorias()
    {
        var categorias = _productoService.ObtenerCategorias();
        return Ok(categorias);
    }
}

