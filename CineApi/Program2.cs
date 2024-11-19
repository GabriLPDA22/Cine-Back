using cine_web_app.back_end.Models;
using cine_web_app.back_end.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProductoService>();
builder.Services.AddSingleton<CineService>();
builder.Services.AddSingleton<ButacaService>(); // Registrar el servicio de butacas

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

// Obtener el servicio de ButacaService
var butacaService = app.Services.GetRequiredService<ButacaService>();

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

    var cineConPeliculas = new
    {
        cine.Id,
        cine.Nombre,
        peliculas = cine.Peliculas.Select(pelicula => new
        {
            pelicula.Id,
            pelicula.Titulo,
            pelicula.Sesiones
        }).ToList()
    };

    return Results.Ok(cineConPeliculas);
});

// Endpoint para obtener todas las películas
app.MapGet("/api/Movie/GetPeliculas", () =>
{
    return Results.Ok(cineService.ObtenerCines().SelectMany(c => c.Peliculas));
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

// Endpoint para obtener todas las butacas
app.MapGet("/api/Butacas/GetButacas", () =>
{
    var butacas = butacaService.ObtenerButacas();
    if (butacas.Count == 0)
    {
        return Results.NotFound(new { mensaje = "No hay butacas disponibles. Inicializa las butacas primero." });
    }
    return Results.Ok(butacas);
}).WithName("GetButacas");

// Endpoint para reservar butacas
app.MapPost("/api/Butacas/ReservarButacas", (List<string> coordenadasButacas) =>
{
    Console.WriteLine("Coordenadas recibidas para reservar: " + string.Join(", ", coordenadasButacas));

    var resultado = butacaService.ReservarButacas(coordenadasButacas);

    if (resultado)
    {
        Console.WriteLine("Reservas realizadas con éxito.");
        return Results.Ok(new { mensaje = "Butacas reservadas con éxito." });
    }

    Console.WriteLine("Error al reservar butacas: Alguna ya está ocupada o no existe.");
    return Results.BadRequest(new { mensaje = "Error al reservar butacas. Puede que alguna ya esté ocupada o no exista." });
}).WithName("ReservarButacas");

// Endpoint para inicializar las butacas desde el front-end
app.MapPost("/api/Butacas/InicializarButacas", async (HttpRequest request) =>
{
    try
    {
        var butacasIniciales = await request.ReadFromJsonAsync<List<Butaca>>();
        if (butacasIniciales == null || !butacasIniciales.Any())
        {
            return Results.BadRequest(new { mensaje = "La lista de butacas no puede estar vacía." });
        }

        butacaService.InicializarButacas(butacasIniciales);
        return Results.Ok(new { mensaje = "Butacas inicializadas con éxito." });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al inicializar las butacas: {ex.Message}");
        return Results.BadRequest(new { mensaje = "Error al inicializar las butacas.", detalle = ex.Message });
    }
}).WithName("InicializarButacas");




// Endpoint para reestablecer todas las butacas al estado inicial
app.MapPost("/api/Butacas/ReestablecerButacas", () =>
{
    butacaService.ReestablecerButacas();
    Console.WriteLine("Butacas reestablecidas al estado inicial.");
    return Results.Ok(new { mensaje = "Butacas reestablecidas al estado inicial." });
}).WithName("ReestablecerButacas");


// Endpoint para obtener todos los productos
app.MapGet("/api/Productos/GetProductos", (string? categoria) =>
{
    var productoService = app.Services.GetRequiredService<ProductoService>();

    if (string.IsNullOrEmpty(categoria))
    {
        return Results.Ok(productoService.ObtenerProductos());
    }

    try
    {
        var productosPorCategoria = productoService.ObtenerProductosPorCategoria(categoria);
        return Results.Ok(productosPorCategoria);
    }
    catch (ArgumentException ex)
    {
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
