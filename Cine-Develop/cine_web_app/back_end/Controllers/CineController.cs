using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using cine_web_app.back_end.Models;
using System;

namespace cine_web_app.back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CineController : ControllerBase
    {
        // Lista de cines que contiene películas y sesiones
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
                        Actores = "Tom Holland, Zendaya, Benedict Cumberbatch, Jacob Batalon, Willem Dafoe, Alfred Molina, Jamie Foxx",
                        Calificacion = 8.2,
                        Imagen = "/cine_web_app/front-end/images/Banner-Spiderman-no-way-home.jpg",
                        Cartel = "/cine_web_app/front-end/images/Spiderman-No-Way-Home-Cartel.jpg",
                        Sesiones = new Dictionary<string, List<Sesion>>
                        {
                            { "2024-11-11", new List<Sesion>
                                {
                                    new Sesion { Hora = "16:30", Sala = "10", EsISense = true, EsVOSE = false },
                                    new Sesion { Hora = "18:05", Sala = "8", EsISense = false, EsVOSE = true },
                                    new Sesion { Hora = "20:15", Sala = "7", EsISense = false, EsVOSE = false },
                                    new Sesion { Hora = "22:30", Sala = "5", EsISense = true, EsVOSE = true }
                                }
                            },
                            // ... (agrega más fechas y sesiones si lo deseas)
                        }
                    },
                    // Puedes agregar más películas si es necesario
                }
            },
            new Cine
            {
                Id = 2,
                Nombre = "Cinesa",
                Peliculas = new List<Pelicula>
                {
                    new Pelicula
                    {
                        Id = 2,
                        Titulo = "X-Men Apocalypse",
                        Descripcion = "Apocalypse, el primer y más poderoso mutante, despierta tras miles de años y, decepcionado con el mundo actual, decide acabar con la humanidad para instaurar un nuevo orden. Reuniendo a un grupo de mutantes poderosos, incluidos Magneto, Psylocke, Storm y Angel, planea destruir el mundo como lo conocemos. ",
                        FechaEstreno = new DateTime(2016, 5, 27),
                        Genero = "Sci-fi",
                        Director = "Bryan Singer",
                        Actores = "James McAvoy, Michael Fassbender, Jennifer Lawrence",
                        Calificacion = 7.0,
                        Imagen = "/cine_web_app/front-end/images/X-Men_Apocalypse_Banner.jpg",
                        Cartel = "/cine_web_app/front-end/images/X-MEN_Apocalypse.jpg",
                        Sesiones = new Dictionary<string, List<Sesion>>
                        {
                            { "2024-11-12", new List<Sesion>
                                {
                                    new Sesion { Hora = "15:00", Sala = "1", EsISense = true, EsVOSE = false },
                                    new Sesion { Hora = "17:30", Sala = "3", EsISense = false, EsVOSE = true }
                                }
                            },
                            // ... (agrega más fechas y sesiones si lo deseas)
                        }
                    },
                    // Puedes agregar más películas si es necesario
                }
            },
            new Cine
            {
                Id = 3,
                Nombre = "Torre Outlet",
                Peliculas = new List<Pelicula>
                {
                    new Pelicula
                    {
                        Id = 3,
                        Titulo = "Venom: El Último Baile",
                        Descripcion = "Eddie Brock intenta reavivar su carrera entrevistando al asesino en serie Cletus Kasady, quien se convierte en el anfitrión del simbionte Carnage y escapa de prisión después de una fallida ejecución.",
                        FechaEstreno = new DateTime(2024, 10, 25),
                        Genero = "Acción, Aventura, Ciencia ficción",
                        Director = "Kelly Marcel",
                        Actores = "Rhys Ifans, Chiwetel Ejiofor, Tom Hardy",
                        Calificacion = 6.4,
                        Imagen = "/cine_web_app/front-end/images/Venom-3-Banner.jpg",
                        Cartel = "/cine_web_app/front-end/images/Venom_3.jpg",
                        Sesiones = new Dictionary<string, List<Sesion>>
                        {
                            { "2024-11-13", new List<Sesion>
                                {
                                    new Sesion { Hora = "14:30", Sala = "6", EsISense = false, EsVOSE = false },
                                    new Sesion { Hora = "17:00", Sala = "3", EsISense = true, EsVOSE = true }
                                }
                            },
                            // ... (agrega más fechas y sesiones si lo deseas)
                        }
                    },
                    // Puedes agregar más películas si es necesario
                }
            },
            new Cine
            {
                Id = 4,
                Nombre = "Casco Antiguo",
                Peliculas = new List<Pelicula>
                {
                    // Agrega las películas correspondientes
                }
            }
            // Puedes agregar más cines si es necesario
        };

        // Método para obtener todos los cines con sus películas y sesiones
        [HttpGet("GetCinesConPeliculas")]
        public IActionResult GetCinesConPeliculas()
        {
            return Ok(cines);
        }

        // Método para obtener la lista de cines (sin películas)
        [HttpGet("GetCines")]
        public IActionResult GetCines()
        {
            var cinesSinPeliculas = cines.Select(c => new
            {
                c.Id,
                c.Nombre
            }).ToList();

            return Ok(cinesSinPeliculas);
        }

        // Método para obtener un cine específico con sus películas y sesiones
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

        // Método para obtener una película específica en un cine específico
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


        // Método para obtener las sesiones de una película específica en un cine específico
        [HttpGet("GetPeliculaSesionesPorCine")]
        public IActionResult GetPeliculaSesionesPorCine(int cineId, int peliculaId)
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

            return Ok(pelicula.Sesiones);
        }
    }
}
