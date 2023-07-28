using GeekWorld.Domain.Models;

namespace GeekWorld.Domain.Contracts
{
    public interface IPostRepository
    {
        Task<Post?> Add(Post post);

        Task<Post?> GetById(Guid id);

        Task<IEnumerable<Post>> GetPostsByUserAndFriends(Guid meId, int page, int limit);

        Task<IEnumerable<Post>> GetPostsByUser(Guid meId, Guid userId, int page, int limit);

        void AddLikePost(LikePost likePost);
    }
}
