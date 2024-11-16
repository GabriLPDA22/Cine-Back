using cine_web_app.back_end.Models; // Corregido el espacio de nombres
using cine_web_app.back_end.Services; // Corregido el espacio de nombres
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProductoService>();

// Registrar el servicio CineService
builder.Services.AddSingleton<CineService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "https://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Comentar esta línea si tienes problemas de SSL en localhost
// app.UseHttpsRedirection();

app.UseCors("PermitirFrontend");

// try
// {
//     app.UseStaticFiles(new StaticFileOptions
//     {
//         FileProvider = new PhysicalFileProvider(
//             Path.Combine(Directory.GetCurrentDirectory(), "front-end")),
//         RequestPath = "/cine_web_app/front-end"
//     });
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"Error al configurar archivos estáticos: {ex.Message}");
// }

// Obtener el servicio CineService
var cineService = app.Services.GetRequiredService<CineService>();

// Endpoint para obtener la lista de cines
app.MapGet("/api/Cine/GetCines", () =>
{
    var cinesSinPeliculas = cineService.ObtenerCines().Select(c => new
    {
        c.Id,
        c.Nombre
    }).ToList();

    return Results.Ok(cinesSinPeliculas);
}).WithName("GetCines");

// Endpoint para obtener un cine específico con sus películas
app.MapGet("/api/Cine/GetCineConPeliculas", (int cineId) =>
{
    var cine = cineService.ObtenerCinePorId(cineId);
    if (cine == null)
    {
        return Results.NotFound("Cine no encontrado");
    }
    return Results.Ok(cine);
}).WithName("GetCineConPeliculas");

// Endpoint para obtener todas las películas
app.MapGet("/api/Movie/GetPeliculas", () =>
{
    return Results.Ok(cineService.ObtenerCines().SelectMany(c => c.Peliculas)); // Devuelve todas las películas
}).WithName("GetPeliculas");

// Endpoint para obtener una película específica por ID
app.MapGet("/api/Movie/GetPeliculaById", (int id) =>
{
    var pelicula = cineService.ObtenerCines()
                              .SelectMany(c => c.Peliculas)
                              .FirstOrDefault(p => p.Id == id);
    if (pelicula == null)
    {
        return Results.NotFound("Película no encontrada");
    }
    return Results.Ok(pelicula);
}).WithName("GetPeliculaById");

// Endpoint para obtener información de selección de asientos
app.MapGet("/api/Cine/GetSeatSelectionInfo", (string cineName, string movieTitle, string sessionDate, string sessionTime) =>
{
    var cine = cineService.ObtenerCines().FirstOrDefault(c => c.Nombre == cineName);
    if (cine == null)
    {
        return Results.NotFound("Cine no encontrado");
    }

    var pelicula = cine.Peliculas.FirstOrDefault(p => p.Titulo == movieTitle);
    if (pelicula == null)
    {
        return Results.NotFound("Película no encontrada en este cine");
    }

    if (!pelicula.Sesiones.TryGetValue(sessionDate, out var sesiones))
    {
        return Results.NotFound("No hay sesiones para esta fecha");
    }

    var sesion = sesiones.FirstOrDefault(s => s.Hora == sessionTime);
    if (sesion == null)
    {
        return Results.NotFound("Sesión no encontrada en este horario");
    }

    var seatSelectionInfo = new
    {
        MovieTitle = pelicula.Titulo,
        CineName = cine.Nombre,
        SessionDate = sessionDate,
        SessionTime = sesion.Hora,
        Room = sesion.Sala,
        EsISense = sesion.EsISense,
        EsVOSE = sesion.EsVOSE,
        BannerImage = pelicula.Imagen
    };

    return Results.Ok(seatSelectionInfo);
}).WithName("GetSeatSelectionInfo");

// Obtener el servicio ProductoService
var productoService = app.Services.GetRequiredService<ProductoService>();

app.MapGet("/api/Productos/GetProductos", (string? categoria, ProductoService productoService) =>
{
    IEnumerable<Producto> productos;

    if (!string.IsNullOrEmpty(categoria))
    {
        if (categoria == "Top Ventas")
        {
            productos = productoService.ObtenerProductos();
        }
        else
        {
            productos = productoService.ObtenerProductos()
                                        .Where(p => p.Categorias.Contains(categoria));
        }
    }
    else
    {
        productos = productoService.ObtenerProductos();
    }

    return Results.Ok(productos);
}).WithName("GetProductos");

// Endpoint para obtener las categorías
app.MapGet("/api/Productos/GetCategorias", () =>
{
    return Results.Ok(new List<string> { "Top Ventas", "Menus", "Palomitas", "Bebidas", "Hot Food", "Merchandising", "Snacks Dulces", "Infantil" });
}).WithName("GetCategorias");

app.Run();
