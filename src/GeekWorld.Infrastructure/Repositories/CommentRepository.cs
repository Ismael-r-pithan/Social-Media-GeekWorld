using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Models;
using GeekWorld.Infrastructure.Database.Configs;
using Microsoft.EntityFrameworkCore;

namespace GeekWorld.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddCommentOnPost(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Comment>> GetCommentsByPost(Guid postId)
         {
            return await _context.Comments.Include(c => c.Author)
                      .Where(c => c.PostId == postId).ToListAsync();
         }
    }
}