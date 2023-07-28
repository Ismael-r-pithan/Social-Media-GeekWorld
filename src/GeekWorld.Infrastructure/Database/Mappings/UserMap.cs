using GeekWorld.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekWorld.Infrastructure.Database.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id")
                .HasColumnType("VARCHAR")
                .IsRequired();
                
            builder.Property(x => x.FullName).HasColumnName("full_name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Email).HasColumnName("email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Nickname).HasColumnName("nickname")
                .HasColumnType("VARCHAR")
                .HasDefaultValue("")
                .HasMaxLength(50);

            builder.Property(x => x.DateOfBirth).HasColumnName("date_of_birth")
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(x => x.CEP).HasColumnName("cep")
                .HasColumnType("VARCHAR")
                .HasMaxLength(8)
                .IsRequired();

            builder.Property(x => x.Password).HasColumnName("password")
                .HasColumnType("VARCHAR")
                .HasMaxLength(128);

            builder.Property(x => x.ImageProfile).HasColumnName("image_profile")
                .HasDefaultValue("")
                .HasColumnType("VARCHAR")
                .HasMaxLength(512);

        }
    }
}
