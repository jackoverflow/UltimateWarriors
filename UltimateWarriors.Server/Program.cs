using Npgsql;
using Dapper;
using System.IO;
using UltimateWarriors.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register the repository
builder.Services.AddScoped<IUltimateWarriorsRepository, UltimateWarriorsRepository>();

// Configure CORS to allow requests from any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Move CORS before other middleware
app.UseCors("AllowAllOrigins");

// Reset and Initialize the database
ResetDatabase(app.Configuration.GetConnectionString("DefaultConnection"));

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Try using HTTP instead of HTTPS for local development
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

void ResetDatabase(string connectionString)
{
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
    }
    using var connection = new NpgsqlConnection(connectionString);
    // Drop existing tables in correct order (due to foreign key constraints)
    var dropTables = @"
        DROP TABLE IF EXISTS WarriorWeapon CASCADE;
        DROP TABLE IF EXISTS Warriors CASCADE;
        DROP TABLE IF EXISTS Weapons CASCADE;
    ";
    connection.Execute(dropTables);

    // Recreate tables using InitialSchema.sql
    var script = File.ReadAllText("Database/InitialSchema.sql");
    connection.Execute(script);
}
