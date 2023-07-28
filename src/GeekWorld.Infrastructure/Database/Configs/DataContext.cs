using GeekWorld.Domain.Models;
using GeekWorld.Infrastructure.Database.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GeekWorld.Infrastructure.Database.Configs
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        private DataContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LikePost> LikePosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new LikePostMap());
            modelBuilder.ApplyConfiguration(new FriendshipMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
        }

    }
}
