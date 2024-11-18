using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using cine_web_app.back_end.Models;

namespace cine_web_app.back_end.Services
{
    public class CineService
    {
        private readonly List<Cine> _cines;

        public CineService()
        {
            _cines = InicializarCines();
        }

        // Método para obtener la lista de cines
        public List<Cine> ObtenerCines()
        {
            return _cines;
        }

        // Método para obtener todas las películas sin duplicados
        public List<Pelicula> ObtenerTodasLasPeliculas()
        {
            return _cines.SelectMany(c => c.Peliculas)
                         .GroupBy(p => p.Id) // Agrupa por ID
                         .Select(g => g.First()) // Selecciona solo una película por ID
                         .ToList();
        }

        // Método para obtener películas en cartelera sin duplicados
        public List<Pelicula> ObtenerPeliculasEnCartelera()
        {
            return _cines.SelectMany(c => c.Peliculas)
                         .Where(p => p.EnCartelera)
                         .GroupBy(p => p.Id)
                         .Select(g => g.First())
                         .ToList();
        }

        // Método para obtener películas en venta anticipada sin duplicados
        public List<Pelicula> ObtenerPeliculasEnVentaAnticipada()
        {
            return _cines.SelectMany(c => c.Peliculas)
                         .Where(p => p.EnVentaAnticipada)
                         .GroupBy(p => p.Id)
                         .Select(g => g.First())
                         .ToList();
        }

        // Método para obtener películas próximas sin duplicados
        public List<Pelicula> ObtenerPeliculasProximas()
        {
            return _cines.SelectMany(c => c.Peliculas)
                         .Where(p => p.FechaEstreno > DateTime.Now && !p.EnCartelera && !p.EnVentaAnticipada)
                         .GroupBy(p => p.Id)
                         .Select(g => g.First())
                         .ToList();
        }

        // Método para obtener un cine por ID
        public Cine ObtenerCinePorId(int id)
        {
            return _cines.FirstOrDefault(c => c.Id == id);
        }

        // Método para obtener una película específica por cineId y peliculaId
        public Pelicula ObtenerPelicula(int cineId, int peliculaId)
        {
            var cine = ObtenerCinePorId(cineId);
            return cine?.Peliculas.FirstOrDefault(p => p.Id == peliculaId);
        }
        // Inicializar la lista de cines
        private List<Cine> InicializarCines()
        {
            return new List<Cine>
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
                            Sesiones = CrearSesionesSpiderMan(),
                            EnCartelera = false, // Ya no está en cartelera
                            EnVentaAnticipada = false // Tampoco está en venta anticipada
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
                            Sesiones = CrearSesionesXMen(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = true // Si está en venta anticipada
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
                            Sesiones = CrearSesionesVenom(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = false // No está en venta anticipada
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
                            Sesiones = CrearSesionesTerrifier(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = false // No está en venta anticipada
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
                            Sesiones = CrearSesionesBatman(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = true // No está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 6,
                            Titulo = "Gladiator II",
                            Descripcion = "Años después de presenciar la muerte del admirado héroe Máximo...",
                            FechaEstreno = new DateTime(2024, 11, 15),
                            Genero = "Acción, Aventura, Drama",
                            Director = "Ridley Scott",
                            Duracion = "2h 27m",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Calificacion = 8.5,
                            Imagen = "/cine_web_app/front-end/images/Banner-Gladiator-II.jpg",
                            Cartel = "/cine_web_app/front-end/images/Gladiator-II-Cartel.jpg",
                            EdadRecomendada = 16,
                            ImagenEdadRecomendada = "/cine_web_app/front-end/images/16.jpg",
                            Sesiones = CrearSesionesGladiator2(),  // Asegúrate de definir las sesiones para Gladiator II
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 7,
                            Titulo = "Red One",
                            Descripcion = "Tras el secuestro de Papá Noel, nombre en clave: RED ONE, el Jefe de Seguridad del Polo Norte (Dwayne Johnson) debe formar equipo con el cazarrecompensas más infame del mundo (Chris Evans) en una misión trotamundos llena de acción para salvar la Navidad. No te pierdas #RedOne, protagonizada por Dwayne Johnson y Chris Evans. Disfruta de la película a partir del 6 de noviembre solo en cines.",
                            FechaEstreno = new DateTime(2024, 11, 6),
                            Genero = "Acción, Comedia, Aventura",
                            Director = "Jake Kasdan",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Duracion = "2h 3m",
                            Calificacion = 8.5,
                            Imagen = "/cine_web_app/front-end/images/Banner-Red-One.jpg",
                            Cartel = "/cine_web_app/front-end/images/Red-One-Cartel.jpg",
                            EdadRecomendada = 7,
                            ImagenEdadRecomendada = "/cine_web_app/front-end/images/7.jpg",
                            Sesiones = CrearSesionesRedOne(), // Asegúrate de definir las sesiones para Red One
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 8, // Ajusta el ID según corresponda
                            Titulo = "Robot Salvaje",
                            Descripcion = "La película sigue el épico viaje de un robot -la unidad 7134 de Roz, 'Roz' para abreviar- que naufraga en una isla deshabitada y debe aprender a adaptarse al duro entorno, entablando gradualmente relaciones con los animales de la isla y convirtiéndose en padre adoptivo de un gosling huérfano.",
                            FechaEstreno = new DateTime(2024, 10, 11),
                            Genero = "Aventura, Animación",
                            Director = "Chris Sanders",
                            Actores = "Bill Nighy, Lupita Nyong'o, Stephanie Hsu, Mark Hamill, Ving Rhames, Catherine O'Hara, Matt Berry, Pedro Pascal, Kit Connor",
                            Duracion = "1h 41m",
                            Calificacion = 9.0, // Ajusta esta calificación según sea necesario
                            Imagen = "/cine_web_app/front-end/images/Banner-Robot-Salvaje.jpg", // Cambia el nombre si necesitas una imagen específica
                            Cartel = "/cine_web_app/front-end/images/Robot-Salvaje-Cartel.jpg", // Cambia el nombre si necesitas una imagen específica
                            EdadRecomendada = 0, // Edad para todos los públicos
                            ImagenEdadRecomendada = "/cine_web_app/front-end/images/Todos.jpg", // Cambia el nombre si necesitas una imagen específica
                            Sesiones = CrearSesionesRobotSalvaje(), // Asegúrate de definir las sesiones para Robot Salvaje
                            EnCartelera = true,
                            EnVentaAnticipada = true
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
                            Sesiones = CrearSesionesTerrifier(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = false // No está en venta anticipada
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
                            Sesiones = CrearSesionesBatman(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = true // No está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 6,
                            Titulo = "Gladiator II",
                            Descripcion = "Años después de presenciar la muerte del admirado héroe Máximo...",
                            FechaEstreno = new DateTime(2024, 11, 15),
                            Genero = "Acción, Aventura, Drama",
                            Director = "Ridley Scott",
                            Duracion = "2h 27m",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Calificacion = 8.5,
                            Imagen = "/cine_web_app/front-end/images/Banner-Gladiator-II.jpg",
                            Cartel = "/cine_web_app/front-end/images/Gladiator-II-Cartel.jpg",
                            EdadRecomendada = 16,
                            ImagenEdadRecomendada = "/cine_web_app/front-end/images/16.jpg",
                            Sesiones = CrearSesionesGladiator2(),  // Asegúrate de definir las sesiones para Gladiator II
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 7,
                            Titulo = "Red One",
                            Descripcion = "Tras el secuestro de Papá Noel, nombre en clave: RED ONE, el Jefe de Seguridad del Polo Norte (Dwayne Johnson) debe formar equipo con el cazarrecompensas más infame del mundo (Chris Evans) en una misión trotamundos llena de acción para salvar la Navidad. No te pierdas #RedOne, protagonizada por Dwayne Johnson y Chris Evans. Disfruta de la película a partir del 6 de noviembre solo en cines.",
                            FechaEstreno = new DateTime(2024, 11, 6),
                            Genero = "Acción, Comedia, Aventura",
                            Director = "Jake Kasdan",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Duracion = "2h 3m",
                            Calificacion = 8.5,
                            Imagen = "/cine_web_app/front-end/images/Banner-Red-One.jpg",
                            Cartel = "/cine_web_app/front-end/images/Red-One-Cartel.jpg",
                            EdadRecomendada = 7,
                            ImagenEdadRecomendada = "/cine_web_app/front-end/images/7.jpg",
                            Sesiones = CrearSesionesRedOne(), // Asegúrate de definir las sesiones para Red One
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 8, // Ajusta el ID según corresponda
                            Titulo = "Robot Salvaje",
                            Descripcion = "La película sigue el épico viaje de un robot -la unidad 7134 de Roz, 'Roz' para abreviar- que naufraga en una isla deshabitada y debe aprender a adaptarse al duro entorno, entablando gradualmente relaciones con los animales de la isla y convirtiéndose en padre adoptivo de un gosling huérfano.",
                            FechaEstreno = new DateTime(2024, 10, 11),
                            Genero = "Aventura, Animación",
                            Director = "Chris Sanders",
                            Actores = "Bill Nighy, Lupita Nyong'o, Stephanie Hsu, Mark Hamill, Ving Rhames, Catherine O'Hara, Matt Berry, Pedro Pascal, Kit Connor",
                            Duracion = "1h 41m",
                            Calificacion = 9.0, // Ajusta esta calificación según sea necesario
                            Imagen = "/cine_web_app/front-end/images/Banner-Robot-Salvaje.jpg", // Cambia el nombre si necesitas una imagen específica
                            Cartel = "/cine_web_app/front-end/images/Robot-Salvaje-Cartel.jpg", // Cambia el nombre si necesitas una imagen específica
                            EdadRecomendada = 0, // Edad para todos los públicos
                            ImagenEdadRecomendada = "/cine_web_app/front-end/images/Todos.jpg", // Cambia el nombre si necesitas una imagen específica
                            Sesiones = CrearSesionesRobotSalvaje(), // Asegúrate de definir las sesiones para Robot Salvaje
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                    }
                }
            };
        }
        
        private Dictionary<string, Dictionary<string, List<Sesion>>>  CrearSesionesRedOne()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
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
                        }
                        // Agrega más días y sesiones para Gran Casa según sea necesario
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Hora = "17:00", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Hora = "19:30", Sala = "2", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Hora = "16:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "20:00", Sala = "3", EsISense = false, EsVOSE = true }
                            }
                        }
                        // Agrega más días y sesiones para Puerto Venecia según sea necesario
                    }
                }
            };
        }


       private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesXMen()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
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
                        }
                        // Agrega más fechas y sesiones para Gran Casa según sea necesario
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
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
                        }
                        // Agrega más fechas y sesiones para Puerto Venecia según sea necesario
                    }
                }
            };
        }


        // Método de creación de sesiones para Venom
        private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesVenom()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Hora = "14:00", Sala = "2", EsISense = false, EsVOSE = false },
                                new Sesion { Hora = "17:15", Sala = "5", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Hora = "13:30", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Hora = "16:45", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "20:00", Sala = "6", EsISense = true, EsVOSE = true }
                            }
                        }
                        // Agrega más fechas para "Gran Casa" según sea necesario
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Hora = "15:00", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "18:20", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "21:30", Sala = "9", EsISense = false, EsVOSE = false }
                            }
                        }
                        // Agrega más fechas para "Puerto Venecia" según sea necesario
                    }
                }
            };
        }



        // Método de creación de sesiones para Terrifier 3
       private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesTerrifier()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
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
                        }
                        // Agrega más días y sesiones para Gran Casa
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
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
                        }
                        // Agrega más días y sesiones para Puerto Venecia
                    }
                }
            };
        }


        // Método de creación de sesiones para The Batman
        private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesBatman()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
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
                        }
                        // Agrega más días y sesiones para Gran Casa según sea necesario
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
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
                        }
                        // Agrega más días y sesiones para Puerto Venecia según sea necesario
                    }
                }
            };
        }
        private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesGladiator2()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
                    {
                        // Lunes 11 de noviembre
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Hora = "14:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "16:30", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "19:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "21:30", Sala = "3", EsISense = false, EsVOSE = true }
                            }
                        },
                        // Martes 12 de noviembre
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Hora = "15:00", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "17:30", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "20:00", Sala = "5", EsISense = false, EsVOSE = false },
                                new Sesion { Hora = "22:30", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        },
                        // Miércoles 13 de noviembre
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Hora = "13:45", Sala = "1", EsISense = false, EsVOSE = false },
                                new Sesion { Hora = "16:15", Sala = "3", EsISense = true, EsVOSE = true },
                                new Sesion { Hora = "18:45", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "21:00", Sala = "8", EsISense = true, EsVOSE = false }
                            }
                        },
                        // Jueves 14 de noviembre
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Hora = "14:30", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "17:00", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "19:30", Sala = "2", EsISense = false, EsVOSE = false },
                                new Sesion { Hora = "22:00", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        },
                        // Viernes 15 de noviembre
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Hora = "16:30", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "19:00", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "21:30", Sala = "4", EsISense = false, EsVOSE = false },
                                new Sesion { Hora = "23:45", Sala = "6", EsISense = true, EsVOSE = true }
                            }
                        },
                        // Sábado 16 de noviembre
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Hora = "13:30", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Hora = "16:00", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "18:30", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "21:00", Sala = "2", EsISense = true, EsVOSE = true }
                            }
                        },
                        // Domingo 17 de noviembre
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Hora = "15:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "17:30", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "20:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "22:30", Sala = "6", EsISense = false, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Hora = "14:00", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "16:30", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "19:00", Sala = "2", EsISense = true, EsVOSE = true },
                                new Sesion { Hora = "21:30", Sala = "3", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Hora = "15:30", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "18:00", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "20:30", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "23:00", Sala = "7", EsISense = false, EsVOSE = true }
                            }
                        },
                        // Añadir más días y sesiones para Puerto Venecia según sea necesario.
                    }
                }
            };
        }
        private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesSpiderMan()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
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
                        }
                        // Agrega más días y sesiones para Gran Casa según sea necesario
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Hora = "17:00", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Hora = "19:30", Sala = "2", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Hora = "16:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "20:00", Sala = "3", EsISense = false, EsVOSE = true }
                            }
                        }
                        // Agrega más días y sesiones para Puerto Venecia según sea necesario
                    }
                }
            };
        }
        private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesRobotSalvaje()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-10-12", new List<Sesion>
                            {
                                new Sesion { Hora = "15:00", Sala = "1", EsISense = false, EsVOSE = false },
                                new Sesion { Hora = "17:15", Sala = "2", EsISense = true, EsVOSE = true },
                                new Sesion { Hora = "19:30", Sala = "4", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-10-13", new List<Sesion>
                            {
                                new Sesion { Hora = "14:30", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "16:45", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "19:00", Sala = "6", EsISense = true, EsVOSE = true }
                            }
                        }
                        // Agrega más días y sesiones para Gran Casa según sea necesario
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-10-12", new List<Sesion>
                            {
                                new Sesion { Hora = "16:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Hora = "18:30", Sala = "4", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-10-13", new List<Sesion>
                            {
                                new Sesion { Hora = "15:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Hora = "17:30", Sala = "6", EsISense = false, EsVOSE = true }
                            }
                        }
                        // Agrega más días y sesiones para Puerto Venecia según sea necesario
                    }
                }
            };
        }

    }
}