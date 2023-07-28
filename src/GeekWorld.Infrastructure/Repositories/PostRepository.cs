using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Enums;
using GeekWorld.Domain.Models;
using GeekWorld.Infrastructure.Database.Configs;
using Microsoft.EntityFrameworkCore;

namespace GeekWorld.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Post?> Add(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public void AddLikePost(LikePost likePost)
        {
            _context.LikePosts.Add(likePost);
            _context.SaveChanges();
        }

        public async Task<Post?> GetById(Guid id)
        {
            Post? post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return null;
            }

            return post;
        }

        public async Task<IEnumerable<Post>> GetPostsByUser(Guid meId, Guid userId, int page, int limit)
        {
            var friendshipExists = await _context.Friendships
                .AnyAsync(f => (f.UserId == meId && f.FriendId == userId) || (f.UserId == userId && f.FriendId == meId));

            var query = _context.Posts.Include(p => p.Author)
                .Where(p => p.AuthorId == userId && (friendshipExists || p.Visibility == Visibility.PUBLIC.ToString()))
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserAndFriends(Guid meId, int page, int limit)
        {
            var user = await _context.Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id == meId);


            if (user == null)
            {
                return Enumerable.Empty<Post>();
            }

            var friendIds = user.Friends.Select(f => f.FriendId).ToList();
            friendIds.Add(meId);

            var query = _context.Posts
                .Where(p => friendIds.Contains(p.AuthorId))
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit);

            return await query.ToListAsync();
        }

    }
}
