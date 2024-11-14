// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using System.Linq;
// using cine_web_app.back_end.Models;
// using System;

// namespace cine_web_app.back_end.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class CineController : ControllerBase
//     {
//         // Lista de cines que contiene películas y sesiones
//         private readonly List<Cine> cines = new List<Cine>
//         {
//             new Cine
//             {
//                 Id = 1,
//                 Nombre = "Puerto Venecia",
//                 Peliculas = new List<Pelicula>
//                 {
//                     new Pelicula
//                     {
//                         Id = 1,
//                         Titulo = "Spider-Man: No Way Home",
//                         Descripcion = "Cuando la identidad de Spider-Man es revelada...",
//                         FechaEstreno = new DateTime(2021, 12, 17),
//                         Genero = "Sci-fi",
//                         Director = "Jon Watts",
//                         Actores = "Tom Holland, Zendaya, Benedict Cumberbatch, Jacob Batalon, Willem Dafoe, Alfred Molina, Jamie Foxx",
//                         Calificacion = 8.2,
//                         Imagen = "/cine_web_app/front-end/images/Banner-Spiderman-no-way-home.jpg",
//                         Cartel = "/cine_web_app/front-end/images/Spiderman-No-Way-Home-Cartel.jpg",
//                         Sesiones = new Dictionary<string, List<Sesion>>
//                         {
//                             { "2024-11-11", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "16:30", Sala = "10", EsISense = true, EsVOSE = false },
//                                     new Sesion { Hora = "18:05", Sala = "8", EsISense = false, EsVOSE = true }
//                                 }
//                             },
//                             { "2024-11-12", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "14:00", Sala = "12", EsISense = false, EsVOSE = false },
//                                     new Sesion { Hora = "17:30", Sala = "6", EsISense = true, EsVOSE = true }
//                                 }
//                             }
//                         }
//                     },
//                     new Pelicula
//                     {
//                         Id = 2,
//                         Titulo = "The Batman",
//                         Descripcion = "Bruce Wayne, en sus primeros años como el Caballero Oscuro, enfrenta a un asesino en serie conocido como el Riddler. Mientras investiga los crímenes, descubre secretos oscuros sobre su familia y la corrupción en Gotham. Con la ayuda de Catwoman, busca hacer justicia en una ciudad sumida en el caos.",
//                         FechaEstreno = new DateTime(2022, 3, 4),
//                         Genero = "Acción, Crimen, Drama",
//                         Director = "Matt Reeves",
//                         Actores = "Robert Pattinson, Zoë Kravitz, Colin Farrell",
//                         Calificacion = 7.9,
//                         Imagen = "/cine_web_app/front-end/images/Banner-The-Batman.jpg",
//                         Cartel = "/cine_web_app/front-end/images/The-Batman-Cartel.jpg",
//                         Sesiones = new Dictionary<string, List<Sesion>>
//                         {
//                             { "2024-11-11", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "19:00", Sala = "7", EsISense = false, EsVOSE = true },
//                                     new Sesion { Hora = "21:30", Sala = "9", EsISense = true, EsVOSE = false }
//                                 }
//                             },
//                             { "2024-11-12", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "15:00", Sala = "3", EsISense = false, EsVOSE = false }
//                                 }
//                             }
//                         }
//                     }
//                 }
//             },
//             new Cine
//             {
//                 Id = 2,
//                 Nombre = "Cinesa",
//                 Peliculas = new List<Pelicula>
//                 {
//                     new Pelicula
//                     {
//                         Id = 3,
//                         Titulo = "X-Men Apocalypse",
//                         Descripcion = "Apocalypse, el primer y más poderoso mutante...",
//                         FechaEstreno = new DateTime(2016, 5, 27),
//                         Genero = "Sci-fi",
//                         Director = "Bryan Singer",
//                         Actores = "James McAvoy, Michael Fassbender, Jennifer Lawrence",
//                         Calificacion = 7.0,
//                         Imagen = "/cine_web_app/front-end/images/X-Men_Apocalypse_Banner.jpg",
//                         Cartel = "/cine_web_app/front-end/images/X-MEN_Apocalypse.jpg",
//                         Sesiones = new Dictionary<string, List<Sesion>>
//                         {
//                             { "2024-11-12", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "15:00", Sala = "1", EsISense = true, EsVOSE = false },
//                                     new Sesion { Hora = "17:30", Sala = "3", EsISense = false, EsVOSE = true }
//                                 }
//                             },
//                             { "2024-11-13", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "12:45", Sala = "4", EsISense = false, EsVOSE = true }
//                                 }
//                             }
//                         }
//                     },
//                     new Pelicula
//                     {
//                         Id = 4,
//                         Titulo = "Avatar: The Way of Water",
//                         Descripcion = "Jake Sully y Neytiri enfrentan nuevos desafíos en Pandora...",
//                         FechaEstreno = new DateTime(2022, 12, 16),
//                         Genero = "Acción, Aventura, Fantasía",
//                         Director = "James Cameron",
//                         Actores = "Sam Worthington, Zoe Saldana, Sigourney Weaver",
//                         Calificacion = 7.8,
//                         Imagen = "/cine_web_app/front-end/images/Avatar2-Banner.jpg",
//                         Cartel = "/cine_web_app/front-end/images/Avatar2-Cartel.jpg",
//                         Sesiones = new Dictionary<string, List<Sesion>>
//                         {
//                             { "2024-11-12", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "18:00", Sala = "8", EsISense = true, EsVOSE = false }
//                                 }
//                             },
//                             { "2024-11-13", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "13:30", Sala = "5", EsISense = false, EsVOSE = true }
//                                 }
//                             }
//                         }
//                     }
//                 }
//             },
//             new Cine
//             {
//                 Id = 3,
//                 Nombre = "Torre Outlet",
//                 Peliculas = new List<Pelicula>
//                 {
//                     new Pelicula
//                     {
//                         Id = 5,
//                         Titulo = "Venom: El Último Baile",
//                         Descripcion = "Eddie Brock intenta reavivar su carrera...",
//                         FechaEstreno = new DateTime(2024, 10, 25),
//                         Genero = "Acción, Aventura, Ciencia ficción",
//                         Director = "Kelly Marcel",
//                         Actores = "Rhys Ifans, Chiwetel Ejiofor, Tom Hardy",
//                         Calificacion = 6.4,
//                         Imagen = "/cine_web_app/front-end/images/Venom-3-Banner.jpg",
//                         Cartel = "/cine_web_app/front-end/images/Venom_3.jpg",
//                         Sesiones = new Dictionary<string, List<Sesion>>
//                         {
//                             { "2024-11-13", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "14:30", Sala = "6", EsISense = false, EsVOSE = false }
//                                 }
//                             },
//                             { "2024-11-14", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "17:00", Sala = "3", EsISense = true, EsVOSE = true }
//                                 }
//                             }
//                         }
//                     }
//                 }
//             },
//             new Cine
//             {
//                 Id = 4,
//                 Nombre = "Casco Antiguo",
//                 Peliculas = new List<Pelicula>
//                 {
//                     new Pelicula
//                     {
//                         Id = 6,
//                         Titulo = "Guardians of the Galaxy Vol. 3",
//                         Descripcion = "Star-Lord, Rocket, Groot y otros héroes luchan...",
//                         FechaEstreno = new DateTime(2023, 5, 5),
//                         Genero = "Aventura, Ciencia ficción, Acción",
//                         Director = "James Gunn",
//                         Actores = "Chris Pratt, Zoe Saldana, Dave Bautista",
//                         Calificacion = 8.0,
//                         Imagen = "/cine_web_app/front-end/images/Guardians-3-Banner.jpg",
//                         Cartel = "/cine_web_app/front-end/images/Guardians-3-Cartel.jpg",
//                         Sesiones = new Dictionary<string, List<Sesion>>
//                         {
//                             { "2024-11-14", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "15:00", Sala = "4", EsISense = false, EsVOSE = false }
//                                 }
//                             },
//                             { "2024-11-15", new List<Sesion>
//                                 {
//                                     new Sesion { Hora = "14:00", Sala = "5", EsISense = true, EsVOSE = false }
//                                 }
//                             }
//                         }
//                     }
//                 }
//             }
//         };

//         // Métodos de la API (ya proporcionados) ...
//         [HttpGet("GetCinesConPeliculas")]
//         public IActionResult GetCinesConPeliculas()
//         {
//             return Ok(cines);
//         }

//         [HttpGet("GetCines")]
//         public IActionResult GetCines()
//         {
//             var cinesSinPeliculas = cines.Select(c => new
//             {
//                 c.Id,
//                 c.Nombre
//             }).ToList();

//             return Ok(cinesSinPeliculas);
//         }

//         [HttpGet("GetCineConPeliculas")]
//         public IActionResult GetCineConPeliculas(int cineId)
//         {
//             var cine = cines.FirstOrDefault(c => c.Id == cineId);
//             if (cine == null)
//             {
//                 return NotFound("Cine no encontrado");
//             }
//             return Ok(cine);
//         }

//         [HttpGet("GetPeliculaPorCine")]
//         public IActionResult GetPeliculaPorCine(int cineId, int peliculaId)
//         {
//             var cine = cines.FirstOrDefault(c => c.Id == cineId);
//             if (cine == null)
//             {
//                 return NotFound("Cine no encontrado");
//             }

//             var pelicula = cine.Peliculas.FirstOrDefault(p => p.Id == peliculaId);
//             if (pelicula == null)
//             {
//                 return NotFound("Película no encontrada en este cine");
//             }

//             return Ok(pelicula);
//         }

//         [HttpGet("GetPeliculaSesionesPorCine")]
//         public IActionResult GetPeliculaSesionesPorCine(int cineId, int peliculaId)
//         {
//             var cine = cines.FirstOrDefault(c => c.Id == cineId);
//             if (cine == null)
//             {
//                 return NotFound("Cine no encontrado");
//             }

//             var pelicula = cine.Peliculas.FirstOrDefault(p => p.Id == peliculaId);
//             if (pelicula == null)
//             {
//                 return NotFound("Película no encontrada en este cine");
//             }

//             return Ok(pelicula.Sesiones);
//         }
//     }
// }
