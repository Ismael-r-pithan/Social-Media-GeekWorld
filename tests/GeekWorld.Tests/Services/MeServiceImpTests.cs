using AutoMapper;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Application.Implementations;
using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Models;
using Moq;

namespace GeekWorld.Tests.Services
{
    public class MeServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IMeService _meService;

        public MeServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _meService = new MeServiceImp(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Me_ShouldReturnProfileResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var user = new User("John Doe", "john@example.com", "johndoe", DateTime.Now, "12345", "password", "image.jpg");
            var expectedResponse = _mapperMock.Object.Map<ProfileResponse>(user);

            _userRepositoryMock.Setup(repo => repo.GetById(id))
                .ReturnsAsync(user);

            _mapperMock.Setup(mapper => mapper.Map<ProfileResponse>(It.IsAny<User>()))
                .Returns(expectedResponse);

            // Act
            var result = await _meService.Me(id);

            // Assert
            Assert.Equal(expectedResponse, result);
        }


    }

}
