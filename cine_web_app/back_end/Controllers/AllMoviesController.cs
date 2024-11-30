using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using cine_web_app.back_end.Models;
using cine_web_app.back_end.Services;

namespace cine_web_app.back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllMoviesController : ControllerBase
    {
        private readonly CineService _cineService;

        public AllMoviesController(CineService cineService)
        {
            _cineService = cineService;
        }

        // Endpoint para obtener todas las películas
        [HttpGet("All")]
        public IActionResult GetAllMovies()
        {
            var peliculas = _cineService.ObtenerTodasLasPeliculas();
            return Ok(peliculas);
        }

        // Endpoint para obtener películas en cartelera
        [HttpGet("Cartelera")]
        public IActionResult GetMoviesEnCartelera()
        {
            var peliculas = _cineService.ObtenerPeliculasEnCartelera();
            return Ok(peliculas);
        }

        // Endpoint para obtener películas en venta anticipada
        [HttpGet("PreSale")]
        public IActionResult GetPreSaleMovies()
        {
            var peliculas = _cineService.ObtenerPeliculasEnVentaAnticipada();
            return Ok(peliculas);
        }

        // Endpoint para obtener películas próximas
        [HttpGet("ComingSoon")]
        public IActionResult GetComingSoonMovies()
        {
            var peliculas = _cineService.ObtenerPeliculasProximas();
            return Ok(peliculas);
        }
    }
}
