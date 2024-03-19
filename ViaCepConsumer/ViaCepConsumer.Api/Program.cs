using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ViaCepConsumer.Api.Infrastructure.Contexts;
using ViaCepConsumer.Api.Infrastructure.Repositories;
using ViaCepConsumer.Api.Infrastructure.Repositories.Interfaces;
using ViaCepConsumer.Api.Services;
using ViaCepConsumer.Api.Services.Interfaces;

namespace ViaCepConsumer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);

            var app = builder.Build();
            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();
            ApplyMigrations(app);
            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            ConfigureDependencies(builder);
            ConfigureAuthentication(builder);
            ConfigureRedisCaching(builder);
            ConfigureSwagger(builder);
        }

        private static void ConfigureDependencies(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DatabaseContext>();

            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddTransient<IViaCepService, ViaCepService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ICachingService, CachingService>();
            builder.Services.AddTransient<IEncryptorService, EncryptorService>();
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            byte[] key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtKey").Value);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private static void ConfigureRedisCaching(WebApplicationBuilder builder)
        {
            builder.Services.AddStackExchangeRedisCache(
                options =>
                {
                    options.InstanceName = "redis";
                    options.Configuration = "redis";
                });
        }

        private static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "ViaCEP consumer ASP.NET Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Julio Eduardo Schuambach",
                        Url = new Uri("https://github.com/julioschuambach"),
                        Email = "julioschuambach.dev@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/license/mit/")
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "After registering and logging in, copy your bearer token and paste it like in the example below:" + "<br>" +
                                  "Example: <strong>Bearer 1234567890abcdefghijklmnopqrstuvwxyz</strong><br><br>",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        }, new List<string>()
                    }
                });
            });
        }

        private static void ApplyMigrations(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DatabaseContext>();

            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
        }
    }
}
