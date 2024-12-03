using Microsoft.AspNetCore.Mvc;
using cine_web_app.back_end.Services;
using cine_web_app.back_end.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly ProductoService _productoService;

    public ProductosController(ProductoService productoService)
    {
        _productoService = productoService;
    }

    // Obtener productos por categoría
    [HttpGet("GetProductos")]
    public IActionResult GetProductos([FromQuery] string? categoria)
    {
        try
        {
            var productos = string.IsNullOrEmpty(categoria)
                ? _productoService.ObtenerProductos() // Si no se especifica categoría, devuelve todos los productos
                : _productoService.ObtenerProductosPorCategoria(categoria);

            return Ok(productos);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Error al obtener los productos", Details = ex.Message });
        }
    }

    // Obtener todas las categorías disponibles
    [HttpGet("GetCategorias")]
    public IActionResult GetCategorias()
    {
        try
        {
            var categorias = _productoService.ObtenerCategorias();
            return Ok(categorias);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Error al obtener las categorías", Details = ex.Message });
        }
    }

}
