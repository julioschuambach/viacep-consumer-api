using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaCepConsumer.Api.Entities;

namespace ViaCepConsumer.Api.Infrastructure.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureTable(builder);
            ConfigurePrimaryKeys(builder);
            ConfigureProperties(builder);
            ConfigureIndexes(builder);
        }

        private void ConfigureTable(EntityTypeBuilder<User> builder)
            => builder.ToTable("Users");

        private void ConfigurePrimaryKeys(EntityTypeBuilder<User> builder)
            => builder.HasKey(x => x.Id)
                      .HasName("PK_Users_Id");

        private void ConfigureProperties(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id)
                   .HasColumnName("Id")
                   .HasColumnType("UNIQUEIDENTIFIER")
                   .IsRequired();

            builder.Property(x => x.Username)
                   .HasColumnName("Username")
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Email)
                   .HasColumnName("Email")
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Password)
                   .HasColumnName("Password")
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(x => x.CreatedDate)
                   .HasColumnName("CreatedDate")
                   .HasColumnType("DATETIME")
                   .IsRequired();

            builder.Property(x => x.LastUpdatedDate)
                   .HasColumnName("LastUpdatedDate")
                   .HasColumnType("DATETIME")
                   .IsRequired();

            builder.HasMany(x => x.Roles);
        }

        private void ConfigureIndexes(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.Id, "IX_Users_Id")
                   .IsUnique();

            builder.HasIndex(x => x.Username, "IX_Users_Username")
                   .IsUnique();

            builder.HasIndex(x => x.Email, "IX_Users_Email")
                   .IsUnique();
        }
    }
}
