using Microsoft.EntityFrameworkCore;
using NewZealandWalks.API;
using NewZealandWalks.API.Data;
using NewZealandWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Récupérer le chemin depuis appsettings.json
string? connectionString = builder.Configuration.GetConnectionString("NZWalksConnectionString");
// Configurer la chaîne de connexion pour SQLite
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlite(connectionString));

// Ajout des Repository
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
