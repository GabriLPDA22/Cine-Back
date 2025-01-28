using CineAPI.Repositories;
using CineAPI.Repositories.Interfaces;
using CineAPI.Services;
using CineAPI.Services.Interfaces;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Obtener el proveedor de base de datos desde la configuración
var databaseProvider = builder.Configuration["DatabaseProvider"] ?? "PostgreSQL"; // Por defecto, PostgreSQL

// Obtener las cadenas de conexión desde appsettings.json
var postgresConnection = builder.Configuration.GetConnectionString("CineDB");

// Validar la configuración del proveedor
if (string.IsNullOrEmpty(databaseProvider))
{
    throw new InvalidOperationException("El proveedor de base de datos no está configurado en appsettings.json.");
}

// Configurar los servicios según el proveedor seleccionado
if (databaseProvider == "PostgreSQL")
{
    Console.WriteLine("Usando PostgreSQL...");
    builder.Services.AddScoped<IMovieRepository>(provider =>
        new MovieRepository(postgresConnection!));
    builder.Services.AddScoped<IMovieService>(provider =>
        new MovieService(provider.GetRequiredService<IMovieRepository>()));
}
else
{
    throw new InvalidOperationException($"Proveedor de base de datos no reconocido: {databaseProvider}");
}

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
