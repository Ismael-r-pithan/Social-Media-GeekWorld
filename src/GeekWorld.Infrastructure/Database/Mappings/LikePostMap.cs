using GeekWorld.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekWorld.Infrastructure.Database.Mappings
{
    public class LikePostMap : IEntityTypeConfiguration<LikePost>
    {
        public void Configure(EntityTypeBuilder<LikePost> builder)
        {
            builder.ToTable("LikePost");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id")
                .HasColumnType("VARCHAR")
                .IsRequired();

            builder.Property(x => x.Value).HasColumnName("value")
                .HasColumnType("BOOLEAN")
                .IsRequired();

            builder.Property(x => x.UserId).HasColumnName("user_id")
                .HasColumnType("VARCHAR")
                .IsRequired();

            builder.Property(x => x.PostId).HasColumnName("post_id")
                .HasColumnType("VARCHAR")
                .IsRequired();



            builder.HasOne(x => x.User).WithMany(x => x.LikePosts).HasForeignKey(x => x.UserId);
            
            builder.HasOne(x => x.Post).WithMany(x => x.LikePosts).HasForeignKey(x => x.PostId);
        }
    }
}