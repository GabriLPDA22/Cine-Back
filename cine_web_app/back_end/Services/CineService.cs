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
                         .Where(p => !p.EnCartelera && !p.EnVentaAnticipada)
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
                            Descripcion = "Cuando la identidad de Spider-Man es revelada, Peter Parker se enfrenta a las consecuencias que afectan a su vida personal y a sus seres queridos. Desesperado, busca la ayuda del Doctor Strange para restaurar el secreto, pero el hechizo sale mal, desatando un caos multiversal donde viejos enemigos regresan y nuevas alianzas se forman.",
                            FechaEstreno = "2021, 12, 17",
                            Genero = "acción, aventura, ciencia ficción",
                            Director = "Jon Watts",
                            Duracion = "1 hora 55 minutos",
                            Actores = "Tom Holland, Zendaya, Benedict Cumberbatch, Jacob Batalon, Willem Dafoe, Alfred Molina, Jamie Foxx",
                            Calificacion = 8.2,
                            Imagen = "/../images/Banner-Spiderman-no-way-home.jpg",
                            Cartel = "/../images/Spiderman-No-Way-Home-Cartel.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesSpiderMan(),
                            EnCartelera = false, // Ya no está en cartelera
                            EnVentaAnticipada = false, // Tampoco está en venta anticipada
                            opiniones = "Esta pelicula es la hostia 10/10, recomendadda si o si",
                            puntuacion = 4
                        },
                        new Pelicula
                        {
                            Id = 2,
                            Titulo = "X-Men Apocalypse",
                            Descripcion = "Años después de la muerte de Máximo, Lucio, ahora adulto, se enfrenta a nuevas amenazas que ponen en riesgo al Imperio Romano. Inspirado por el sacrificio del gladiador, Lucio deberá enfrentarse a la corrupción y al poder en su intento por restaurar la justicia y el honor en Roma.",
                            FechaEstreno = "2016, 5, 27",
                            Genero = "Sci-fi",
                            Director = "Bryan Singer",
                            Actores = "James McAvoy, Michael Fassbender, Jennifer Lawrence, Oscar Isaac, Nicholas Hoult, Rose Byrne, Evan Peters, Sophie Turner",
                            Duracion = "2 horas 24 minutos",
                            Calificacion = 7.0,
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Imagen = "/../images/X-Men_Apocalypse_Banner.jpg",
                            Cartel = "/../images/X-MEN_Apocalypse.jpg",
                            Sesiones = CrearSesionesXMen(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = true // Si está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 3,
                            Titulo = "Venom: El Último Baile",
                            Descripcion = "Eddie Brock intenta reavivar su carrera entrevistando al asesino en serie Cletus Kasady, quien se convierte en el anfitrión del simbionte Carnage y escapa de prisión después de una fallida ejecución.",
                            FechaEstreno = "2024, 10, 25",
                            Genero = "Sci-fi",
                            Director = "Kelly Marcel",
                            Actores = "Rhys Ifans, Chiwetel Ejiofor, Tom Hardy, Stephen Graham, Alanna Ubach, Juno Temple, Clark Backo, Peggy Lu",
                            Duracion = "1 hora 48 minutos",
                            Calificacion = 6.4,
                            Imagen = "/../images/Venom-3-Banner.jpg",
                            Cartel = "/../images/Venom_3.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesVenom(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = false // No está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 4,
                            Titulo = "Terrifier 3",
                            Descripcion = "Este año la Navidad llega antes. El payaso Art desata el caos entre los desprevenidos habitantes del condado de Miles...",
                            FechaEstreno = "2024, 10, 31",
                            Genero = "Terror",
                            Duracion = "2 horas 5 minutos",
                            Calificacion = 7.5,
                            Director = "Damien Leone",
                            Actores = "Felissa Rose, Samantha Scaffidi, David Howard Thornton, Lauren LaVera, Chris Jericho, Elliott Fullam",
                            Imagen = "/../images/banner-terrifier-3.jpg", // Ruta del banner
                            Cartel = "/../images/terrifier-3.jpg", // Ruta del cartel
                            EdadRecomendada = 18, // Marcado como para mayores de 18 años
                            ImagenEdadRecomendada = "/../images/18.jpg",
                            Sesiones = CrearSesionesTerrifier(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = false // No está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 5,
                            Titulo = "The Batman",
                            Descripcion = "Bruce Wayne, en sus primeros años como el Caballero Oscuro, enfrenta a un asesino en serie conocido como el Riddler. Mientras investiga los crímenes, descubre secretos oscuros sobre su familia y la corrupción en Gotham. Con la ayuda de Catwoman, busca hacer justicia en una ciudad sumida en el caos.",
                            FechaEstreno = "2022, 3, 4",
                            Genero = "Acción, Crimen, Drama",
                            Director = "Matt Reeves",
                            Actores = "Robert Pattinson, Zoë Kravitz, Colin Farrell",
                            Calificacion = 7.9,
                            Imagen = "/../images/Banner-The-Batman.jpg",
                            Cartel = "/../images/The-Batman-Cartel.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesBatman(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = true // No está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 6,
                            Titulo = "Gladiator II",
                            Descripcion = "Años después de la muerte de Máximo, Lucio, ahora adulto, se enfrenta a nuevas amenazas que ponen en riesgo al Imperio Romano. Inspirado por el sacrificio del gladiador, Lucio deberá enfrentarse a la corrupción y al poder en su intento por restaurar la justicia y el honor en Roma.",
                            FechaEstreno = "2024, 11, 15",
                            Genero = "Acción, Aventura, Drama",
                            Director = "Ridley Scott",
                            Duracion = "2h 27m",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Calificacion = 8.5,
                            Imagen = "/../images/Banner-Gladiator-II.jpg",
                            Cartel = "/../images/Gladiator-II-Cartel.jpg",
                            EdadRecomendada = 16,
                            ImagenEdadRecomendada = "/../images/16.jpg",
                            Sesiones = CrearSesionesGladiator2(),  // Asegúrate de definir las sesiones para Gladiator II
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 7,
                            Titulo = "Red One",
                            Descripcion = "Tras el secuestro de Papá Noel, nombre en clave: RED ONE, el Jefe de Seguridad del Polo Norte (Dwayne Johnson) debe formar equipo con el cazarrecompensas más infame del mundo (Chris Evans) en una misión trotamundos llena de acción para salvar la Navidad. No te pierdas #RedOne, protagonizada por Dwayne Johnson y Chris Evans. Disfruta de la película a partir del 6 de noviembre solo en cines.",
                            FechaEstreno = "2024, 11, 6",
                            Genero = "Acción, Comedia, Aventura",
                            Director = "Jake Kasdan",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Duracion = "2h 3m",
                            Calificacion = 8.5,
                            Imagen = "/../images/Banner-Red-One.jpg",
                            Cartel = "/../images/Red-One-Cartel.jpg",
                            EdadRecomendada = 7,
                            ImagenEdadRecomendada = "/../images/7.jpg",
                            Sesiones = CrearSesionesRedOne(), // Asegúrate de definir las sesiones para Red One
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 8, // Ajusta el ID según corresponda
                            Titulo = "Robot Salvaje",
                            Descripcion = "La película sigue el épico viaje de un robot -la unidad 7134 de Roz, 'Roz' para abreviar- que naufraga en una isla deshabitada y debe aprender a adaptarse al duro entorno, entablando gradualmente relaciones con los animales de la isla y convirtiéndose en padre adoptivo de un gosling huérfano.",
                            FechaEstreno = "2024, 10, 11",
                            Genero = "Aventura, Animación",
                            Director = "Chris Sanders",
                            Actores = "Bill Nighy, Lupita Nyong'o, Stephanie Hsu, Mark Hamill, Ving Rhames, Catherine O'Hara, Matt Berry, Pedro Pascal, Kit Connor",
                            Duracion = "1h 41m",
                            Calificacion = 9.0, // Ajusta esta calificación según sea necesario
                            Imagen = "/../images/Banner-Robot-Salvaje.jpg", // Cambia el nombre si necesitas una imagen específica
                            Cartel = "/../images/Robot-Salvaje-Cartel.jpg", // Cambia el nombre si necesitas una imagen específica
                            EdadRecomendada = 0, // Edad para todos los públicos
                            ImagenEdadRecomendada = "/../images/Todos.jpg", // Cambia el nombre si necesitas una imagen específica
                            Sesiones = CrearSesionesRobotSalvaje(), // Asegúrate de definir las sesiones para Robot Salvaje
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 9, // Cambia el ID según tu necesidad
                            Titulo = "Wicked",
                            Descripcion = "Tras dos décadas como uno de los musicales más aclamados y longevos en escena, WICKED llega en noviembre a la gran pantalla para convertirse en un esperadísimo y espectacular evento cinematográfico que definirá una era.",
                            FechaEstreno = "2024, 11, 22",
                            Genero = "Musical, Fantasía, Romance",
                            Director = "Jon M. Chu",
                            Duracion = "2h 40m",
                            Actores = "Peter Dinklage, Jeff Goldblum, Michelle Yeoh, Bronwyn James, Bowen Yang, Cynthia Erivo, Keala Settle, Ariana Grande, Marissa Bode, Jonathan Bailey, Ethan Slater",
                            Calificacion = 6.7, 
                            Imagen = "/../images/Banner-Wicked.jpg", // Ruta de la imagen del banner
                            Cartel = "/../images/Wicked-Cartel.jpg", // Ruta del cartel
                            EdadRecomendada = 0,
                            ImagenEdadRecomendada = "/../images/Todos.jpg", // Ajusta según el ícono de edades
                            Sesiones = CrearSesionesWicked(), // Método para generar sesiones
                            EnCartelera = false, // Cambia según la lógica de tu aplicación
                            EnVentaAnticipada = true // Cambia según la lógica de tu aplicación
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
                            Descripcion = "Cuando la identidad de Spider-Man es revelada, Peter Parker se enfrenta a las consecuencias que afectan a su vida personal y a sus seres queridos. Desesperado, busca la ayuda del Doctor Strange para restaurar el secreto, pero el hechizo sale mal, desatando un caos multiversal donde viejos enemigos regresan y nuevas alianzas se forman.",
                            FechaEstreno = "2021, 12, 17",
                            Genero = "acción, aventura, ciencia ficción",
                            Director = "Jon Watts",
                            Duracion = "1 hora 55 minutos",
                            Actores = "Tom Holland, Zendaya, Benedict Cumberbatch, Jacob Batalon, Willem Dafoe, Alfred Molina, Jamie Foxx",
                            Calificacion = 8.2,
                            Imagen = "/../images/Banner-Spiderman-no-way-home.jpg",
                            Cartel = "/../images/Spiderman-No-Way-Home-Cartel.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesSpiderMan()
                        },
                        new Pelicula
                        {
                            Id = 2,
                            Titulo = "X-Men Apocalypse",
                            Descripcion = "Años después de la muerte de Máximo, Lucio, ahora adulto, se enfrenta a nuevas amenazas que ponen en riesgo al Imperio Romano. Inspirado por el sacrificio del gladiador, Lucio deberá enfrentarse a la corrupción y al poder en su intento por restaurar la justicia y el honor en Roma.",
                            FechaEstreno = "2016, 5, 27",
                            Genero = "Sci-fi",
                            Director = "Bryan Singer",
                            Actores = "James McAvoy, Michael Fassbender, Jennifer Lawrence, Oscar Isaac, Nicholas Hoult, Rose Byrne, Evan Peters, Sophie Turner",
                            Duracion = "2 horas 24 minutos",
                            Calificacion = 7.0,
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Imagen = "/../images/X-Men_Apocalypse_Banner.jpg",
                            Cartel = "/../images/X-MEN_Apocalypse.jpg",
                            Sesiones = CrearSesionesXMen()
                        },
                        new Pelicula
                        {
                            Id = 3,
                            Titulo = "Venom: El Último Baile",
                            Descripcion = "Eddie Brock intenta reavivar su carrera entrevistando al asesino en serie Cletus Kasady, quien se convierte en el anfitrión del simbionte Carnage y escapa de prisión después de una fallida ejecución.",
                            FechaEstreno = "2024, 10, 25",
                            Genero = "Sci-fi",
                            Director = "Kelly Marcel",
                            Actores = "Rhys Ifans, Chiwetel Ejiofor, Tom Hardy, Stephen Graham, Alanna Ubach, Juno Temple, Clark Backo, Peggy Lu",
                            Duracion = "1 hora 48 minutos",
                            Calificacion = 6.4,
                            Imagen = "/../images/Venom-3-Banner.jpg",
                            Cartel = "/../images/Venom_3.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesVenom()
                        },
                        new Pelicula
                        {
                            Id = 4,
                            Titulo = "Terrifier 3",
                            Descripcion = "Este año la Navidad llega antes. El payaso Art desata el caos entre los desprevenidos habitantes del condado de Miles...",
                            FechaEstreno = "2024, 10, 31",
                            Genero = "Terror",
                            Duracion = "2 horas 5 minutos",
                            Calificacion = 7.5,
                            Director = "Damien Leone",
                            Actores = "Felissa Rose, Samantha Scaffidi, David Howard Thornton, Lauren LaVera, Chris Jericho, Elliott Fullam",
                            Imagen = "/../images/banner-terrifier-3.jpg", // Ruta del banner
                            Cartel = "/../images/terrifier-3.jpg", // Ruta del cartel
                            EdadRecomendada = 18, // Marcado como para mayores de 18 años
                            ImagenEdadRecomendada = "/../images/18.jpg",
                            Sesiones = CrearSesionesTerrifier(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = false // No está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 5,
                            Titulo = "The Batman",
                            Descripcion = "Bruce Wayne, en sus primeros años como el Caballero Oscuro, enfrenta a un asesino en serie conocido como el Riddler. Mientras investiga los crímenes, descubre secretos oscuros sobre su familia y la corrupción en Gotham. Con la ayuda de Catwoman, busca hacer justicia en una ciudad sumida en el caos.",
                            FechaEstreno = "2022, 3, 4",
                            Genero = "Acción, Crimen, Drama",
                            Director = "Matt Reeves",
                            Actores = "Robert Pattinson, Zoë Kravitz, Colin Farrell",
                            Calificacion = 7.9,
                            Imagen = "/../images/Banner-The-Batman.jpg",
                            Cartel = "/../images/The-Batman-Cartel.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesBatman(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = true // No está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 6,
                            Titulo = "Gladiator II",
                            Descripcion = "Años después de la muerte de Máximo, Lucio, ahora adulto, se enfrenta a nuevas amenazas que ponen en riesgo al Imperio Romano. Inspirado por el sacrificio del gladiador, Lucio deberá enfrentarse a la corrupción y al poder en su intento por restaurar la justicia y el honor en Roma.",
                            FechaEstreno = "2024, 11, 15",
                            Genero = "Acción, Aventura, Drama",
                            Director = "Ridley Scott",
                            Duracion = "2h 27m",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Calificacion = 8.5,
                            Imagen = "/../images/Banner-Gladiator-II.jpg",
                            Cartel = "/../images/Gladiator-II-Cartel.jpg",
                            EdadRecomendada = 16,
                            ImagenEdadRecomendada = "/../images/16.jpg",
                            Sesiones = CrearSesionesGladiator2(),  // Asegúrate de definir las sesiones para Gladiator II
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 7,
                            Titulo = "Red One",
                            Descripcion = "Tras el secuestro de Papá Noel, nombre en clave: RED ONE, el Jefe de Seguridad del Polo Norte (Dwayne Johnson) debe formar equipo con el cazarrecompensas más infame del mundo (Chris Evans) en una misión trotamundos llena de acción para salvar la Navidad. No te pierdas #RedOne, protagonizada por Dwayne Johnson y Chris Evans. Disfruta de la película a partir del 6 de noviembre solo en cines.",
                            FechaEstreno = "2024, 11, 6",
                            Genero = "Acción, Comedia, Aventura",
                            Director = "Jake Kasdan",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Duracion = "2h 3m",
                            Calificacion = 8.5,
                            Imagen = "/../images/Banner-Red-One.jpg",
                            Cartel = "/../images/Red-One-Cartel.jpg",
                            EdadRecomendada = 7,
                            ImagenEdadRecomendada = "/../images/7.jpg",
                            Sesiones = CrearSesionesRedOne(), // Asegúrate de definir las sesiones para Red One
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 8, // Ajusta el ID según corresponda
                            Titulo = "Robot Salvaje",
                            Descripcion = "La película sigue el épico viaje de un robot -la unidad 7134 de Roz, 'Roz' para abreviar- que naufraga en una isla deshabitada y debe aprender a adaptarse al duro entorno, entablando gradualmente relaciones con los animales de la isla y convirtiéndose en padre adoptivo de un gosling huérfano.",
                            FechaEstreno = "2024, 10, 11",
                            Genero = "Aventura, Animación",
                            Director = "Chris Sanders",
                            Actores = "Bill Nighy, Lupita Nyong'o, Stephanie Hsu, Mark Hamill, Ving Rhames, Catherine O'Hara, Matt Berry, Pedro Pascal, Kit Connor",
                            Duracion = "1h 41m",
                            Calificacion = 9.0, // Ajusta esta calificación según sea necesario
                            Imagen = "/../images/Banner-Robot-Salvaje.jpg", // Cambia el nombre si necesitas una imagen específica
                            Cartel = "/../images/Robot-Salvaje-Cartel.jpg", // Cambia el nombre si necesitas una imagen específica
                            EdadRecomendada = 0, // Edad para todos los públicos
                            ImagenEdadRecomendada = "/../images/Todos.jpg", // Cambia el nombre si necesitas una imagen específica
                            Sesiones = CrearSesionesRobotSalvaje(), // Asegúrate de definir las sesiones para Robot Salvaje
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 9, // Cambia el ID según tu necesidad
                            Titulo = "Wicked",
                            Descripcion = "Tras dos décadas como uno de los musicales más aclamados y longevos en escena, WICKED llega en noviembre a la gran pantalla para convertirse en un esperadísimo y espectacular evento cinematográfico que definirá una era.",
                            FechaEstreno = "2024, 11, 22",
                            Genero = "Musical, Fantasía, Romance",
                            Director = "Jon M. Chu",
                            Duracion = "2h 40m",
                            Actores = "Peter Dinklage, Jeff Goldblum, Michelle Yeoh, Bronwyn James, Bowen Yang, Cynthia Erivo, Keala Settle, Ariana Grande, Marissa Bode, Jonathan Bailey, Ethan Slater",
                            Calificacion = 6.7, 
                            Imagen = "/../images/Banner-Wicked.jpg", // Ruta de la imagen del banner
                            Cartel = "/../images/Wicked-Cartel.jpg", // Ruta del cartel
                            EdadRecomendada = 0,
                            ImagenEdadRecomendada = "/../images/Todos.jpg", // Ajusta según el ícono de edades
                            Sesiones = CrearSesionesWicked(), // Método para generar sesiones
                            EnCartelera = false, // Cambia según la lógica de tu aplicación
                            EnVentaAnticipada = true // Cambia según la lógica de tu aplicación
                        },
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
                            Id = 1,
                            Titulo = "Spider-Man: No Way Home",
                            Descripcion = "Cuando la identidad de Spider-Man es revelada, Peter Parker se enfrenta a las consecuencias que afectan a su vida personal y a sus seres queridos. Desesperado, busca la ayuda del Doctor Strange para restaurar el secreto, pero el hechizo sale mal, desatando un caos multiversal donde viejos enemigos regresan y nuevas alianzas se forman.",
                            FechaEstreno = "2021, 12, 17",
                            Genero = "acción, aventura, ciencia ficción",
                            Director = "Jon Watts",
                            Duracion = "1 hora 55 minutos",
                            Actores = "Tom Holland, Zendaya, Benedict Cumberbatch, Jacob Batalon, Willem Dafoe, Alfred Molina, Jamie Foxx",
                            Calificacion = 8.2,
                            Imagen = "/../images/Banner-Spiderman-no-way-home.jpg",
                            Cartel = "/../images/Spiderman-No-Way-Home-Cartel.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesSpiderMan()
                        },
                        new Pelicula
                        {
                            Id = 2,
                            Titulo = "X-Men Apocalypse",
                            Descripcion = "Años después de la muerte de Máximo, Lucio, ahora adulto, se enfrenta a nuevas amenazas que ponen en riesgo al Imperio Romano. Inspirado por el sacrificio del gladiador, Lucio deberá enfrentarse a la corrupción y al poder en su intento por restaurar la justicia y el honor en Roma.",
                            FechaEstreno = "2016, 5, 27",
                            Genero = "Sci-fi",
                            Director = "Bryan Singer",
                            Actores = "James McAvoy, Michael Fassbender, Jennifer Lawrence, Oscar Isaac, Nicholas Hoult, Rose Byrne, Evan Peters, Sophie Turner",
                            Duracion = "2 horas 24 minutos",
                            Calificacion = 7.0,
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Imagen = "/../images/X-Men_Apocalypse_Banner.jpg",
                            Cartel = "/../images/X-MEN_Apocalypse.jpg",
                            Sesiones = CrearSesionesXMen()
                        },
                        new Pelicula
                        {
                            Id = 3,
                            Titulo = "Venom: El Último Baile",
                            Descripcion = "Eddie Brock intenta reavivar su carrera entrevistando al asesino en serie Cletus Kasady, quien se convierte en el anfitrión del simbionte Carnage y escapa de prisión después de una fallida ejecución.",
                            FechaEstreno = "2024, 10, 25",
                            Genero = "Sci-fi",
                            Director = "Kelly Marcel",
                            Actores = "Rhys Ifans, Chiwetel Ejiofor, Tom Hardy, Stephen Graham, Alanna Ubach, Juno Temple, Clark Backo, Peggy Lu",
                            Duracion = "1 hora 48 minutos",
                            Calificacion = 6.4,
                            Imagen = "/../images/Venom-3-Banner.jpg",
                            Cartel = "/../images/Venom_3.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesVenom()
                        },
                        new Pelicula
                        {
                            Id = 4,
                            Titulo = "Terrifier 3",
                            Descripcion = "Este año la Navidad llega antes. El payaso Art desata el caos entre los desprevenidos habitantes del condado de Miles...",
                            FechaEstreno = "2024, 10, 31",
                            Genero = "Terror",
                            Duracion = "2 horas 5 minutos",
                            Calificacion = 7.5,
                            Director = "Damien Leone",
                            Actores = "Felissa Rose, Samantha Scaffidi, David Howard Thornton, Lauren LaVera, Chris Jericho, Elliott Fullam",
                            Imagen = "/../images/banner-terrifier-3.jpg", // Ruta del banner
                            Cartel = "/../images/terrifier-3.jpg", // Ruta del cartel
                            EdadRecomendada = 18, // Marcado como para mayores de 18 años
                            ImagenEdadRecomendada = "/../images/18.jpg",
                            Sesiones = CrearSesionesTerrifier(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = false // No está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 5,
                            Titulo = "The Batman",
                            Descripcion = "Bruce Wayne, en sus primeros años como el Caballero Oscuro, enfrenta a un asesino en serie conocido como el Riddler. Mientras investiga los crímenes, descubre secretos oscuros sobre su familia y la corrupción en Gotham. Con la ayuda de Catwoman, busca hacer justicia en una ciudad sumida en el caos.",
                            FechaEstreno = "2022, 3, 4",
                            Genero = "Acción, Crimen, Drama",
                            Director = "Matt Reeves",
                            Actores = "Robert Pattinson, Zoë Kravitz, Colin Farrell",
                            Calificacion = 7.9,
                            Imagen = "/../images/Banner-The-Batman.jpg",
                            Cartel = "/../images/The-Batman-Cartel.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesBatman(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = true // No está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 6,
                            Titulo = "Gladiator II",
                            Descripcion = "Años después de la muerte de Máximo, Lucio, ahora adulto, se enfrenta a nuevas amenazas que ponen en riesgo al Imperio Romano. Inspirado por el sacrificio del gladiador, Lucio deberá enfrentarse a la corrupción y al poder en su intento por restaurar la justicia y el honor en Roma.",
                            FechaEstreno = "2024, 11, 15",
                            Genero = "Acción, Aventura, Drama",
                            Director = "Ridley Scott",
                            Duracion = "2h 27m",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Calificacion = 8.5,
                            Imagen = "/../images/Banner-Gladiator-II.jpg",
                            Cartel = "/../images/Gladiator-II-Cartel.jpg",
                            EdadRecomendada = 16,
                            ImagenEdadRecomendada = "/../images/16.jpg",
                            Sesiones = CrearSesionesGladiator2(),  // Asegúrate de definir las sesiones para Gladiator II
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 7,
                            Titulo = "Red One",
                            Descripcion = "Tras el secuestro de Papá Noel, nombre en clave: RED ONE, el Jefe de Seguridad del Polo Norte (Dwayne Johnson) debe formar equipo con el cazarrecompensas más infame del mundo (Chris Evans) en una misión trotamundos llena de acción para salvar la Navidad. No te pierdas #RedOne, protagonizada por Dwayne Johnson y Chris Evans. Disfruta de la película a partir del 6 de noviembre solo en cines.",
                            FechaEstreno = "2024, 11, 6",
                            Genero = "Acción, Comedia, Aventura",
                            Director = "Jake Kasdan",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Duracion = "2h 3m",
                            Calificacion = 8.5,
                            Imagen = "/../images/Banner-Red-One.jpg",
                            Cartel = "/../images/Red-One-Cartel.jpg",
                            EdadRecomendada = 7,
                            ImagenEdadRecomendada = "/../images/7.jpg",
                            Sesiones = CrearSesionesRedOne(), // Asegúrate de definir las sesiones para Red One
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 8, // Ajusta el ID según corresponda
                            Titulo = "Robot Salvaje",
                            Descripcion = "La película sigue el épico viaje de un robot -la unidad 7134 de Roz, 'Roz' para abreviar- que naufraga en una isla deshabitada y debe aprender a adaptarse al duro entorno, entablando gradualmente relaciones con los animales de la isla y convirtiéndose en padre adoptivo de un gosling huérfano.",
                            FechaEstreno = "2024, 10, 11",
                            Genero = "Aventura, Animación",
                            Director = "Chris Sanders",
                            Actores = "Bill Nighy, Lupita Nyong'o, Stephanie Hsu, Mark Hamill, Ving Rhames, Catherine O'Hara, Matt Berry, Pedro Pascal, Kit Connor",
                            Duracion = "1h 41m",
                            Calificacion = 9.0, // Ajusta esta calificación según sea necesario
                            Imagen = "/../images/Banner-Robot-Salvaje.jpg", // Cambia el nombre si necesitas una imagen específica
                            Cartel = "/../images/Robot-Salvaje-Cartel.jpg", // Cambia el nombre si necesitas una imagen específica
                            EdadRecomendada = 0, // Edad para todos los públicos
                            ImagenEdadRecomendada = "/../images/Todos.jpg", // Cambia el nombre si necesitas una imagen específica
                            Sesiones = CrearSesionesRobotSalvaje(), // Asegúrate de definir las sesiones para Robot Salvaje
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 9, // Cambia el ID según tu necesidad
                            Titulo = "Wicked",
                            Descripcion = "Tras dos décadas como uno de los musicales más aclamados y longevos en escena, WICKED llega en noviembre a la gran pantalla para convertirse en un esperadísimo y espectacular evento cinematográfico que definirá una era.",
                            FechaEstreno = "2024, 11, 22",
                            Genero = "Musical, Fantasía, Romance",
                            Director = "Jon M. Chu",
                            Duracion = "2h 40m",
                            Actores = "Peter Dinklage, Jeff Goldblum, Michelle Yeoh, Bronwyn James, Bowen Yang, Cynthia Erivo, Keala Settle, Ariana Grande, Marissa Bode, Jonathan Bailey, Ethan Slater",
                            Calificacion = 6.7, 
                            Imagen = "/../images/Banner-Wicked.jpg", // Ruta de la imagen del banner
                            Cartel = "/../images/Wicked-Cartel.jpg", // Ruta del cartel
                            EdadRecomendada = 0,
                            ImagenEdadRecomendada = "/../images/Todos.jpg", // Ajusta según el ícono de edades
                            Sesiones = CrearSesionesWicked(), // Método para generar sesiones
                            EnCartelera = false, // Cambia según la lógica de tu aplicación
                            EnVentaAnticipada = true // Cambia según la lógica de tu aplicación
                        },
                    }
                },
                new Cine
                {
                    Id = 4,
                    Nombre = "Casco Antiguo",
                    Peliculas = new List<Pelicula>
                    {
                        new Pelicula
                        {
                            Id = 1,
                            Titulo = "Spider-Man: No Way Home",
                            Descripcion = "Cuando la identidad de Spider-Man es revelada, Peter Parker se enfrenta a las consecuencias que afectan a su vida personal y a sus seres queridos. Desesperado, busca la ayuda del Doctor Strange para restaurar el secreto, pero el hechizo sale mal, desatando un caos multiversal donde viejos enemigos regresan y nuevas alianzas se forman.",
                            FechaEstreno = "2021, 12, 17",
                            Genero = "acción, aventura, ciencia ficción",
                            Director = "Jon Watts",
                            Duracion = "1 hora 55 minutos",
                            Actores = "Tom Holland, Zendaya, Benedict Cumberbatch, Jacob Batalon, Willem Dafoe, Alfred Molina, Jamie Foxx",
                            Calificacion = 8.2,
                            Imagen = "/../images/Banner-Spiderman-no-way-home.jpg",
                            Cartel = "/../images/Spiderman-No-Way-Home-Cartel.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesSpiderMan()
                        },
                        new Pelicula
                        {
                            Id = 2,
                            Titulo = "X-Men Apocalypse",
                            Descripcion = "Años después de la muerte de Máximo, Lucio, ahora adulto, se enfrenta a nuevas amenazas que ponen en riesgo al Imperio Romano. Inspirado por el sacrificio del gladiador, Lucio deberá enfrentarse a la corrupción y al poder en su intento por restaurar la justicia y el honor en Roma.",
                            FechaEstreno = "2016, 5, 27",
                            Genero = "Sci-fi",
                            Director = "Bryan Singer",
                            Actores = "James McAvoy, Michael Fassbender, Jennifer Lawrence, Oscar Isaac, Nicholas Hoult, Rose Byrne, Evan Peters, Sophie Turner",
                            Duracion = "2 horas 24 minutos",
                            Calificacion = 7.0,
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Imagen = "/../images/X-Men_Apocalypse_Banner.jpg",
                            Cartel = "/../images/X-MEN_Apocalypse.jpg",
                            Sesiones = CrearSesionesXMen()
                        },
                        new Pelicula
                        {
                            Id = 3,
                            Titulo = "Venom: El Último Baile",
                            Descripcion = "Eddie Brock intenta reavivar su carrera entrevistando al asesino en serie Cletus Kasady, quien se convierte en el anfitrión del simbionte Carnage y escapa de prisión después de una fallida ejecución.",
                            FechaEstreno = "2024, 10, 25",
                            Genero = "Sci-fi",
                            Director = "Kelly Marcel",
                            Actores = "Rhys Ifans, Chiwetel Ejiofor, Tom Hardy, Stephen Graham, Alanna Ubach, Juno Temple, Clark Backo, Peggy Lu",
                            Duracion = "1 hora 48 minutos",
                            Calificacion = 6.4,
                            Imagen = "/../images/Venom-3-Banner.jpg",
                            Cartel = "/../images/Venom_3.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesVenom()
                        },
                        new Pelicula
                        {
                            Id = 4,
                            Titulo = "Terrifier 3",
                            Descripcion = "Este año la Navidad llega antes. El payaso Art desata el caos entre los desprevenidos habitantes del condado de Miles...",
                            FechaEstreno = "2024, 10, 31",
                            Genero = "Terror",
                            Duracion = "2 horas 5 minutos",
                            Calificacion = 7.5,
                            Director = "Damien Leone",
                            Actores = "Felissa Rose, Samantha Scaffidi, David Howard Thornton, Lauren LaVera, Chris Jericho, Elliott Fullam",
                            Imagen = "/../images/banner-terrifier-3.jpg", // Ruta del banner
                            Cartel = "/../images/terrifier-3.jpg", // Ruta del cartel
                            EdadRecomendada = 18, // Marcado como para mayores de 18 años
                            ImagenEdadRecomendada = "/../images/18.jpg",
                            Sesiones = CrearSesionesTerrifier(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = false // No está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 5,
                            Titulo = "The Batman",
                            Descripcion = "Bruce Wayne, en sus primeros años como el Caballero Oscuro, enfrenta a un asesino en serie conocido como el Riddler. Mientras investiga los crímenes, descubre secretos oscuros sobre su familia y la corrupción en Gotham. Con la ayuda de Catwoman, busca hacer justicia en una ciudad sumida en el caos.",
                            FechaEstreno = "2022, 3, 4",
                            Genero = "Acción, Crimen, Drama",
                            Director = "Matt Reeves",
                            Actores = "Robert Pattinson, Zoë Kravitz, Colin Farrell",
                            Calificacion = 7.9,
                            Imagen = "/../images/Banner-The-Batman.jpg",
                            Cartel = "/../images/The-Batman-Cartel.jpg",
                            EdadRecomendada = 12,
                            ImagenEdadRecomendada = "/../images/12.jpg",
                            Sesiones = CrearSesionesBatman(),
                            EnCartelera = true, // Actualmente en cartelera
                            EnVentaAnticipada = true // No está en venta anticipada
                        },
                        new Pelicula
                        {
                            Id = 6,
                            Titulo = "Gladiator II",
                            Descripcion = "Años después de la muerte de Máximo, Lucio, ahora adulto, se enfrenta a nuevas amenazas que ponen en riesgo al Imperio Romano. Inspirado por el sacrificio del gladiador, Lucio deberá enfrentarse a la corrupción y al poder en su intento por restaurar la justicia y el honor en Roma.",
                            FechaEstreno = "2024, 11, 15",
                            Genero = "Acción, Aventura, Drama",
                            Director = "Ridley Scott",
                            Duracion = "2h 27m",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Calificacion = 8.5,
                            Imagen = "/../images/Banner-Gladiator-II.jpg",
                            Cartel = "/../images/Gladiator-II-Cartel.jpg",
                            EdadRecomendada = 16,
                            ImagenEdadRecomendada = "/../images/16.jpg",
                            Sesiones = CrearSesionesGladiator2(),  // Asegúrate de definir las sesiones para Gladiator II
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 7,
                            Titulo = "Red One",
                            Descripcion = "Tras el secuestro de Papá Noel, nombre en clave: RED ONE, el Jefe de Seguridad del Polo Norte (Dwayne Johnson) debe formar equipo con el cazarrecompensas más infame del mundo (Chris Evans) en una misión trotamundos llena de acción para salvar la Navidad. No te pierdas #RedOne, protagonizada por Dwayne Johnson y Chris Evans. Disfruta de la película a partir del 6 de noviembre solo en cines.",
                            FechaEstreno = "2024, 11, 6",
                            Genero = "Acción, Comedia, Aventura",
                            Director = "Jake Kasdan",
                            Actores = "Paul Mescal, Denzel Washington, Connie Nielsen, Joseph Quinn, Derek Jacobi, Fred Hechinger, Lior Raz, Pedro Pascal",
                            Duracion = "2h 3m",
                            Calificacion = 8.5,
                            Imagen = "/../images/Banner-Red-One.jpg",
                            Cartel = "/../images/Red-One-Cartel.jpg",
                            EdadRecomendada = 7,
                            ImagenEdadRecomendada = "/../images/7.jpg",
                            Sesiones = CrearSesionesRedOne(), // Asegúrate de definir las sesiones para Red One
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 8, // Ajusta el ID según corresponda
                            Titulo = "Robot Salvaje",
                            Descripcion = "La película sigue el épico viaje de un robot -la unidad 7134 de Roz, 'Roz' para abreviar- que naufraga en una isla deshabitada y debe aprender a adaptarse al duro entorno, entablando gradualmente relaciones con los animales de la isla y convirtiéndose en padre adoptivo de un gosling huérfano.",
                            FechaEstreno = "2024, 10, 11",
                            Genero = "Aventura, Animación",
                            Director = "Chris Sanders",
                            Actores = "Bill Nighy, Lupita Nyong'o, Stephanie Hsu, Mark Hamill, Ving Rhames, Catherine O'Hara, Matt Berry, Pedro Pascal, Kit Connor",
                            Duracion = "1h 41m",
                            Calificacion = 9.0, // Ajusta esta calificación según sea necesario
                            Imagen = "/../images/Banner-Robot-Salvaje.jpg", // Cambia el nombre si necesitas una imagen específica
                            Cartel = "/../images/Robot-Salvaje-Cartel.jpg", // Cambia el nombre si necesitas una imagen específica
                            EdadRecomendada = 0, // Edad para todos los públicos
                            ImagenEdadRecomendada = "/../images/Todos.jpg", // Cambia el nombre si necesitas una imagen específica
                            Sesiones = CrearSesionesRobotSalvaje(), // Asegúrate de definir las sesiones para Robot Salvaje
                            EnCartelera = true,
                            EnVentaAnticipada = true
                        },
                        new Pelicula
                        {
                            Id = 9, // Cambia el ID según tu necesidad
                            Titulo = "Wicked",
                            Descripcion = "Tras dos décadas como uno de los musicales más aclamados y longevos en escena, WICKED llega en noviembre a la gran pantalla para convertirse en un esperadísimo y espectacular evento cinematográfico que definirá una era.",
                            FechaEstreno = "2024, 11, 22",
                            Genero = "Musical, Fantasía, Romance",
                            Director = "Jon M. Chu",
                            Duracion = "2h 40m",
                            Actores = "Peter Dinklage, Jeff Goldblum, Michelle Yeoh, Bronwyn James, Bowen Yang, Cynthia Erivo, Keala Settle, Ariana Grande, Marissa Bode, Jonathan Bailey, Ethan Slater",
                            Calificacion = 6.7, 
                            Imagen = "/../images/Banner-Wicked.jpg", // Ruta de la imagen del banner
                            Cartel = "/../images/Wicked-Cartel.jpg", // Ruta del cartel
                            EdadRecomendada = 0,
                            ImagenEdadRecomendada = "/../images/Todos.jpg", // Ajusta según el ícono de edades
                            Sesiones = CrearSesionesWicked(), // Método para generar sesiones
                            EnCartelera = false, // Cambia según la lógica de tu aplicación
                            EnVentaAnticipada = true // Cambia según la lógica de tu aplicación
                        },
                    }
                },
            };
        }

        private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesRedOne()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 1, Hora = "16:30", Sala = "10", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 2, Hora = "18:05", Sala = "8", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 3, Hora = "20:30", Sala = "3", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 4, Hora = "22:15", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 5, Hora = "00:45", Sala = "7", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 6 ,Hora = "15:30", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 7 ,Hora = "17:45", Sala = "9", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 8 ,Hora = "19:50", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 9 ,Hora = "22:10", Sala = "2", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 10 ,Hora = "14:00", Sala = "4", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 11 ,Hora = "16:30", Sala = "1", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 12 ,Hora = "19:00", Sala = "8", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 13 ,Hora = "21:45", Sala = "7", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 14 ,Hora = "15:00", Sala = "10", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 15 ,Hora = "17:00", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 16 ,Hora = "20:30", Sala = "6", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 17 ,Hora = "18:00", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 18 ,Hora = "20:30", Sala = "9", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 19 ,Hora = "22:00", Sala = "2", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 20 ,Hora = "23:45", Sala = "8", EsISense = false, EsVOSE = false }
                            }
                        }
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 21, Hora = "17:00", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 22, Hora = "19:30", Sala = "2", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 23, Hora = "21:45", Sala = "4", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 24, Hora = "23:15", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 25, Hora = "00:30", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 26, Hora = "16:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 27, Hora = "20:00", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 28, Hora = "22:15", Sala = "8", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 29, Hora = "15:00", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 30, Hora = "18:30", Sala = "9", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 31, Hora = "17:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 32, Hora = "19:30", Sala = "7", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 33, Hora = "22:00", Sala = "5", EsISense = true, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Torre Outlet", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 34, Hora = "15:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 35, Hora = "18:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 36, Hora = "21:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 37, Hora = "22:45", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 38, Hora = "00:30", Sala = "7", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 39, Hora = "16:15", Sala = "5", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 40, Hora = "19:45", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 41, Hora = "22:15", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 42, Hora = "14:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 43, Hora = "17:00", Sala = "6", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 44, Hora = "15:30", Sala = "10", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 45, Hora = "18:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 46, Hora = "20:30", Sala = "2", EsISense = true, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Casco Antiguo", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 47, Hora = "15:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 48, Hora = "18:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 49, Hora = "21:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 50, Hora = "22:45", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 51, Hora = "00:30", Sala = "7", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 52, Hora = "16:15", Sala = "5", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 53, Hora = "19:45", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 54, Hora = "22:15", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 55, Hora = "14:30", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 56, Hora = "17:00", Sala = "4", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 57, Hora = "16:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 58, Hora = "18:30", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 59, Hora = "21:00", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        }
                    }
                }
            };
        }

        private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesWicked()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                    {
                        "Gran Casa", new Dictionary<string, List<Sesion>>
                        {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 60, Hora = "16:00", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 61, Hora = "18:45", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 62, Hora = "21:30", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 63, Hora = "23:30", Sala = "8", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 64, Hora = "14:30", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 65, Hora = "18:00", Sala = "6", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 66, Hora = "20:30", Sala = "4", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 67, Hora = "22:45", Sala = "5", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 68, Hora = "17:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 69, Hora = "20:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 70, Hora = "22:30", Sala = "6", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 71, Hora = "14:00", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 72, Hora = "18:30", Sala = "3", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 73, Hora = "16:00", Sala = "4", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 74, Hora = "19:00", Sala = "2", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 75, Hora = "21:30", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 76, Hora = "14:45", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 77, Hora = "18:15", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 78, Hora = "20:45", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 79, Hora = "23:00", Sala = "6", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Id = 80, Hora = "15:30", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 81, Hora = "20:00", Sala = "9", EsISense = true, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 82, Hora = "15:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 83, Hora = "18:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 84, Hora = "21:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 85, Hora = "23:30", Sala = "5", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 86, Hora = "16:15", Sala = "5", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 87, Hora = "19:45", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 88, Hora = "22:15", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 89, Hora = "00:00", Sala = "6", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 90, Hora = "17:15", Sala = "1", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 91, Hora = "20:30", Sala = "6", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 92, Hora = "14:00", Sala = "2", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 93, Hora = "18:30", Sala = "5", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 94, Hora = "16:30", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 95, Hora = "19:00", Sala = "7", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 96, Hora = "21:30", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 97, Hora = "23:15", Sala = "6", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 98, Hora = "15:45", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 99, Hora = "20:15", Sala = "4", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 100, Hora = "22:00", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 101, Hora = "23:30", Sala = "8", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Id = 102, Hora = "14:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 103, Hora = "19:45", Sala = "3", EsISense = false, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Torre Outlet", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 104, Hora = "15:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 105, Hora = "18:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 106, Hora = "21:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 107, Hora = "23:15", Sala = "5", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 108, Hora = "01:00", Sala = "6", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 109, Hora = "16:15", Sala = "5", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 110, Hora = "19:45", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 111, Hora = "22:15", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 112, Hora = "00:00", Sala = "9", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 113, Hora = "17:00", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 114, Hora = "19:30", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 115, Hora = "22:00", Sala = "6", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 116, Hora = "14:30", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 117, Hora = "18:00", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 118, Hora = "20:45", Sala = "5", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 119, Hora = "16:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 120, Hora = "18:45", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 121, Hora = "21:15", Sala = "4", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 122, Hora = "15:00", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 123, Hora = "18:30", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 124, Hora = "20:45", Sala = "8", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 125, Hora = "23:00", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 126, Hora = "00:30", Sala = "7", EsISense = true, EsVOSE = false }
                            }
                        }
                    }
                },
                {
                    "Casco Antiguo", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 127, Hora = "15:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 128, Hora = "18:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 129, Hora = "21:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 130, Hora = "23:15", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 131, Hora = "01:00", Sala = "6", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 132, Hora = "16:15", Sala = "5", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 133, Hora = "19:45", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 134, Hora = "22:15", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 135, Hora = "16:00", Sala = "1", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 136, Hora = "19:30", Sala = "4", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 137, Hora = "22:00", Sala = "8", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 138, Hora = "23:45", Sala = "5", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 139, Hora = "14:30", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 140, Hora = "18:00", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 141, Hora = "20:45", Sala = "5", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 142, Hora = "15:00", Sala = "9", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 143, Hora = "18:30", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 144, Hora = "21:00", Sala = "3", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 145, Hora = "14:00", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 146, Hora = "17:30", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 147, Hora = "21:00", Sala = "8", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 148, Hora = "23:15", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 150, Hora = "01:00", Sala = "6", EsISense = true, EsVOSE = false }
                            }
                        }
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
                                new Sesion { Id = 151, Hora = "15:00", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 152, Hora = "17:30", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 153, Hora = "20:00", Sala = "2", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 154, Hora = "22:30", Sala = "4", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion {Id = 155, Hora = "16:00", Sala = "7", EsISense = false, EsVOSE = true },
                                new Sesion {Id = 156, Hora = "18:45", Sala = "9", EsISense = true, EsVOSE = false },
                                new Sesion {Id = 157, Hora = "21:00", Sala = "5", EsISense = true, EsVOSE = true }
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
                                new Sesion { Id = 158, Hora = "14:30", Sala = "6", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 159, Hora = "17:00", Sala = "3", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 160, Hora = "20:00", Sala = "10", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 161, Hora = "15:15", Sala = "4", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 162, Hora = "18:10", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 163, Hora = "20:30", Sala = "2", EsISense = true, EsVOSE = true }
                            }
                        }
                        // Agrega más fechas y sesiones para Puerto Venecia según sea necesario
                    }
                },
                {
                    "Casco Antiguo", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 164, Hora = "15:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 165, Hora = "18:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 166, Hora = "21:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 167, Hora = "22:45", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 168, Hora = "00:30", Sala = "7", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 170, Hora = "16:15", Sala = "5", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 171, Hora = "19:45", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 172, Hora = "22:15", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 173, Hora = "14:30", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 174, Hora = "17:00", Sala = "4", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 175, Hora = "16:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 176, Hora = "18:30", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 177, Hora = "21:00", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        }
                    }
                },
                {
                    "Torre Outlet", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 178, Hora = "15:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 179, Hora = "18:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 180, Hora = "21:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 181, Hora = "23:15", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 182, Hora = "00:45", Sala = "7", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 183, Hora = "16:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 184, Hora = "18:30", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 185, Hora = "20:45", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 186, Hora = "14:30", Sala = "2", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 187, Hora = "17:00", Sala = "9", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 188, Hora = "20:15", Sala = "7", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 189, Hora = "15:00", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 190, Hora = "17:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 191, Hora = "20:00", Sala = "3", EsISense = true, EsVOSE = true }
                            }
                        }
                    }
                },
            };
        }
        private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesVenom()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 192, Hora = "14:00", Sala = "2", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 193, Hora = "17:15", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 194, Hora = "20:30", Sala = "8", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 195, Hora = "13:30", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 196, Hora = "16:45", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 197, Hora = "20:00", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 198, Hora = "22:15", Sala = "9", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 199, Hora = "14:30", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 200, Hora = "17:45", Sala = "5", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 201, Hora = "21:00", Sala = "8", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 202, Hora = "16:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 203, Hora = "18:30", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 204, Hora = "22:00", Sala = "6", EsISense = true, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 205, Hora = "15:00", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 206, Hora = "18:20", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 207, Hora = "21:30", Sala = "9", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 208, Hora = "23:45", Sala = "2", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 209, Hora = "16:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 210, Hora = "18:30", Sala = "7", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 211, Hora = "21:00", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 212, Hora = "15:00", Sala = "9", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 213, Hora = "17:45", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 214, Hora = "20:30", Sala = "2", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 215, Hora = "16:15", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 216, Hora = "19:00", Sala = "4", EsISense = false, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Torre Outlet", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 216, Hora = "15:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 217, Hora = "18:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 218, Hora = "21:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 219, Hora = "23:15", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 220, Hora = "00:45", Sala = "7", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 221, Hora = "16:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 222, Hora = "18:30", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 223, Hora = "20:45", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 224, Hora = "14:30", Sala = "2", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 225, Hora = "17:00", Sala = "9", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 226, Hora = "20:15", Sala = "7", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 227, Hora = "15:00", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 228, Hora = "17:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 229, Hora = "20:00", Sala = "3", EsISense = true, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Casco Antiguo", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 230, Hora = "15:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 231, Hora = "18:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 232, Hora = "21:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 233, Hora = "22:45", Sala = "5", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 234, Hora = "16:15", Sala = "5", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 235, Hora = "19:45", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 236, Hora = "22:15", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 237, Hora = "14:30", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 238, Hora = "17:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 239, Hora = "20:30", Sala = "2", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 240, Hora = "15:45", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 241, Hora = "19:00", Sala = "6", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 242, Hora = "22:00", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        }
                    }
                }
            };
        }

        private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesTerrifier()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 243, Hora = "14:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 244, Hora = "17:15", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 245, Hora = "19:30", Sala = "8", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 246, Hora = "22:00", Sala = "3", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 247, Hora = "13:30", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 248, Hora = "16:45", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 249, Hora = "20:00", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 250, Hora = "22:30", Sala = "4", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 251, Hora = "14:00", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 252, Hora = "16:45", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 253, Hora = "19:15", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 254, Hora = "21:30", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 255, Hora = "23:45", Sala = "8", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 256, Hora = "15:00", Sala = "6", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 257, Hora = "18:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 258, Hora = "21:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 259, Hora = "23:30", Sala = "5", EsISense = false, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {  
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 260, Hora = "15:00", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 261, Hora = "18:20", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 262, Hora = "21:30", Sala = "9", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 263, Hora = "23:15", Sala = "3", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 264, Hora = "00:45", Sala = "2", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 265, Hora = "13:45", Sala = "2", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 266, Hora = "17:00", Sala = "6", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 267, Hora = "20:15", Sala = "10", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 268, Hora = "23:00", Sala = "5", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 269, Hora = "14:30", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 270, Hora = "16:45", Sala = "9", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 271, Hora = "19:00", Sala = "2", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 272, Hora = "15:15", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 273, Hora = "18:00", Sala = "8", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 274, Hora = "21:30", Sala = "3", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 275, Hora = "23:00", Sala = "9", EsISense = false, EsVOSE = false }
                            }
                        }
                    }
                },
                {
                    "Torre Outlet", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 276, Hora = "16:00", Sala = "1", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 277, Hora = "18:30", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 278, Hora = "20:45", Sala = "5", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 279, Hora = "14:30", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 280, Hora = "17:15", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 281, Hora = "19:45", Sala = "6", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 282, Hora = "22:00", Sala = "7", EsISense = false, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Casco Antiguo", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 283, Hora = "15:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 284, Hora = "17:30", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 285, Hora = "20:00", Sala = "5", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 286, Hora = "14:00", Sala = "1", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 287, Hora = "16:45", Sala = "4", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 288, Hora = "19:30", Sala = "6", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 289, Hora = "15:30", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 290, Hora = "18:00", Sala = "8", EsISense = false, EsVOSE = false }
                            }
                        }
                    }
                }
            };
        }

        private Dictionary<string, Dictionary<string, List<Sesion>>> CrearSesionesBatman()
        {
            return new Dictionary<string, Dictionary<string, List<Sesion>>>
            {
                {
                    "Gran Casa", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 291, Hora = "16:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 292, Hora = "19:00", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 293, Hora = "21:30", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 294, Hora = "23:50", Sala = "4", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 295, Hora = "15:30", Sala = "7", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 296, Hora = "18:45", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 297, Hora = "21:10", Sala = "9", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 298, Hora = "23:45", Sala = "6", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 299, Hora = "01:00", Sala = "3", EsISense = false, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 300, Hora = "14:20", Sala = "4", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 301, Hora = "17:40", Sala = "6", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 302, Hora = "20:50", Sala = "10", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 303, Hora = "23:15", Sala = "8", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 304, Hora = "16:15", Sala = "8", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 304, Hora = "19:30", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 305, Hora = "22:45", Sala = "1", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 306, Hora = "01:00", Sala = "3", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 307, Hora = "15:00", Sala = "9", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 308, Hora = "18:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 309, Hora = "20:45", Sala = "7", EsISense = false, EsVOSE = false }
                            }
                        }
                    }
                },
                {
                    "Torre Outlet", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 310, Hora = "14:00", Sala = "1", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 311, Hora = "16:45", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 312, Hora = "19:30", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 313, Hora = "21:45", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 314, Hora = "15:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 315, Hora = "17:30", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 316, Hora = "20:00", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 317, Hora = "23:00", Sala = "8", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 318, Hora = "16:00", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 319, Hora = "18:45", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 320, Hora = "21:00", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Casco Antiguo", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 321, Hora = "15:15", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 322, Hora = "17:45", Sala = "6", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 323, Hora = "20:15", Sala = "8", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 324, Hora = "22:30", Sala = "9", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 325, Hora = "14:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 326, Hora = "16:30", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 327, Hora = "19:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 328, Hora = "21:45", Sala = "1", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 329, Hora = "15:00", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 330, Hora = "17:30", Sala = "8", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 331, Hora = "20:00", Sala = "6", EsISense = true, EsVOSE = true }
                            }
                        }
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
                                new Sesion { Id = 332, Hora = "14:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 333, Hora = "16:30", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 334, Hora = "19:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 335, Hora = "21:30", Sala = "3", EsISense = false, EsVOSE = true }
                            }
                        },
                        // Martes 12 de noviembre
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 334, Hora = "15:00", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 335, Hora = "17:30", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 336, Hora = "20:00", Sala = "5", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 337, Hora = "22:30", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        },
                        // Miércoles 13 de noviembre
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 338, Hora = "13:45", Sala = "1", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 339, Hora = "16:15", Sala = "3", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 340, Hora = "18:45", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 341, Hora = "21:00", Sala = "8", EsISense = true, EsVOSE = false }
                            }
                        },
                        // Jueves 14 de noviembre
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 342, Hora = "14:30", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 343, Hora = "17:00", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 344, Hora = "19:30", Sala = "2", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 345, Hora = "22:00", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        },
                        // Viernes 15 de noviembre
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 346, Hora = "16:30", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 347, Hora = "19:00", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 348, Hora = "21:30", Sala = "4", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 349, Hora = "23:45", Sala = "6", EsISense = true, EsVOSE = true }
                            }
                        },
                        // Sábado 16 de noviembre
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 350, Hora = "13:30", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 351, Hora = "16:00", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 352, Hora = "18:30", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 353, Hora = "21:00", Sala = "2", EsISense = true, EsVOSE = true }
                            }
                        },
                        // Domingo 17 de noviembre
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Id = 354, Hora = "15:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 355, Hora = "17:30", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 356, Hora = "20:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 357, Hora = "22:30", Sala = "6", EsISense = false, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 358, Hora = "14:00", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 359, Hora = "16:30", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 360, Hora = "19:00", Sala = "2", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 361, Hora = "21:30", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 362, Hora = "23:45", Sala = "6", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 363, Hora = "15:30", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 364, Hora = "18:00", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 365, Hora = "20:30", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 366, Hora = "23:00", Sala = "7", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 367, Hora = "14:45", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 368, Hora = "17:15", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 369, Hora = "19:45", Sala = "8", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 370, Hora = "22:15", Sala = "2", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 371, Hora = "14:30", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 372, Hora = "17:00", Sala = "8", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 373, Hora = "19:30", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 374, Hora = "21:45", Sala = "7", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 375, Hora = "15:00", Sala = "9", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 376, Hora = "18:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 377, Hora = "20:45", Sala = "7", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 378, Hora = "23:30", Sala = "4", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 379, Hora = "16:00", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 380, Hora = "18:30", Sala = "5", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 381, Hora = "21:00", Sala = "8", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Id = 382, Hora = "15:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 383, Hora = "17:30", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 384, Hora = "20:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 385, Hora = "22:45", Sala = "1", EsISense = false, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Torre Outlet", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 386, Hora = "13:30", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 387, Hora = "16:00", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 388, Hora = "18:30", Sala = "5", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 389, Hora = "21:00", Sala = "7", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 390, Hora = "14:15", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 391, Hora = "17:00", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 392, Hora = "20:15", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 393, Hora = "22:30", Sala = "8", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 394, Hora = "15:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 395, Hora = "18:30", Sala = "7", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 396, Hora = "21:45", Sala = "9", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 397, Hora = "14:45", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 398, Hora = "17:15", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 399, Hora = "20:00", Sala = "8", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 400, Hora = "22:30", Sala = "6", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 401, Hora = "15:00", Sala = "9", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 402, Hora = "18:00", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 403, Hora = "21:00", Sala = "5", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 404, Hora = "13:30", Sala = "2", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 405, Hora = "16:15", Sala = "4", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 406, Hora = "19:00", Sala = "8", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Id = 407, Hora = "14:00", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 408, Hora = "16:45", Sala = "7", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 409, Hora = "19:15", Sala = "1", EsISense = true, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Casco Antiguo", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 410, Hora = "14:00", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 411, Hora = "16:30", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 412, Hora = "19:00", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 413, Hora = "21:30", Sala = "1", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 414, Hora = "15:00", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 415, Hora = "17:45", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 416, Hora = "20:30", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 417, Hora = "14:30", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 418, Hora = "17:00", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 419, Hora = "19:30", Sala = "9", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 420, Hora = "15:30", Sala = "3", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 421, Hora = "18:00", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 422, Hora = "20:45", Sala = "8", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 423, Hora = "14:00", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 424, Hora = "17:00", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 425, Hora = "19:45", Sala = "6", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 426, Hora = "16:00", Sala = "9", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 427, Hora = "18:45", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Id = 428, Hora = "15:00", Sala = "7", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 429, Hora = "17:30", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 430, Hora = "20:00", Sala = "2", EsISense = false, EsVOSE = true }
                            }
                        }
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
                                new Sesion { Id = 431, Hora = "16:30", Sala = "10", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 432, Hora = "18:05", Sala = "8", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 433, Hora = "15:30", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 434, Hora = "17:45", Sala = "9", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 435, Hora = "19:50", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 436, Hora = "22:10", Sala = "2", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 437, Hora = "14:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 438, Hora = "16:30", Sala = "7", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 439, Hora = "19:00", Sala = "5", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 440, Hora = "15:15", Sala = "2", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 441, Hora = "18:00", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 442, Hora = "20:30", Sala = "8", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 443, Hora = "13:45", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 444, Hora = "16:00", Sala = "9", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 445, Hora = "18:45", Sala = "1", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 446, Hora = "14:30", Sala = "4", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 447, Hora = "17:00", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 448, Hora = "20:00", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 449, Hora = "22:30", Sala = "8", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Id = 450, Hora = "15:00", Sala = "3", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 451, Hora = "18:00", Sala = "6", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 452, Hora = "20:45", Sala = "9", EsISense = true, EsVOSE = false }
                            }
                        }
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 453, Hora = "17:00", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 454, Hora = "19:30", Sala = "2", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 455, Hora = "21:45", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 456, Hora = "23:00", Sala = "8", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 457, Hora = "16:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 458, Hora = "20:00", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 459, Hora = "22:15", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 460, Hora = "14:30", Sala = "4", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 461, Hora = "17:15", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 462, Hora = "19:45", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 463, Hora = "22:30", Sala = "6", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 464, Hora = "15:00", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 465, Hora = "18:00", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 466, Hora = "20:45", Sala = "3", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 467, Hora = "23:00", Sala = "7", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 468, Hora = "16:15", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 469, Hora = "18:45", Sala = "6", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 470, Hora = "21:00", Sala = "8", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 471, Hora = "14:00", Sala = "9", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 472, Hora = "16:30", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 473, Hora = "19:00", Sala = "5", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 474, Hora = "21:30", Sala = "8", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Id = 475, Hora = "15:00", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 476, Hora = "17:30", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 477, Hora = "20:00", Sala = "6", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 478, Hora = "22:30", Sala = "9", EsISense = false, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Torre Outlet", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 478, Hora = "14:00", Sala = "4", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 479, Hora = "16:30", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 480, Hora = "19:00", Sala = "1", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 481, Hora = "21:45", Sala = "3", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 482, Hora = "15:30", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 483, Hora = "18:00", Sala = "8", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 484, Hora = "20:30", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 485, Hora = "22:45", Sala = "7", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 486, Hora = "14:45", Sala = "5", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 487, Hora = "17:30", Sala = "4", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 488, Hora = "20:00", Sala = "6", EsISense = true, EsVOSE = false }
                            }
                        },
                    }
                },
                {
                    "Casco Antiguo", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 489, Hora = "13:45", Sala = "2", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 490, Hora = "16:00", Sala = "7", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 491, Hora = "18:30", Sala = "9", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 492, Hora = "21:00", Sala = "3", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 493, Hora = "14:15", Sala = "5", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 494, Hora = "16:45", Sala = "8", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 494, Hora = "19:15", Sala = "1", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 495, Hora = "21:45", Sala = "4", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 496, Hora = "15:00", Sala = "6", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 497, Hora = "17:45", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 498, Hora = "20:30", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 499, Hora = "14:30", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 500, Hora = "17:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 501, Hora = "19:30", Sala = "8", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 502, Hora = "13:45", Sala = "1", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 503, Hora = "16:15", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 504, Hora = "19:00", Sala = "6", EsISense = false, EsVOSE = true }
                            }
                        }
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
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 505, Hora = "15:00", Sala = "1", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 506, Hora = "17:15", Sala = "2", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 507, Hora = "19:30", Sala = "4", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 508, Hora = "14:30", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 509, Hora = "16:45", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 510, Hora = "19:00", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 511, Hora = "21:15", Sala = "7", EsISense = false, EsVOSE = false }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 512, Hora = "15:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 513, Hora = "17:30", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 514, Hora = "20:00", Sala = "5", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 515, Hora = "14:00", Sala = "1", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 516, Hora = "16:15", Sala = "6", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 517, Hora = "18:45", Sala = "8", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 518, Hora = "15:30", Sala = "7", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 519, Hora = "18:00", Sala = "4", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 520, Hora = "20:30", Sala = "9", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 521, Hora = "16:00", Sala = "3", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 522, Hora = "18:30", Sala = "5", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 523, Hora = "21:00", Sala = "2", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Id = 524, Hora = "14:30", Sala = "1", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 525, Hora = "17:00", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 526, Hora = "19:45", Sala = "8", EsISense = false, EsVOSE = true }
                            }
                        }
                    }
                },
                {
                    "Puerto Venecia", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 527, Hora = "16:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 528, Hora = "18:30", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 529, Hora = "20:45", Sala = "3", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 530, Hora = "15:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 531, Hora = "17:30", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 532, Hora = "20:00", Sala = "2", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 533, Hora = "14:45", Sala = "8", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 534, Hora = "17:00", Sala = "9", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 535, Hora = "19:15", Sala = "1", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 536, Hora = "15:30", Sala = "3", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 537, Hora = "18:00", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 538, Hora = "20:45", Sala = "7", EsISense = true, EsVOSE = false }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 539, Hora = "16:15", Sala = "4", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 540, Hora = "18:30", Sala = "2", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 541, Hora = "21:00", Sala = "8", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 542, Hora = "14:30", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 2, Hora = "17:15", Sala = "7", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 3, Hora = "20:00", Sala = "3", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Id = 543, Hora = "15:00", Sala = "9", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 544, Hora = "17:45", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 545, Hora = "20:30", Sala = "4", EsISense = true, EsVOSE = false }
                            }
                        }
                    }
                },
                {
                    "Torre Outlet", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 546, Hora = "13:30", Sala = "1", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 547, Hora = "16:00", Sala = "3", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 548, Hora = "18:30", Sala = "5", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 549, Hora = "14:15", Sala = "6", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 550, Hora = "16:45", Sala = "2", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 551, Hora = "19:00", Sala = "4", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 552, Hora = "15:00", Sala = "7", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 553, Hora = "18:00", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 554, Hora = "20:30", Sala = "1", EsISense = true, EsVOSE = true }
                            }
                        }
                        // Agrega más días y sesiones si es necesario
                    }
                },
                {
                    "Casco Antiguo", new Dictionary<string, List<Sesion>>
                    {
                        { "2024-11-11", new List<Sesion>
                            {
                                new Sesion { Id = 555, Hora = "14:00", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 556, Hora = "16:30", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 557, Hora = "19:00", Sala = "6", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-12", new List<Sesion>
                            {
                                new Sesion { Id = 558, Hora = "13:45", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 559, Hora = "16:15", Sala = "8", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 560, Hora = "18:45", Sala = "7", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-13", new List<Sesion>
                            {
                                new Sesion { Id = 561, Hora = "15:30", Sala = "3", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 562, Hora = "17:45", Sala = "1", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 563, Hora = "20:15", Sala = "9", EsISense = true, EsVOSE = false }
                            }
                        }, 
                        { "2024-11-14", new List<Sesion>
                            {
                                new Sesion { Id = 564, Hora = "14:30", Sala = "2", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 565, Hora = "17:00", Sala = "4", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 566, Hora = "19:30", Sala = "6", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-15", new List<Sesion>
                            {
                                new Sesion { Id = 567, Hora = "15:00", Sala = "5", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 568, Hora = "17:30", Sala = "7", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 569, Hora = "20:00", Sala = "9", EsISense = true, EsVOSE = true }
                            }
                        },
                        { "2024-11-16", new List<Sesion>
                            {
                                new Sesion { Id = 569, Hora = "14:45", Sala = "3", EsISense = false, EsVOSE = false },
                                new Sesion { Id = 570, Hora = "17:15", Sala = "8", EsISense = true, EsVOSE = true },
                                new Sesion { Id = 571, Hora = "19:45", Sala = "1", EsISense = false, EsVOSE = true }
                            }
                        },
                        { "2024-11-17", new List<Sesion>
                            {
                                new Sesion { Id = 572, Hora = "15:30", Sala = "6", EsISense = true, EsVOSE = false },
                                new Sesion { Id = 573, Hora = "18:00", Sala = "7", EsISense = false, EsVOSE = true },
                                new Sesion { Id = 574, Hora = "20:30", Sala = "9", EsISense = true, EsVOSE = true }
                            }
                        }
                    }
                }
            };
        }
    }
}