using cine_web_app.back_end.Models; // Corregido el espacio de nombres
using cine_web_app.back_end.Services; // Corregido el espacio de nombres
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;  // Agregar el espacio de nombres Newtonsoft.Json
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
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

app.UseCors("PermitirFrontend");

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
    // Obtener el cine por su ID
    var cine = cineService.ObtenerCinePorId(cineId);

    // Si el cine no existe, devolver un error 404
    if (cine == null)
    {
        return Results.NotFound("Cine no encontrado");
    }

    // Asegurarnos de que las sesiones se devuelvan correctamente
    var cineConPeliculas = new
    {
        cine.Id,
        cine.Nombre,
        peliculas = cine.Peliculas.Select(pelicula => new
        {
            pelicula.Id,
            pelicula.Titulo,
            pelicula.Sesiones // Devuelves las sesiones junto con la película
        }).ToList()
    };

    return Results.Ok(cineConPeliculas); // Devuelve las películas con las sesiones
});

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
});

// Endpoint para obtener las películas en cartelera
app.MapGet("/api/Movie/GetPeliculasEnCartelera", () =>
{
    return Results.Ok(cineService.ObtenerPeliculasEnCartelera());
}).WithName("GetPeliculasEnCartelera");

// Endpoint para obtener las películas en venta anticipada
app.MapGet("/api/Movie/GetPeliculasEnVentaAnticipada", () =>
{
    return Results.Ok(cineService.ObtenerPeliculasEnVentaAnticipada());
}).WithName("GetPeliculasEnVentaAnticipada");

// Endpoint para obtener las películas próximas
app.MapGet("/api/Movie/GetPeliculasProximas", () =>
{
    return Results.Ok(cineService.ObtenerPeliculasProximas());
}).WithName("GetPeliculasProximas");

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

    if (!pelicula.Sesiones.TryGetValue(cineName, out var sesionesPorFecha))
    {
        return Results.NotFound("No hay sesiones para este cine");
    }

    if (!sesionesPorFecha.TryGetValue(sessionDate, out var sesiones))
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

// Endpoint para obtener todos los productos
app.MapGet("/api/Productos/GetProductos", (string? categoria) =>
{
    var productoService = app.Services.GetRequiredService<ProductoService>();

    if (string.IsNullOrEmpty(categoria))
    {
        // Devuelve todos los productos si no se especifica una categoría
        return Results.Ok(productoService.ObtenerProductos());
    }

    try
    {
        // Devuelve productos filtrados por categoría
        var productosPorCategoria = productoService.ObtenerProductosPorCategoria(categoria);
        return Results.Ok(productosPorCategoria);
    }
    catch (ArgumentException ex)
    {
        // Devuelve un error si la categoría no es válida
        return Results.BadRequest(ex.Message);
    }
}).WithName("GetProductos");

// Endpoint para obtener todas las categorías
app.MapGet("/api/Productos/GetCategorias", () =>
{
    var productoService = app.Services.GetRequiredService<ProductoService>();
    return Results.Ok(productoService.ObtenerCategorias());
}).WithName("GetCategorias");


app.Run();
