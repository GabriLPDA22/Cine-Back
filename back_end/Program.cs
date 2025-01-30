using CineAPI.Repositories;
using CineAPI.Repositories.Interfaces;
using CineAPI.Services;
using CineAPI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Cargar configuración desde appsettings.json
var configuration = builder.Configuration;

// Leer el proveedor de base de datos
var databaseProvider = configuration["DatabaseProvider"] ?? "PostgreSQL"; // Por defecto PostgreSQL

// Leer la cadena de conexión desde appsettings.json
var postgresConnection = configuration.GetConnectionString("CineDB_PostgreSQL");

// Verificar si la cadena de conexión es válida
if (string.IsNullOrEmpty(postgresConnection))
{
    throw new InvalidOperationException("La cadena de conexión de la base de datos no está configurada. Verifica tu archivo appsettings.json.");
}

// Imprimir la cadena de conexión para verificar si está bien cargada
Console.WriteLine($"DATABASE_URL: {postgresConnection}");

// Configurar los servicios según el proveedor seleccionado
if (databaseProvider == "PostgreSQL")
{
    Console.WriteLine("Usando PostgreSQL...");

    // Configuración del repositorio y servicios para películas
    builder.Services.AddScoped<IMovieRepository>(provider =>
        new MovieRepository(postgresConnection));
}

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5173",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configurar el pipeline de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

// Usar la política de CORS configurada
app.UseCors("AllowLocalhost5173");

app.UseAuthorization();

app.MapControllers();

app.Run();