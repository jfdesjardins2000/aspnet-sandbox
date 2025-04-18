using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewZealandWalks.API.Data;
using NewZealandWalks.API.MiddleWares;
using NewZealandWalks.API.Repositories;
using Serilog;
using System.Numerics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ////1. * *Premi�re approche(Static) * * :
        //Log.Logger = new LoggerConfiguration()
        //    .MinimumLevel.Information()
        //    .Enrich.FromLogContext()
        //    .WriteTo.Console()
        //    .WriteTo.File(
        //        path: "Logs/log-.log",
        //        rollingInterval: RollingInterval.Day,
        //        retainedFileCountLimit: 1,
        //        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
        //    .CreateLogger();

        //// Int�gration avec ASP.NET Core
        //builder.Host.UseSerilog();


        // 2. * *Deuxi�me approche(DI) * * :
        Serilog.Core.Logger logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                path: "Logs/log-.log",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 1,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);


        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        // Configuration de Swagger pour permettre l'Authentication JWT (Bearer Token)
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "NZ Walks API", Version = "v1" });
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = JwtBearerDefaults.AuthenticationScheme
                         },
                         Scheme = "Oauth2",
                         Name = JwtBearerDefaults.AuthenticationScheme,
                         In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        // R�cup�rer le chemin depuis appsettings.json
        string? connectionString = builder.Configuration.GetConnectionString("NZWalksConnectionString");

        // Configurer la cha�ne de connexion pour la BD SQLite Business
        builder.Services.AddDbContext<NZWalksDbContext>(options => options.UseSqlite(connectionString));

        // Configurer la cha�ne de connexion pour la BD SQLite Authentication
        string? authConnectionString = builder.Configuration.GetConnectionString("NZWalksAuthConnectionString");
        builder.Services.AddDbContext<NZWalksAuthDbContext>(options => options.UseSqlite(authConnectionString));

        // Ajout des Repository
        builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
        builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
        builder.Services.AddScoped<ITokenRepository, TokenRepository>();
        builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

        // Configuration du Service Identity
        builder.Services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NzWalks")
            .AddEntityFrameworkStores<NZWalksAuthDbContext>()
            .AddDefaultTokenProviders();

        // Configuration de l'Identite (password)
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;

            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;

            options.SignIn.RequireConfirmedEmail = true;
            options.Password.RequiredUniqueChars = 1;
        });

        // Ajout de l'Authentication JWT (Version 1)
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            });

        WebApplication app = builder.Build();

        // La Section qui suit sert a configurer les MiddleWare

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<GlobalExceptionHandler>();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        //Permet de pouvoir servir des objets static tel que des images  trouv� dans le
        //dossier C:\Logiciels\_SampleDev\LearnAspNetCore\aspnet-sandbox\NewZealandWalks\NewZealandWalks.API\Images\
        // par exemple : https://localhost:7252/Images/JFD.jpg
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
            RequestPath = "/Images"
        });

        app.MapControllers();

        app.Run();
    }
}