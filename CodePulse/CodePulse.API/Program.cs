using CodePulse.API.Data;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IConfigurationRoot configuration = ObtenirConfiguration();
        builder.Configuration.AddConfiguration(configuration);

        // Provient de ASPNETCORE_ENVIRONMENT dans launchSettings.json selon le profil d'execution qu'on choisi.
        string env = builder.Environment.EnvironmentName;

        builder.Logging.ClearProviders();
        Serilog.Core.Logger logger = ConfigureLogging(configuration);
        logger.Information("builder.Environment: {Env}", env);
        builder.Logging.AddSerilog(logger);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "CodePulse.API", Version = "v1", Description = $"Environnement: {builder.Environment.EnvironmentName}" });
        });

        // Récupérer le chemin depuis appsettings.json
        string? connectionString = builder.Configuration.GetConnectionString("CodePulseConnectionString");

        // Configurer la chaîne de connexion pour la BD SQLite Business
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));


        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

        WebApplication app = builder.Build();

        // On active Swagger UI peu importe l'environnement
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(options =>
        {
            options.AllowAnyHeader();
            options.AllowAnyOrigin();
            options.AllowAnyMethod();
        });

        //app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static IConfigurationRoot ObtenirConfiguration()
    {
        // permet de lancer  l'executable de n'importe où sans etre dans le repertoire qui contient l'executable et qu'il reconnaisse le appsettings.json
        var executingAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (executingAssemblyLocation == null)
        {
            throw new InvalidOperationException("Impossible de déterminer l'emplacement de l'assembly en cours d'exécution.");
        }
        string appSettingsPath = Path.Combine(executingAssemblyLocation, "appsettings.json");

        IConfigurationRoot configuration = new ConfigurationBuilder()
          .AddJsonFile(appSettingsPath)
          .Build();

        return configuration;
    }

    private static Serilog.Core.Logger ConfigureLogging(IConfigurationRoot configuration)
    {
        Serilog.Core.Logger logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        logger.Information("=== CodePulse.API.exe ===");
        logger.Debug("Chemin fichier Log : {LogPath}", configuration["Serilog:WriteTo:0:Args:path"]);

        return logger;
    }
}