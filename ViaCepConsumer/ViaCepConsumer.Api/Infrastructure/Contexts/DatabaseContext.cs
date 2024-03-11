using Microsoft.EntityFrameworkCore;
using ViaCepConsumer.Api.Entities;
using ViaCepConsumer.Api.Infrastructure.Mappings;

namespace ViaCepConsumer.Api.Infrastructure.Contexts
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DatabaseContext(IConfiguration configuration)
            => _configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServer"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
        }
    }
}
