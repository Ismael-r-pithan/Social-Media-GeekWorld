using AutoMapper;
using GeekWorld.Application.Contracts.Dtos.Requests.Posts;
using GeekWorld.Application.Contracts.Dtos.Responses.Posts;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Models;

namespace GeekWorld.Application.Implementations
{
    public class PostServiceImp : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public PostServiceImp(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<PostResponse> Add(CreatePostRequest request, Guid authorId)
        {

            Post post = _mapper.Map<Post>(request);

            PostResponse response = _mapper.Map<PostResponse>(post);

            try
            {
                User? author = await _userRepository.GetById(authorId);

                if (author == null)
                {
                    response.AddNotification(new Validations.Notification("Author do Post não foi encontrado"));
                    return response;
                }

                post.AddAuthor(author);

                await _postRepository.Add(post);
                return response;
            } catch
            {
                response.AddNotification(new Validations.Notification("Servidor indisponível no momento, tente novamente mais tarde"));
                return response;
            }
        }

        public async Task<LikePostResponse> AddLikePost(Guid authorId, Guid postId, AddLikePostRequest request)
        {

            LikePostResponse response = new();
            LikePost likePost = _mapper.Map<LikePost>(request);
            User? author = await _userRepository.GetById(authorId);
            Post? post = await _postRepository.GetById(postId);
            if (post == null)
            {
                response.AddNotification(new Validations.Notification("Post foi removido"));
                return response;
            }
            if (author == null)
            {
                response.AddNotification(new Validations.Notification("Seu Usuário não foi encontrado, enctre em contato com supp@email.com"));
                return response;
            }
            likePost.AddLikePost(post, author);
            _postRepository.AddLikePost(likePost);

            response = _mapper.Map<LikePostResponse>(likePost);
            return response;
        }

        public async Task<IEnumerable<PostResponse>> GetAll(Guid meId, int page, int limit)
        {
            var posts = await _postRepository.GetPostsByUserAndFriends(meId, page, limit);
            IEnumerable<PostResponse> response = posts.Select(post => _mapper.Map<PostResponse>(post));

            return response;
        }

        public Task<PostResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostResponse>> GetPostsByUser(Guid meId, Guid userId, int page, int limit)
        {
            var posts = await _postRepository.GetPostsByUser(meId, userId, page, limit);
            IEnumerable<PostResponse> response = posts.Select(post => _mapper.Map<PostResponse>(post));

            return response;
        }
    }
}
