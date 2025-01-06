using CodePulse.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Runtime;

/*
 Dans l'arborescence d'un projet ASP.NET Core, il est important de bien organiser les classes
 pour maintenir une structure claire et modulaire. La classe MySettings, qui sert à représenter
 les paramètres de configuration, devrait être placée dans un dossier approprié.

Organisation Typique
Voici une structure recommandée pour votre projet :

 /YourProject
│
├── /Controllers
│   └── HomeController.cs
│
├── /Models
│   └── MySettings.cs
│
├── /Properties
│   └── launchSettings.json
│
├── appsettings.json
├── appsettings.Development.json
├── appsettings.Staging.json
├── appsettings.Production.json
├── Program.cs
├── YourProject.csproj


Vous pouvez injecter les paramètres configurés dans un contrôleur ou un service via IOptions<MySettings> :

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly MySettings _mySettings;

        public HomeController(IOptions<MySettings> mySettings)
        {
            _mySettings = mySettings.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { ConnectionString = _mySettings.ConnectionString });
        }
    }

 */

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Console.WriteLine($"builder.Environment: {builder.Environment.EnvironmentName}");

        builder.Configuration
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();

        // Add services to the container.
        builder.Services.AddControllers();

        // Référence à la classe MySettings
        builder.Services.Configure<MySettings>(builder.Configuration.GetSection(nameof(MySettings)));

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "CodePulse.API", Version = "v1", Description = $"Environnement: {builder.Environment.EnvironmentName}" });
        });

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //builder.Services.AddOpenApi();

        WebApplication app = builder.Build();

        // Configuration spécifique :
        Console.WriteLine($"Environnement est : {app.Environment.EnvironmentName}");

        // Obtenir la valeur de la chaîne de connexion à partir de la configuration
        MySettings? mySettings = app.Configuration.GetSection(nameof(MySettings)).Get<MySettings>();
        Console.WriteLine($"ConnectionString {mySettings?.ConnectionString}");



        //// Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.MapOpenApi();
        //}

        // Condiguration de base : https://localhost:7015/swagger/index.html
        //if (app.Environment.IsDevelopment()) // Swagger UI est uniquement activé en mode développement
        //{
        //    Console.WriteLine($"ConnectionString {mySettings?.ConnectionString}");
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}

        // On active Swagger UI peu importe l'environnement
        app.UseSwagger();
        app.UseSwaggerUI();

        //// Configure the HTTP request pipeline.
        //// Configuration spécifique :
        //// Avec la configuration suivante dans votre pipeline HTTP,
        //// vous pouvez accéder à l'interface utilisateur Swagger à la racine de votre application Web
        //// Points Clés dans app.UseSwaggerUI:
        //// c.RoutePrefix = string.Empty : Cela signifie que l'UI Swagger remplace la page d'accueil par défaut de l'application.
        //// c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodePulse.API v1") : Cela signifie que l'UI Swagger charge la spécification Swagger JSON à partir de /swagger/v1/swagger.json.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger(); // Expose the Swagger JSON endpoint
        //    app.UseSwaggerUI(options => // Serve the Swagger UI
        //    {
        //        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CodePulse.API v1"); // Adresse du endpoint JSON de Swagger est toujours disponible à https://localhost:7015/swagger/v1/swagger.json
        //        options.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root: https://localhost:7015/index.html
        //    });
        //}

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}