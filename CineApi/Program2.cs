using cine_web_app.back_end.Models; // Corregido el espacio de nombres
using cine_web_app.back_end.Services; // Corregido el espacio de nombres
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;  // Agregar el espacio de nombres Newtonsoft.Json
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

app.MapGet("/api/Movie/GetPeliculasEnCartelera", () =>
{
    return Results.Ok(cineService.ObtenerPeliculasEnCartelera());
}).WithName("GetPeliculasEnCartelera");

app.MapGet("/api/Movie/GetPeliculasEnVentaAnticipada", () =>
{
    return Results.Ok(cineService.ObtenerPeliculasEnVentaAnticipada());
}).WithName("GetPeliculasEnVentaAnticipada");

app.MapGet("/api/Movie/GetPeliculasProximas", () =>
{
    return Results.Ok(cineService.ObtenerPeliculasProximas());
}).WithName("GetPeliculasProximas");

app.Run();
