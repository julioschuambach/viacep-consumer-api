using Microsoft.EntityFrameworkCore;
using ViaCepConsumer.Api.Entities;
using ViaCepConsumer.Api.Infrastructure.Mappings;

namespace ViaCepConsumer.Api.Infrastructure.Contexts
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString = "Server = localhost, 1433; Database = ViaCepConsumer; User ID = sa; Password = 1q2w3e4r@#$;";
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
        }
    }
}
