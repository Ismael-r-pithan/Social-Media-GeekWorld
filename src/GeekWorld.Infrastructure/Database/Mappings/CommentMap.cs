using GeekWorld.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekWorld.Infrastructure.Database.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content).HasColumnName("content")
                .HasColumnType("VARCHAR")
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(x => x.CreatedAt).HasColumnName("created_at")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(x => x.AuthorId).HasColumnName("author_id")
                .HasColumnType("VARCHAR")
                .IsRequired();

            builder.Property(x => x.PostId).HasColumnName("post_id")
                .HasColumnType("VARCHAR")
                .IsRequired();



            builder.HasOne(x => x.Author).WithMany(x => x.Comments).HasForeignKey(x => x.AuthorId);

            builder.HasOne(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId);
        }
    }
}
