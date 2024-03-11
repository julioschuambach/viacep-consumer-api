using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaCepConsumer.Api.Entities;

namespace ViaCepConsumer.Api.Infrastructure.Mappings
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            ConfigureTable(builder);
            ConfigurePrimaryKeys(builder);
            ConfigureProperties(builder);
            ConfigureIndexes(builder);
        }

        private void ConfigureTable(EntityTypeBuilder<Role> builder)
            => builder.ToTable("Roles");

        private void ConfigurePrimaryKeys(EntityTypeBuilder<Role> builder)
            => builder.HasKey(x => x.Id)
                      .HasName("PK_Roles_Id");

        private void ConfigureProperties(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .HasColumnType("UNIQUEIDENTIFIER")
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasColumnName("Name")
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasMany(x => x.Users);
        }

        private void ConfigureIndexes(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(x => x.Id, "IX_Roles_Id")
                   .IsUnique();

            builder.HasIndex(x => x.Name, "IX_Roles_Name")
                   .IsUnique();
        }
    }
}
