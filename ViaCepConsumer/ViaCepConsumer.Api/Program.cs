using ViaCepConsumer.Api.Infrastructure.Contexts;

namespace ViaCepConsumer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DatabaseContext>();
            builder.Services.AddControllers();

            var app = builder.Build();
            app.MapControllers();
            app.Run();
        }
    }
}
