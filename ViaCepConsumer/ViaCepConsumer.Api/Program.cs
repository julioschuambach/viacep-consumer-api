using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
            builder.Services.AddDbContext<DatabaseContext>();
            builder.Services.AddControllers();
            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddTransient<IViaCepService, ViaCepService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            ConfigureAuthentication(builder);


            var app = builder.Build();
            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();
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
    }
}
