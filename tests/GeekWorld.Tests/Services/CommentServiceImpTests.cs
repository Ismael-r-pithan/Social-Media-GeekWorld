namespace GeekWorld.Tests.Services;

using AutoMapper;
using GeekWorld.Application.Contracts.Dtos.Requests.Comments;
using GeekWorld.Application.Implementations;
using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Models;
using Moq;

public class CommentServiceImpTests
{
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CommentServiceImp _commentService;

    public CommentServiceImpTests()
    {
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _postRepositoryMock = new Mock<IPostRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _commentService = new CommentServiceImp(
            _commentRepositoryMock.Object,
            _mapperMock.Object,
            _postRepositoryMock.Object,
            _userRepositoryMock.Object
        );
    }

    [Fact]
    public async Task AddCommentOnPost_ShouldAddComment_WhenAuthorAndPostExist()
    {
        // Arrange
        var request = new CreateCommentRequest();
        var postId = Guid.NewGuid();
        var authorId = Guid.NewGuid();
        var author = new User();
        var post = new Post();
        var comment = new Comment();

        _userRepositoryMock.Setup(repo => repo.GetById(authorId))
            .ReturnsAsync(author);

        _postRepositoryMock.Setup(repo => repo.GetById(postId))
            .ReturnsAsync(post);

        _mapperMock.Setup(mapper => mapper.Map<Comment>(request))
            .Returns(comment);

        // Act
        await _commentService.AddCommentOnPost(request, postId, authorId);

        // Assert
        _commentRepositoryMock.Verify(repo => repo.AddCommentOnPost(comment), Times.Once);
    }

    [Fact]
    public async Task AddCommentOnPost_ShouldNotAddComment_WhenAuthorDoesNotExist()
    {
        // Arrange
        var request = new CreateCommentRequest();
        var postId = Guid.NewGuid();
        var authorId = Guid.NewGuid();
        User author = null;
        var post = new Post();

        _userRepositoryMock.Setup(repo => repo.GetById(authorId))
            .ReturnsAsync(author);

        _postRepositoryMock.Setup(repo => repo.GetById(postId))
            .ReturnsAsync(post);

        // Act
        await _commentService.AddCommentOnPost(request, postId, authorId);

        // Assert
        _commentRepositoryMock.Verify(repo => repo.AddCommentOnPost(It.IsAny<Comment>()), Times.Never);
    }

    [Fact]
    public async Task AddCommentOnPost_ShouldNotAddComment_WhenPostDoesNotExist()
    {
        // Arrange
        var request = new CreateCommentRequest();
        var postId = Guid.NewGuid();
        var authorId = Guid.NewGuid();
        var author = new User();
        Post post = null;

        _userRepositoryMock.Setup(repo => repo.GetById(authorId))
            .ReturnsAsync(author);

        _postRepositoryMock.Setup(repo => repo.GetById(postId))
            .ReturnsAsync(post);

        // Act
        await _commentService.AddCommentOnPost(request, postId, authorId);

        // Assert
        _commentRepositoryMock.Verify(repo => repo.AddCommentOnPost(It.IsAny<Comment>()), Times.Never);
    }

}