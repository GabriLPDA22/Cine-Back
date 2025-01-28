using Npgsql;
using cine_web_app.back_end.Models;
using CineAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CineAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string _connectionString;

        public MovieRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("La cadena de conexión no puede ser nula o vacía.");
            }

            _connectionString = connectionString;
        }

        public async Task AddAsync(Movies movie)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    INSERT INTO Movies (
                        Titulo, Genero, Duracion, Clasificacion, Idioma, Sinopsis, FechaEstreno, Director, Actores, 
                        Portada, Banner, EdadRecomendada, ImagenEdadRecomendada, EnCartelera, EnVentaAnticipada
                    ) 
                    VALUES (
                        @Titulo, @Genero, @Duracion, @Clasificacion, @Idioma, @Sinopsis, @FechaEstreno, @Director, @Actores, 
                        @Portada, @Banner, @EdadRecomendada, @ImagenEdadRecomendada, @EnCartelera, @EnVentaAnticipada
                    )";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    AddMovieParameters(command, movie);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int peliculaID)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Movies WHERE PeliculaID = @PeliculaID";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PeliculaID", peliculaID);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<Movies>> GetAllAsync()
        {
            var movies = new List<Movies>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Movies";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Mapeo directo sin MapReaderToMovie
                            var movie = new Movies
                            {
                                PeliculaID = reader.GetInt32(reader.GetOrdinal("PeliculaID")),
                                Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                Genero = reader.GetString(reader.GetOrdinal("Genero")),
                                Duracion = reader.GetInt32(reader.GetOrdinal("Duracion")),
                                Clasificacion = reader.GetString(reader.GetOrdinal("Clasificacion")),
                                Idioma = reader.GetString(reader.GetOrdinal("Idioma")),
                                Sinopsis = reader.GetString(reader.GetOrdinal("Sinopsis")),
                                FechaEstreno = reader.GetDateTime(reader.GetOrdinal("FechaEstreno")),
                                Director = reader.GetString(reader.GetOrdinal("Director")),
                                Actores = reader.GetString(reader.GetOrdinal("Actores")),
                                Portada = reader.GetString(reader.GetOrdinal("Portada")),
                                Banner = reader.GetString(reader.GetOrdinal("Banner")),
                                EdadRecomendada = reader.GetInt32(reader.GetOrdinal("EdadRecomendada")),
                                ImagenEdadRecomendada = reader.GetString(reader.GetOrdinal("ImagenEdadRecomendada")),
                                EnCartelera = reader.GetBoolean(reader.GetOrdinal("EnCartelera")),
                                EnVentaAnticipada = reader.GetBoolean(reader.GetOrdinal("EnVentaAnticipada"))
                            };
                            movies.Add(movie);
                        }
                    }
                }
            }
            return movies;
        }

        public async Task<Movies?> GetByIdAsync(int peliculaID)
        {
            Movies? movie = null;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Movies WHERE PeliculaID = @PeliculaID";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PeliculaID", peliculaID);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Mapeo directo sin MapReaderToMovie
                            movie = new Movies
                            {
                                PeliculaID = reader.GetInt32(reader.GetOrdinal("PeliculaID")),
                                Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                Genero = reader.GetString(reader.GetOrdinal("Genero")),
                                Duracion = reader.GetInt32(reader.GetOrdinal("Duracion")),
                                Clasificacion = reader.GetString(reader.GetOrdinal("Clasificacion")),
                                Idioma = reader.GetString(reader.GetOrdinal("Idioma")),
                                Sinopsis = reader.GetString(reader.GetOrdinal("Sinopsis")),
                                FechaEstreno = reader.GetDateTime(reader.GetOrdinal("FechaEstreno")),
                                Director = reader.GetString(reader.GetOrdinal("Director")),
                                Actores = reader.GetString(reader.GetOrdinal("Actores")),
                                Portada = reader.GetString(reader.GetOrdinal("Portada")),
                                Banner = reader.GetString(reader.GetOrdinal("Banner")),
                                EdadRecomendada = reader.GetInt32(reader.GetOrdinal("EdadRecomendada")),
                                ImagenEdadRecomendada = reader.GetString(reader.GetOrdinal("ImagenEdadRecomendada")),
                                EnCartelera = reader.GetBoolean(reader.GetOrdinal("EnCartelera")),
                                EnVentaAnticipada = reader.GetBoolean(reader.GetOrdinal("EnVentaAnticipada"))
                            };
                        }
                    }
                }
            }
            return movie;
        }

        public async Task UpdateAsync(Movies movie)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    UPDATE Movies 
                    SET 
                        Titulo = @Titulo, Genero = @Genero, Duracion = @Duracion, Clasificacion = @Clasificacion, 
                        Idioma = @Idioma, Sinopsis = @Sinopsis, FechaEstreno = @FechaEstreno, Director = @Director, 
                        Actores = @Actores, Portada = @Portada, Banner = @Banner, 
                        EdadRecomendada = @EdadRecomendada, ImagenEdadRecomendada = @ImagenEdadRecomendada, 
                        EnCartelera = @EnCartelera, EnVentaAnticipada = @EnVentaAnticipada
                    WHERE PeliculaID = @PeliculaID";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    AddMovieParameters(command, movie);
                    command.Parameters.AddWithValue("@PeliculaID", movie.PeliculaID);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private void AddMovieParameters(NpgsqlCommand command, Movies movie)
        {
            command.Parameters.AddWithValue("@Titulo", movie.Titulo);
            command.Parameters.AddWithValue("@Genero", movie.Genero);
            command.Parameters.AddWithValue("@Duracion", movie.Duracion);
            command.Parameters.AddWithValue("@Clasificacion", movie.Clasificacion);
            command.Parameters.AddWithValue("@Idioma", movie.Idioma);
            command.Parameters.AddWithValue("@Sinopsis", movie.Sinopsis);
            command.Parameters.AddWithValue("@FechaEstreno", movie.FechaEstreno);
            command.Parameters.AddWithValue("@Director", movie.Director);
            command.Parameters.AddWithValue("@Actores", movie.Actores);
            command.Parameters.AddWithValue("@Portada", movie.Portada);
            command.Parameters.AddWithValue("@Banner", movie.Banner);
            command.Parameters.AddWithValue("@EdadRecomendada", movie.EdadRecomendada);
            command.Parameters.AddWithValue("@ImagenEdadRecomendada", movie.ImagenEdadRecomendada);
            command.Parameters.AddWithValue("@EnCartelera", movie.EnCartelera);
            command.Parameters.AddWithValue("@EnVentaAnticipada", movie.EnVentaAnticipada);
        }
    }
}
