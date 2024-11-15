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
                Sesiones = CrearSesionesSpiderMan()
            },
            new Pelicula
            {   
                Id = 2,
                Titulo = "X-Men Apocalypse",
                Descripcion = "Apocalypse, el primer y más poderoso mutante...",
                FechaEstreno = new DateTime(2016, 5, 27),
                Genero = "Sci-fi",
                Director = "Bryan Singer",
                Actores = "James McAvoy, Michael Fassbender, Jennifer Lawrence, Oscar Isaac, Nicholas Hoult, Rose Byrne, Evan Peters, Sophie Turner",
                Duracion = "2 horas 24 minutos",
                Calificacion = 7.0,
                EdadRecomendada = 12,
                ImagenEdadRecomendada = "/cine_web_app/front-end/images/12.jpg",
                Imagen = "/cine_web_app/front-end/images/X-Men_Apocalypse_Banner.jpg",
                Cartel = "/cine_web_app/front-end/images/X-MEN_Apocalypse.jpg",
                Sesiones = CrearSesionesXMen()
            },
            new Pelicula
            {   
                Id = 3,
                Titulo = "Venom: El Último Baile",
                Descripcion = "Eddie Brock intenta reavivar su carrera entrevistando al asesino en serie Cletus Kasady, quien se convierte en el anfitrión del simbionte Carnage y escapa de prisión después de una fallida ejecución.",
                FechaEstreno = new DateTime(2024, 10, 25),
                Genero = "Sci-fi",
                Director = "Kelly Marcel",
                Actores = "Rhys Ifans, Chiwetel Ejiofor, Tom Hardy, Stephen Graham, Alanna Ubach, Juno Temple, Clark Backo, Peggy Lu",
                Duracion = "1 hora 48 minutos",
                Calificacion = 6.4,
                Imagen = "/cine_web_app/front-end/images/Venom-3-Banner.jpg",
                Cartel = "/cine_web_app/front-end/images/Venom_3.jpg",
                EdadRecomendada = 12, 
                ImagenEdadRecomendada = "/cine_web_app/front-end/images/12.jpg",
                Sesiones = CrearSesionesVenom()
            },
            new Pelicula
            {
                Id = 4,    
                Titulo = "Terrifier 3",
                Descripcion = "Este año la Navidad llega antes. El payaso Art desata el caos entre los desprevenidos habitantes del condado de Miles...",
                FechaEstreno = new DateTime(2024, 10, 31),
                Genero = "Terror",
                Duracion = "2 horas 5 minutos",
                Calificacion = 7.5,
                Director = "Damien Leone",
                Actores = "Felissa Rose, Samantha Scaffidi, David Howard Thornton, Lauren LaVera, Chris Jericho, Elliott Fullam",
                Imagen = "/cine_web_app/front-end/images/banner-terrifier-3.jpg", // Ruta del banner
                Cartel = "/cine_web_app/front-end/images/terrifier-3.jpg", // Ruta del cartel
                EdadRecomendada = 18, // Marcado como para mayores de 18 años
                ImagenEdadRecomendada = "/cine_web_app/front-end/images/18.jpg",
                Sesiones = CrearSesionesTerrifier()
            },
             new Pelicula
            {
                Id = 5,
                Titulo = "The Batman",
                Descripcion = "Bruce Wayne, en sus primeros años como el Caballero Oscuro, enfrenta a un asesino en serie conocido como el Riddler. Mientras investiga los crímenes, descubre secretos oscuros sobre su familia y la corrupción en Gotham. Con la ayuda de Catwoman, busca hacer justicia en una ciudad sumida en el caos.",
                FechaEstreno = new DateTime(2022, 3, 4),
                Genero = "Acción, Crimen, Drama",
                Director = "Matt Reeves",
                Actores = "Robert Pattinson, Zoë Kravitz, Colin Farrell",
                Calificacion = 7.9,
                Imagen = "/cine_web_app/front-end/images/Banner-The-Batman.jpg",
                Cartel = "/cine_web_app/front-end/images/The-Batman-Cartel.jpg",
                EdadRecomendada = 12, 
                ImagenEdadRecomendada = "/cine_web_app/front-end/images/12.jpg",
                Sesiones = CrearSesionesBatman()
            },
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
                Sesiones = CrearSesionesSpiderMan()
            },
            new Pelicula
            {   
                Id = 2,
                Titulo = "X-Men Apocalypse",
                Descripcion = "Apocalypse, el primer y más poderoso mutante...",
                FechaEstreno = new DateTime(2016, 5, 27),
                Genero = "Sci-fi",
                Director = "Bryan Singer",
                Actores = "James McAvoy, Michael Fassbender, Jennifer Lawrence, Oscar Isaac, Nicholas Hoult, Rose Byrne, Evan Peters, Sophie Turner",
                Duracion = "2 horas 24 minutos",
                Calificacion = 7.0,
                EdadRecomendada = 12,
                ImagenEdadRecomendada = "/cine_web_app/front-end/images/12.jpg",
                Imagen = "/cine_web_app/front-end/images/X-Men_Apocalypse_Banner.jpg",
                Cartel = "/cine_web_app/front-end/images/X-MEN_Apocalypse.jpg",
                Sesiones = CrearSesionesXMen()
            },
            new Pelicula
            {   
                Id = 3,
                Titulo = "Venom: El Último Baile",
                Descripcion = "Eddie Brock intenta reavivar su carrera entrevistando al asesino en serie Cletus Kasady, quien se convierte en el anfitrión del simbionte Carnage y escapa de prisión después de una fallida ejecución.",
                FechaEstreno = new DateTime(2024, 10, 25),
                Genero = "Sci-fi",
                Director = "Kelly Marcel",
                Actores = "Rhys Ifans, Chiwetel Ejiofor, Tom Hardy, Stephen Graham, Alanna Ubach, Juno Temple, Clark Backo, Peggy Lu",
                Duracion = "1 hora 48 minutos",
                Calificacion = 6.4,
                Imagen = "/cine_web_app/front-end/images/Venom-3-Banner.jpg",
                Cartel = "/cine_web_app/front-end/images/Venom_3.jpg",
                EdadRecomendada = 12, 
                ImagenEdadRecomendada = "/cine_web_app/front-end/images/12.jpg",
                Sesiones = CrearSesionesVenom()
            },
            new Pelicula
            {
                Id = 4,    
                Titulo = "Terrifier 3",
                Descripcion = "Este año la Navidad llega antes. El payaso Art desata el caos entre los desprevenidos habitantes del condado de Miles...",
                FechaEstreno = new DateTime(2024, 10, 31),
                Genero = "Terror",
                Duracion = "2 horas 5 minutos",
                Calificacion = 7.5,
                Director = "Damien Leone",
                Actores = "Felissa Rose, Samantha Scaffidi, David Howard Thornton, Lauren LaVera, Chris Jericho, Elliott Fullam",
                Imagen = "/cine_web_app/front-end/images/banner-terrifier-3.jpg", // Ruta del banner
                Cartel = "/cine_web_app/front-end/images/terrifier-3.jpg", // Ruta del cartel
                EdadRecomendada = 18, // Marcado como para mayores de 18 años
                ImagenEdadRecomendada = "/cine_web_app/front-end/images/18.jpg",
                Sesiones = CrearSesionesTerrifier()
            },
            new Pelicula
            {
                Id = 5,
                Titulo = "The Batman",
                Descripcion = "Bruce Wayne, en sus primeros años como el Caballero Oscuro, enfrenta a un asesino en serie conocido como el Riddler. Mientras investiga los crímenes, descubre secretos oscuros sobre su familia y la corrupción en Gotham. Con la ayuda de Catwoman, busca hacer justicia en una ciudad sumida en el caos.",
                FechaEstreno = new DateTime(2022, 3, 4),
                Genero = "Acción, Crimen, Drama",
                Director = "Matt Reeves",
                Actores = "Robert Pattinson, Zoë Kravitz, Colin Farrell",
                Calificacion = 7.9,
                Imagen = "/cine_web_app/front-end/images/Banner-The-Batman.jpg",
                Cartel = "/cine_web_app/front-end/images/The-Batman-Cartel.jpg",
                EdadRecomendada = 12, 
                ImagenEdadRecomendada = "/cine_web_app/front-end/images/12.jpg",
                Sesiones = CrearSesionesBatman()
            },
        }
    }
};

// Métodos de creación de sesiones para cada película
Dictionary<string, List<Sesion>> CrearSesionesSpiderMan()
{
    return new Dictionary<string, List<Sesion>>
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
    };
}

Dictionary<string, List<Sesion>> CrearSesionesXMen()
{
    return new Dictionary<string, List<Sesion>>
    {
        { "2024-11-11", new List<Sesion>
            {
                new Sesion { Hora = "15:00", Sala = "1", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "17:30", Sala = "3", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "20:00", Sala = "2", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "22:30", Sala = "4", EsISense = true, EsVOSE = true }
            }
        },
        { "2024-11-12", new List<Sesion>
            {
                new Sesion { Hora = "16:00", Sala = "7", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "18:45", Sala = "9", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "21:00", Sala = "5", EsISense = true, EsVOSE = true }
            }
        },
        { "2024-11-13", new List<Sesion>
            {
                new Sesion { Hora = "14:30", Sala = "6", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "17:00", Sala = "3", EsISense = true, EsVOSE = true },
                new Sesion { Hora = "20:00", Sala = "10", EsISense = false, EsVOSE = false }
            }
        },
        { "2024-11-14", new List<Sesion>
            {
                new Sesion { Hora = "15:15", Sala = "4", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "18:10", Sala = "7", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "20:30", Sala = "2", EsISense = true, EsVOSE = true }
            }
        },
        { "2024-11-15", new List<Sesion>
            {
                new Sesion { Hora = "16:00", Sala = "5", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "19:00", Sala = "3", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "21:45", Sala = "9", EsISense = false, EsVOSE = true }
            }
        },
        { "2024-11-16", new List<Sesion>
            {
                new Sesion { Hora = "14:00", Sala = "8", EsISense = true, EsVOSE = true },
                new Sesion { Hora = "17:00", Sala = "6", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "20:00", Sala = "4", EsISense = true, EsVOSE = false }
            }
        },
        { "2024-11-17", new List<Sesion>
            {
                new Sesion { Hora = "15:30", Sala = "1", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "18:30", Sala = "3", EsISense = true, EsVOSE = true },
                new Sesion { Hora = "21:15", Sala = "2", EsISense = true, EsVOSE = false }
            }
        }
    };
}

// Método de creación de sesiones para Venom
Dictionary<string, List<Sesion>> CrearSesionesVenom()
{
    return new Dictionary<string, List<Sesion>>
    {
        { "2024-11-11", new List<Sesion>
            {
                new Sesion { Hora = "14:00", Sala = "2", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "17:15", Sala = "5", EsISense = false, EsVOSE = true }
            }
        },
        { "2024-11-12", new List<Sesion>
            {
                new Sesion { Hora = "13:30", Sala = "3", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "16:45", Sala = "7", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "20:00", Sala = "6", EsISense = true, EsVOSE = true }
            }
        },
        { "2024-11-13", new List<Sesion>
            {
                new Sesion { Hora = "15:00", Sala = "8", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "18:20", Sala = "4", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "21:30", Sala = "9", EsISense = false, EsVOSE = false }
            }
        },
        { "2024-11-14", new List<Sesion>
            {
                new Sesion { Hora = "13:45", Sala = "2", EsISense = true, EsVOSE = true },
                new Sesion { Hora = "17:00", Sala = "6", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "20:15", Sala = "10", EsISense = true, EsVOSE = false }
            }
        },
        { "2024-11-15", new List<Sesion>
            {
                new Sesion { Hora = "16:00", Sala = "3", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "19:30", Sala = "7", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "22:00", Sala = "4", EsISense = true, EsVOSE = true }
            }
        },
        { "2024-11-16", new List<Sesion>
            {
                new Sesion { Hora = "12:00", Sala = "1", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "15:30", Sala = "8", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "18:45", Sala = "6", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "21:15", Sala = "2", EsISense = true, EsVOSE = false }
            }
        },
        { "2024-11-17", new List<Sesion>
            {
                new Sesion { Hora = "14:30", Sala = "5", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "17:45", Sala = "9", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "20:00", Sala = "3", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "22:30", Sala = "10", EsISense = true, EsVOSE = true }
            }
        }
    };
}

// Método de creación de sesiones para Terrifier 3
Dictionary<string, List<Sesion>> CrearSesionesTerrifier()
{
    return new Dictionary<string, List<Sesion>>
    {
        { "2024-11-11", new List<Sesion>
            {
                new Sesion { Hora = "15:00", Sala = "4", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "18:30", Sala = "6", EsISense = true, EsVOSE = false }
            }
        },
        { "2024-11-12", new List<Sesion>
            {
                new Sesion { Hora = "14:45", Sala = "5", EsISense = true, EsVOSE = true },
                new Sesion { Hora = "17:50", Sala = "2", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "21:10", Sala = "8", EsISense = true, EsVOSE = false }
            }
        },
        { "2024-11-13", new List<Sesion>
            {
                new Sesion { Hora = "16:20", Sala = "3", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "19:40", Sala = "7", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "22:30", Sala = "1", EsISense = false, EsVOSE = true }
            }
        },
        { "2024-11-14", new List<Sesion>
            {
                new Sesion { Hora = "15:15", Sala = "10", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "18:45", Sala = "4", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "21:30", Sala = "9", EsISense = true, EsVOSE = true }
            }
        },
        { "2024-11-15", new List<Sesion>
            {
                new Sesion { Hora = "14:30", Sala = "2", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "17:00", Sala = "8", EsISense = true, EsVOSE = true },
                new Sesion { Hora = "20:20", Sala = "6", EsISense = false, EsVOSE = false }
            }
        },
        { "2024-11-16", new List<Sesion>
            {
                new Sesion { Hora = "13:50", Sala = "7", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "16:45", Sala = "5", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "19:10", Sala = "3", EsISense = true, EsVOSE = true },
                new Sesion { Hora = "22:00", Sala = "1", EsISense = false, EsVOSE = false }
            }
        },
        { "2024-11-17", new List<Sesion>
            {
                new Sesion { Hora = "15:00", Sala = "9", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "18:30", Sala = "2", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "21:45", Sala = "6", EsISense = true, EsVOSE = true }
            }
        }
    };
}

// Método de creación de sesiones para The Batman
Dictionary<string, List<Sesion>> CrearSesionesBatman()
{
    return new Dictionary<string, List<Sesion>>
    {
        { "2024-11-11", new List<Sesion>
            {
                new Sesion { Hora = "16:00", Sala = "5", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "19:00", Sala = "3", EsISense = false, EsVOSE = true }
            }
        },
        { "2024-11-12", new List<Sesion>
            {
                new Sesion { Hora = "15:30", Sala = "7", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "18:45", Sala = "2", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "21:10", Sala = "9", EsISense = false, EsVOSE = true }
            }
        },
        { "2024-11-13", new List<Sesion>
            {
                new Sesion { Hora = "14:20", Sala = "4", EsISense = true, EsVOSE = true },
                new Sesion { Hora = "17:40", Sala = "6", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "20:50", Sala = "10", EsISense = true, EsVOSE = false }
            }
        },
        { "2024-11-14", new List<Sesion>
            {
                new Sesion { Hora = "16:15", Sala = "8", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "19:30", Sala = "5", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "22:45", Sala = "1", EsISense = false, EsVOSE = true }
            }
        },
        { "2024-11-15", new List<Sesion>
            {
                new Sesion { Hora = "13:00", Sala = "3", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "16:30", Sala = "7", EsISense = false, EsVOSE = true },
                new Sesion { Hora = "20:00", Sala = "4", EsISense = true, EsVOSE = false }
            }
        },
        { "2024-11-16", new List<Sesion>
            {
                new Sesion { Hora = "12:30", Sala = "2", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "15:45", Sala = "8", EsISense = true, EsVOSE = true },
                new Sesion { Hora = "19:00", Sala = "6", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "22:10", Sala = "9", EsISense = false, EsVOSE = true }
            }
        },
        { "2024-11-17", new List<Sesion>
            {
                new Sesion { Hora = "14:00", Sala = "5", EsISense = true, EsVOSE = true },
                new Sesion { Hora = "17:20", Sala = "3", EsISense = false, EsVOSE = false },
                new Sesion { Hora = "20:40", Sala = "7", EsISense = true, EsVOSE = false },
                new Sesion { Hora = "23:00", Sala = "10", EsISense = false, EsVOSE = true }
            }
        }
    };
}

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

app.MapGet("/api/Cine/GetSeatSelectionInfo", (int cineId, int movieId, string sessionDate, string sessionTime) =>
{
    var cine = cines.FirstOrDefault(c => c.Id == cineId);
    if (cine == null)
    {
        return Results.NotFound("Cine no encontrado");
    }

    var pelicula = cine.Peliculas.FirstOrDefault(p => p.Id == movieId);
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

    // Devolver la información para la selección de asientos
    var seatSelectionInfo = new
    {
    MovieTitle = pelicula.Titulo,
    CineName = cine.Nombre,
    SessionDate = sessionDate,
    SessionTime = sesion.Hora,
    Room = sesion.Sala,
    EsISense = sesion.EsISense,
    EsVOSE = sesion.EsVOSE,
    BannerImage = pelicula.Imagen // Asegúrate de que `Imagen` sea la propiedad del banner en la clase `Pelicula`
    };

    return Results.Ok(seatSelectionInfo);
}).WithName("GetSeatSelectionInfo");

app.Run();
