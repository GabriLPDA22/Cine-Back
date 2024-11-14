using cine_web_app.back_end.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var usuarios = new List<Usuario>();

// Endpoint para registrar un nuevo usuario
app.MapPost("/api/auth/register", (Usuario nuevoUsuario) =>
{
    if (usuarios.Any(u => u.Correo == nuevoUsuario.Correo))
    {
        return Results.BadRequest("El correo ya está en uso.");
    }

    nuevoUsuario.Id = usuarios.Count + 1;
    nuevoUsuario.ContraseñaHash = new PasswordHasher<Usuario>().HashPassword(nuevoUsuario, nuevoUsuario.Contraseña);
    nuevoUsuario.Contraseña = null; // Elimina la contraseña en texto plano por seguridad
    usuarios.Add(nuevoUsuario);

    return Results.Ok("Usuario registrado exitosamente.");
}).WithName("RegisterUser");


// Endpoint para iniciar sesión
app.MapPost("/api/auth/login", (Usuario usuario) =>
{
    // Busca al usuario por correo
    var usuarioExistente = usuarios.FirstOrDefault(u => u.Correo == usuario.Correo);
    if (usuarioExistente == null)
    {
        return Results.Unauthorized(); // No se encuentra el correo
    }

    // Verifica la contraseña sin procesar contra el hash almacenado
    var passwordHasher = new PasswordHasher<Usuario>();
    var resultado = passwordHasher.VerifyHashedPassword(usuarioExistente, usuarioExistente.ContraseñaHash, usuario.Contraseña);

    if (resultado == PasswordVerificationResult.Failed)
    {
        return Results.Unauthorized(); // Contraseña incorrecta
    }

    return Results.Ok(new { mensaje = "Inicio de sesión exitoso", nombre = usuarioExistente.Nombre });
}).WithName("LoginUser");


// Nuevo Endpoint para obtener todos los usuarios registrados
app.MapGet("/api/auth/getUsers", () =>
{
    return Results.Ok(usuarios);
}).WithName("GetAllUsers");

// Declaración de la lista de cines (fuera del bloque de código para que sea global)
var cines = new List<Cine>
{
    new Cine
    {
        Id = 1,
        Nombre = "Puerto Venecia",
        Peliculas = new List<Pelicula>
        {
            new Pelicula
            {   
                Id = 1,
                Titulo = "Spider-Man: No Way Home",
                Descripcion = "Cuando la identidad de Spider-Man es revelada...",
                FechaEstreno = new DateTime(2021, 12, 17),
                Genero = "Sci-fi",
                Director = "Jon Watts",
                Duracion = "1 hora 55 minutos",
                Actores = "Tom Holland, Zendaya, Benedict Cumberbatch, Jacob Batalon, Willem Dafoe, Alfred Molina, Jamie Foxx",
                Calificacion = 8.2,
                Imagen = "/cine_web_app/front-end/images/Banner-Spiderman-no-way-home.jpg",
                Cartel = "/cine_web_app/front-end/images/Spiderman-No-Way-Home-Cartel.jpg",
                EdadRecomendada = 12,
                ImagenEdadRecomendada = "/cine_web_app/front-end/images/12.jpg",
                Sesiones = new Dictionary<string, List<Sesion>>
                {
                    { "2024-11-11", new List<Sesion>
                        {
                            new Sesion { Hora = "16:30", Sala = "10", EsISense = true, EsVOSE = false },
                            new Sesion { Hora = "18:05", Sala = "8", EsISense = false, EsVOSE = true }
                        }
                    },
                    { "2024-11-12", new List<Sesion>
                        {
                            new Sesion { Hora = "15:30", Sala = "3", EsISense = false, EsVOSE = false },
                            new Sesion { Hora = "17:45", Sala = "9", EsISense = true, EsVOSE = false },
                            new Sesion { Hora = "19:50", Sala = "6", EsISense = true, EsVOSE = true },
                            new Sesion { Hora = "22:10", Sala = "2", EsISense = false, EsVOSE = true }
                        }
                    },
                { "2024-11-13", new List<Sesion>
                    {
                        new Sesion { Hora = "16:00", Sala = "10", EsISense = true, EsVOSE = false },
                        new Sesion { Hora = "18:10", Sala = "8", EsISense = false, EsVOSE = true },
                        new Sesion { Hora = "20:25", Sala = "5", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "23:00", Sala = "3", EsISense = true, EsVOSE = true }
                    }
                },
                { "2024-11-14", new List<Sesion>
                    {
                        new Sesion { Hora = "14:45", Sala = "4", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "17:00", Sala = "7", EsISense = true, EsVOSE = true },
                        new Sesion { Hora = "19:30", Sala = "2", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "21:50", Sala = "10", EsISense = true, EsVOSE = false }
                    }
                },
                { "2024-11-15", new List<Sesion>
                    {
                        new Sesion { Hora = "16:45", Sala = "5", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "19:00", Sala = "3", EsISense = true, EsVOSE = false },
                        new Sesion { Hora = "21:30", Sala = "8", EsISense = true, EsVOSE = true },
                        new Sesion { Hora = "23:45", Sala = "6", EsISense = false, EsVOSE = true }
                    }
                },
                { "2024-11-16", new List<Sesion>
                    {
                        new Sesion { Hora = "13:30", Sala = "10", EsISense = true, EsVOSE = false },
                        new Sesion { Hora = "16:00", Sala = "8", EsISense = false, EsVOSE = true },
                        new Sesion { Hora = "19:15", Sala = "5", EsISense = true, EsVOSE = false },
                        new Sesion { Hora = "22:20", Sala = "3", EsISense = false, EsVOSE = true }
                    }
                },
                { "2024-11-17", new List<Sesion>
                    {
                        new Sesion { Hora = "14:00", Sala = "7", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "17:20", Sala = "9", EsISense = true, EsVOSE = true },
                        new Sesion { Hora = "20:30", Sala = "2", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "23:00", Sala = "10", EsISense = true, EsVOSE = true }
                    }
                }
                    // Añadir más fechas y sesiones si es necesario
                }
            },
            // Añadir más películas si es necesario
        }
    },
    new Cine
    {
        Id = 2,
        Nombre = "Gran Casa",
        Peliculas = new List<Pelicula>
        {
            new Pelicula
            {   
                Id = 1,
                Titulo = "Spider-Man: No Way Home",
                Descripcion = "Cuando la identidad de Spider-Man es revelada...",
                FechaEstreno = new DateTime(2021, 12, 17),
                Genero = "Sci-fi",
                Director = "Jon Watts",
                Duracion = "1 hora 55 minutos",
                Actores = "Tom Holland, Zendaya, Benedict Cumberbatch, Jacob Batalon, Willem Dafoe, Alfred Molina, Jamie Foxx",
                Calificacion = 8.2,
                Imagen = "/cine_web_app/front-end/images/Banner-Spiderman-no-way-home.jpg",
                Cartel = "/cine_web_app/front-end/images/Spiderman-No-Way-Home-Cartel.jpg",
                EdadRecomendada = 12,
                ImagenEdadRecomendada = "/cine_web_app/front-end/images/12.jpg",
                Sesiones = new Dictionary<string, List<Sesion>>
                {
                { "2024-11-11", new List<Sesion>
                    {
                        new Sesion { Hora = "17:30", Sala = "10", EsISense = true, EsVOSE = false },
                        new Sesion { Hora = "19:05", Sala = "8", EsISense = false, EsVOSE = true }
                    }
                },
                { "2024-11-12", new List<Sesion>
                    {
                        new Sesion { Hora = "16:30", Sala = "3", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "17:45", Sala = "9", EsISense = true, EsVOSE = false },
                        new Sesion { Hora = "20:50", Sala = "6", EsISense = true, EsVOSE = true },
                        new Sesion { Hora = "22:10", Sala = "2", EsISense = false, EsVOSE = true }
                    }
                },
                { "2024-11-13", new List<Sesion>
                    {
                        new Sesion { Hora = "16:00", Sala = "10", EsISense = true, EsVOSE = false },
                        new Sesion { Hora = "18:10", Sala = "8", EsISense = false, EsVOSE = true },
                        new Sesion { Hora = "20:25", Sala = "5", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "23:00", Sala = "3", EsISense = true, EsVOSE = true }
                    }
                },
                { "2024-11-14", new List<Sesion>
                    {
                        new Sesion { Hora = "14:45", Sala = "4", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "17:00", Sala = "7", EsISense = true, EsVOSE = true },
                        new Sesion { Hora = "19:30", Sala = "2", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "21:50", Sala = "10", EsISense = true, EsVOSE = false }
                    }
                },
                { "2024-11-15", new List<Sesion>
                    {
                        new Sesion { Hora = "16:45", Sala = "5", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "19:00", Sala = "3", EsISense = true, EsVOSE = false },
                        new Sesion { Hora = "21:30", Sala = "8", EsISense = true, EsVOSE = true },
                        new Sesion { Hora = "23:45", Sala = "6", EsISense = false, EsVOSE = true }
                    }
                },
                { "2024-11-16", new List<Sesion>
                    {
                        new Sesion { Hora = "13:30", Sala = "10", EsISense = true, EsVOSE = false },
                        new Sesion { Hora = "16:00", Sala = "8", EsISense = false, EsVOSE = true },
                        new Sesion { Hora = "19:15", Sala = "5", EsISense = true, EsVOSE = false },
                        new Sesion { Hora = "22:20", Sala = "3", EsISense = false, EsVOSE = true }
                    }
                },
                { "2024-11-17", new List<Sesion>
                    {
                        new Sesion { Hora = "14:00", Sala = "7", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "17:20", Sala = "9", EsISense = true, EsVOSE = true },
                        new Sesion { Hora = "20:30", Sala = "2", EsISense = false, EsVOSE = false },
                        new Sesion { Hora = "23:00", Sala = "10", EsISense = true, EsVOSE = true }
                    }
                }
                    // Añadir más fechas y sesiones si es necesario
                }
            },
        }
    }
};

// Endpoint para obtener la lista de cines (sin películas)
app.MapGet("/api/Cine/GetCines", () =>
{
    var cinesSinPeliculas = cines.Select(c => new
    {
        c.Id,
        c.Nombre
    }).ToList();

    return Results.Ok(cinesSinPeliculas);
}).WithName("GetCines");

// Endpoint para obtener un cine específico con sus películas y sesiones
app.MapGet("/api/Cine/GetCineConPeliculas", (int cineId) =>
{
    var cine = cines.FirstOrDefault(c => c.Id == cineId);
    if (cine == null)
    {
        return Results.NotFound("Cine no encontrado");
    }
    return Results.Ok(cine);
}).WithName("GetCineConPeliculas");

// Endpoint para obtener todas las películas
app.MapGet("/api/Movie/GetPeliculas", () =>
{
    return Results.Ok(cines.SelectMany(c => c.Peliculas)); // Devuelve todas las películas de todos los cines
}).WithName("GetPeliculas");

// Endpoint para obtener una película específica por ID
app.MapGet("/api/Movie/GetPeliculaById", (int id) =>
{
    var pelicula = cines.SelectMany(c => c.Peliculas).FirstOrDefault(p => p.Id == id);
    if (pelicula == null)
    {
        return Results.NotFound("Película no encontrada");
    }
    return Results.Ok(pelicula);
}).WithName("GetPeliculaById");

app.Run();
