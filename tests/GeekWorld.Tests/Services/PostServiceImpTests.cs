using AutoMapper;
using GeekWorld.Application.Contracts.Dtos.Requests.Posts;
using GeekWorld.Application.Contracts.Dtos.Responses.Posts;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Models;
using Moq;

namespace GeekWorld.Application.Implementations.Tests
{
    public class PostServiceTests
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPostService _postService;

        public PostServiceTests()
        {
            _postRepository = Mock.Of<IPostRepository>();
            _mapper = Mock.Of<IMapper>();
            _userRepository = Mock.Of<IUserRepository>();
            _postService = new PostServiceImp(_postRepository, _mapper, _userRepository);
        }



        [Fact]
        public async Task AddLikePost_ShouldReturnLikePostResponse_WhenAuthorAndPostExist()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var postId = Guid.NewGuid();
            var request = new AddLikePostRequest();
            var author = new User("John Doe", "john@example.com", "johndoe", DateTime.Now, "12345", "password", "image.jpg");
            var post = new Post("Test post content", "Public", DateTime.Now, authorId, author);
            var likePost = new LikePost();

            var expectedResponse = _mapper.Map<LikePostResponse>(likePost);

            Mock.Get(_userRepository)
                .Setup(repo => repo.GetById(authorId))
                .ReturnsAsync(author);

            Mock.Get(_postRepository)
                .Setup(repo => repo.GetById(postId))
                .ReturnsAsync(post);

            Mock.Get(_mapper)
                .Setup(m => m.Map<LikePost>(request))
                .Returns(likePost);

            Mock.Get(_postRepository)
                .Setup(repo => repo.AddLikePost(likePost));

            Mock.Get(_mapper)
                .Setup(m => m.Map<LikePostResponse>(likePost))
                .Returns(expectedResponse);

            // Act
            var result = await _postService.AddLikePost(authorId, postId, request);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

    }
}
