using GeekWorld.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekWorld.Infrastructure.Database.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id")
                .HasColumnType("VARCHAR")
                .IsRequired();

            builder.Property(x => x.Content).HasColumnName("content")
                .HasColumnType("VARCHAR")
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(x => x.Visibility).HasColumnName("visibility")
                .HasColumnType("VARCHAR")
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(x => x.CreatedAt).HasColumnName("created_at")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(x => x.DeletedAt).HasColumnName("deleted_at")
                .HasColumnType("DATETIME");
           
            builder.Property(x => x.AuthorId).HasColumnName("author_id")
                .HasColumnType("VARCHAR")
                .IsRequired();



            builder.HasOne(x => x.Author).WithMany(x => x.Posts).HasForeignKey(x => x.AuthorId);
        }

    }
}