using GeekWorld.Application.Contracts.Dtos.Requests.Comments;
using GeekWorld.Application.Contracts.Dtos.Responses.Comments;

namespace GeekWorld.Application.Contracts.Services
{
    public interface ICommentService
    {
        public Task AddCommentOnPost(CreateCommentRequest request, Guid postId, Guid authorId);

        public Task<IEnumerable<CommentResponse>> GetCommentsByPost(Guid postId);
    }
}
