using GeekWorld.Application.Contracts.Dtos.Requests.Posts;
using GeekWorld.Application.Contracts.Dtos.Responses.Posts;
using GeekWorld.Domain.Models;

namespace GeekWorld.Application.Contracts.Services
{
    public interface IPostService
    {
        Task<PostResponse> Add(CreatePostRequest request, Guid authorId);

        Task<PostResponse> GetById(Guid id);

        Task<IEnumerable<PostResponse>> GetAll(Guid meId, int page, int limit);

        Task<IEnumerable<PostResponse>> GetPostsByUser(Guid meId, Guid userId, int page, int limit);

        Task<LikePostResponse> AddLikePost(Guid authorId, Guid postId, AddLikePostRequest request);
    }
}
