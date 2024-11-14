using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using cine_web_app.back_end.Models;

namespace cine_web_app.back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        // Declaración de la lista de cines que contiene películas
        private readonly List<Cine> cines = new List<Cine>
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
                }
            }
        };

        // Método para obtener todos los cines con sus películas
        [HttpGet("GetCinesConPeliculas")]
        public IActionResult GetCinesConPeliculas()
        {
            return Ok(cines);
        }

        // Método para obtener un cine específico con sus películas
        [HttpGet("GetCineConPeliculas")]
        public IActionResult GetCineConPeliculas(int cineId)
        {
            var cine = cines.FirstOrDefault(c => c.Id == cineId);
            if (cine == null)
            {
                return NotFound("Cine no encontrado");
            }
            return Ok(cine);
        }

        // Método para obtener una película específica en un cine
        [HttpGet("GetPeliculaPorCine")]
        public IActionResult GetPeliculaPorCine(int cineId, int peliculaId)
        {
            var cine = cines.FirstOrDefault(c => c.Id == cineId);
            if (cine == null)
            {
                return NotFound("Cine no encontrado");
            }

            var pelicula = cine.Peliculas.FirstOrDefault(p => p.Id == peliculaId);
            if (pelicula == null)
            {
                return NotFound("Película no encontrada en este cine");
            }

            return Ok(pelicula);
        }
    }
}
