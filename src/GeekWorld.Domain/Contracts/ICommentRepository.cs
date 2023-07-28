using GeekWorld.Domain.Models;

namespace GeekWorld.Domain.Contracts
{
    public interface ICommentRepository
    {
        Task AddCommentOnPost(Comment comment);

        Task<IEnumerable<Comment>> GetCommentsByPost(Guid postId);
    }
}
