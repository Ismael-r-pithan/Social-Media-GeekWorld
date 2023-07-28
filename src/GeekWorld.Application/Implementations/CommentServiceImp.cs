using AutoMapper;
using GeekWorld.Application.Contracts.Dtos.Requests.Comments;
using GeekWorld.Application.Contracts.Dtos.Responses.Comments;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Models;


namespace GeekWorld.Application.Implementations
{
    public class CommentServiceImp : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentServiceImp(ICommentRepository commentRepository, IMapper mapper, IPostRepository postRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task AddCommentOnPost(CreateCommentRequest request, Guid postId, Guid authorId)
        {
            Comment comment = _mapper.Map<Comment>(request);
            var author = await _userRepository.GetById(authorId);
            var post = await _postRepository.GetById(postId);

            if (author != null && post != null)
            {
                comment.CommentPost(author, post);
                await _commentRepository.AddCommentOnPost(comment);
            }
        }

        public async Task<IEnumerable<CommentResponse>> GetCommentsByPost(Guid postId)
        {
            IEnumerable<Comment> comments =  await _commentRepository.GetCommentsByPost(postId);
            IEnumerable<CommentResponse> response = comments.Select(comment => _mapper.Map<CommentResponse>(comment));

            return response;
        }
    }
}
