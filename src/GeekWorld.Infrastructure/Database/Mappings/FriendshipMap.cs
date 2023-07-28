using GeekWorld.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekWorld.Infrastructure.Database.Mappings
{
    public class FriendshipMap : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ToTable("Friendship");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id")
               .HasColumnType("VARCHAR")
               .IsRequired();

            builder.Property(x => x.UserId).HasColumnName("user_id")
                .HasColumnType("VARCHAR")
                .IsRequired();

            builder.Property(x => x.FriendId).HasColumnName("friend_id")
                .HasColumnType("VARCHAR")
                .IsRequired();



            builder.HasOne(x => x.User)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Friend)
                .WithMany()
                .HasForeignKey(x => x.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
