using CineAPI.Repositories;
using CineAPI.Repositories.Interfaces;
using CineAPI.Services;
using CineAPI.Services.Interfaces;
using Microsoft.Extensions.Configuration;  // Asegúrate de incluir esta librería
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
    builder.Services.AddScoped<IMovieService>(provider =>
        new MovieService(provider.GetRequiredService<IMovieRepository>()));

    // Configuración del repositorio y servicios para usuarios
    builder.Services.AddScoped<IUserRepository>(provider =>
        new UserRepository(postgresConnection));
    builder.Services.AddScoped<IUserService>(provider =>
        new UserService(provider.GetRequiredService<IUserRepository>()));
}
else
{
    throw new InvalidOperationException($"Proveedor de base de datos no reconocido: {databaseProvider}");
}

// Configuración de controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cine API", Version = "v1" });
});

// Configurar la autenticación y autorización por cookies
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/login";  
        options.LogoutPath = "/logout";  
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cine API v1");
        c.RoutePrefix = string.Empty; // Esto coloca Swagger en la raíz de la aplicación
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();  
app.UseAuthorization();
app.MapControllers();

// Ejecutar la aplicación
app.Run();
