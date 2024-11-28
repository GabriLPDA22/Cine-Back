using cine_web_app.back_end.Models;
using cine_web_app.back_end.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// ==================== CONFIGURACIÓN DE SERVICIOS ====================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro de servicios necesarios
builder.Services.AddSingleton<ProductoService>();
builder.Services.AddSingleton<CineService>();
builder.Services.AddSingleton<ButacaService>();
builder.Services.AddSingleton<PedidoService>(); // Registro del servicio de pedidos


// Configuración de CORS
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

// ==================== CONFIGURACIÓN DE SWAGGER ====================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ==================== CONFIGURACIÓN DE CORS ====================
app.UseCors("PermitirFrontend");

// ==================== OBTENCIÓN DE SERVICIOS ====================
var cineService = app.Services.GetRequiredService<CineService>();
var butacaService = app.Services.GetRequiredService<ButacaService>();
var pedidoService = app.Services.GetRequiredService<PedidoService>();


// ==================== ENDPOINTS DE CINES ====================

// Endpoint para obtener la lista de cines
app.MapGet("/api/Cine/GetCines", () =>
{
    var cinesSinPeliculas = cineService.ObtenerCines().Select(c => new
    {
        c.Id,
        c.Nombre
    }).ToList();

    return Results.Ok(cinesSinPeliculas);
}).WithName("GetCines")
.WithTags("Cine"); // Asignar el endpoint al grupo "Cine"

// Endpoint para obtener cines por id
app.MapGet("/api/Cine/GetCineById", (int cineId, CineService cineService) =>
{
    var cine = cineService.ObtenerCinePorId(cineId);
    if (cine == null)
    {
        return Results.NotFound($"No se encontró un cine con el ID: {cineId}");
    }

    var resultado = new
    {
        cine.Id,
        cine.Nombre
    };

    return Results.Ok(resultado);
}).WithName("GetCineById")
.WithTags("Cine"); // Asignar el endpoint al grupo "Cine"

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
})
.WithTags("Cine"); // Asignar el endpoint al grupo "Cine"

// Endpoint para obtener información de selección de asientos
app.MapGet("/api/Cine/GetSeatSelectionInfo", (string cineName, string movieTitle, string sessionDate, string sessionTime) =>
{
    var cine = cineService.ObtenerCines().FirstOrDefault(c => c.Nombre == cineName);
    if (cine == null)
    {
        return Results.NotFound("Cine no encontrado");
    }

    var pelicula = cine.Peliculas?.FirstOrDefault(p => p.Titulo == movieTitle);
    if (pelicula == null)
    {
        return Results.NotFound("Película no encontrada en este cine");
    }

    if (pelicula.Sesiones == null || !pelicula.Sesiones.TryGetValue(cineName, out var sesionesPorFecha))
    {
        return Results.NotFound("No hay sesiones para este cine");
    }

    if (!sesionesPorFecha.TryGetValue(sessionDate, out var sesiones) || sesiones == null)
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
}).WithName("GetSeatSelectionInfo")
.WithTags("Cine"); // Asignar el endpoint al grupo "Cine"


// ==================== ENDPOINTS DE PELÍCULAS ====================

// Endpoint para obtener todas las películas
app.MapGet("/api/Movie/GetPeliculas", () =>
{
    return Results.Ok(cineService.ObtenerCines().SelectMany(c => c.Peliculas));
}).WithName("GetPeliculas")
.WithTags("Peliculas");

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
}).WithName("GetPeliculaById")
.WithTags("Peliculas");

// Endpoint para obtener películas en cartelera
app.MapGet("/api/Movie/GetPeliculasEnCartelera", () =>
{
    return Results.Ok(cineService.ObtenerPeliculasEnCartelera());
}).WithName("GetPeliculasEnCartelera")
.WithTags("Peliculas");

// Endpoint para obtener películas en venta anticipada
app.MapGet("/api/Movie/GetPeliculasEnVentaAnticipada", () =>
{
    return Results.Ok(cineService.ObtenerPeliculasEnVentaAnticipada());
}).WithName("GetPeliculasEnVentaAnticipada")
.WithTags("Peliculas");

// Endpoint para obtener películas próximas
app.MapGet("/api/Movie/GetPeliculasProximas", () =>
{
    return Results.Ok(cineService.ObtenerPeliculasProximas());
}).WithName("GetPeliculasProximas")
.WithTags("Peliculas");

// ==================== ENDPOINTS DE BUTACAS ====================

// Endpoint para obtener todas las butacas
app.MapGet("/api/Butacas/GetButacas", () =>
{
    var butacas = butacaService.ObtenerButacas();
    if (butacas == null || butacas.Count == 0)
    {
        return Results.NotFound(new { mensaje = "No hay butacas disponibles. Inicializa las butacas primero." });
    }
    return Results.Ok(butacas);
}).WithName("GetButacas")
.WithTags("Butacas");

// Endpoint para inicializar las butacas desde el front-end REVISA EL ASYNC
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
        return Results.BadRequest(new { mensaje = "Error al inicializar las butacas.", detalle = ex.Message });
    }
}).WithName("InicializarButacas")
.WithTags("Butacas");

// Endpoint para reservar butacas
app.MapPost("/api/Butacas/ReservarButacas", (List<string> coordenadasButacas) =>
{
    if (coordenadasButacas == null || !coordenadasButacas.Any())
    {
        return Results.BadRequest(new { mensaje = "La lista de coordenadas no puede estar vacía." });
    }

    var resultado = butacaService.ReservarButacas(coordenadasButacas);

    if (resultado)
    {
        return Results.Ok(new { mensaje = "Butacas reservadas con éxito." });
    }

    return Results.BadRequest(new { mensaje = "Error al reservar butacas. Puede que alguna ya esté ocupada o no exista." });
}).WithName("ReservarButacas")
.WithTags("Butacas");

// Endpoint para liberar butacas
app.MapPost("/api/Butacas/LiberarButacas", (List<string> coordenadasButacas) =>
{
    if (coordenadasButacas == null || !coordenadasButacas.Any())
    {
        return Results.BadRequest(new { mensaje = "La lista de coordenadas no puede estar vacía." });
    }

    var resultado = butacaService.LiberarButacas(coordenadasButacas);

    if (resultado)
    {
        return Results.Ok(new { mensaje = "Butacas liberadas con éxito." });
    }

    return Results.BadRequest(new { mensaje = "Error al liberar butacas. Puede que alguna no esté ocupada o no exista." });
}).WithName("LiberarButacas")
.WithTags("Butacas");

// Endpoint para calcular el precio total incluyendo suplementos
app.MapPost("/api/Butacas/CalcularPrecio", (List<string> coordenadasButacas) =>
{
    if (coordenadasButacas == null || !coordenadasButacas.Any())
    {
        return Results.BadRequest(new { mensaje = "La lista de coordenadas no puede estar vacía." });
    }

    var suplementoTotal = butacaService.CalcularSuplemento(coordenadasButacas);
    return Results.Ok(new { precioTotal = suplementoTotal });
}).WithName("CalcularPrecio")
.WithTags("Butacas");

// Endpoint para obtener solo butacas VIP
app.MapGet("/api/Butacas/GetButacasVIP", () =>
{
    var butacasVIP = butacaService.ObtenerButacas().Where(b => b.Categoria == "VIP").ToList();
    if (butacasVIP == null || !butacasVIP.Any())
    {
        return Results.NotFound(new { mensaje = "No se encontraron butacas VIP." });
    }

    return Results.Ok(butacasVIP);
}).WithName("GetButacasVIP")
.WithTags("Butacas");

// Endpoint para reestablecer todas las butacas al estado inicial
app.MapPost("/api/Butacas/ReestablecerButacas", () =>
{
    butacaService.ReestablecerButacas();
    return Results.Ok(new { mensaje = "Butacas reestablecidas al estado inicial." });
}).WithName("ReestablecerButacas")
.WithTags("Butacas");


// ==================== ENDPOINTS DE PRODUCTOS ====================

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
}).WithName("GetProductos")
.WithTags("Productos");

// Endpoint para obtener todas las categorías de productos
app.MapGet("/api/Productos/GetCategorias", () =>
{
    var productoService = app.Services.GetRequiredService<ProductoService>();
    return Results.Ok(productoService.ObtenerCategorias());
}).WithName("GetCategorias")
.WithTags("Productos");

// ==================== ENDPOINTS DE PEDIDOS ====================

// Endpoint para obtener todos los pedidos
app.MapGet("/api/Pedido/GetPedidos", () =>
{
    var pedidos = pedidoService.ObtenerPedidos();
    return Results.Ok(pedidos);
}).WithName("GetPedidos")
.WithTags("Pedidos");

// Endpoint para obtener un pedido por ID
app.MapGet("/api/Pedido/GetPedidoPorId/{id}", (int id) =>
{
    var pedido = pedidoService.ObtenerPedidoPorId(id);
    if (pedido == null)
    {
        return Results.NotFound("Pedido no encontrado");
    }
    return Results.Ok(pedido);
}).WithName("GetPedidoPorId")
.WithTags("Pedidos");


// Endpoint para crear un nuevo pedido
app.MapPost("/api/Pedido/CreatePedido", (Pedido pedido) =>
{
    if (pedido == null)
    {
        return Results.BadRequest("El pedido no puede ser nulo.");
    }

    // Validación de campos obligatorios
    if (string.IsNullOrEmpty(pedido.NombreCliente) ||
        string.IsNullOrEmpty(pedido.TituloPelicula) ||
        string.IsNullOrEmpty(pedido.Cine))
    {
        return Results.BadRequest("Faltan datos obligatorios en el pedido.");
    }


    // Agregar el pedido al servicio
    pedidoService.AgregarPedido(pedido);

    // Retornar el ID del pedido recién creado
    return Results.Ok(new
    {
        Message = "Pedido creado correctamente",
        PedidoId = pedido.Id
    });
}).WithName("CreatePedido")
.WithTags("Pedidos");

// ==================== EJECUCIÓN DE LA APLICACIÓN ====================
app.Run();