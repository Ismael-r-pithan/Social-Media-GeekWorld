using AutoMapper;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Application.Implementations;
using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Models;
using Moq;

namespace GeekWorld.Tests.Services
{
    public class UserServiceImpTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserServiceImpTests()
        {
            _userRepository = Mock.Of<IUserRepository>();
            _mapper = Mock.Of<IMapper>();
            _userService = new UserServiceImp(_userRepository, _mapper);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var meId = Guid.NewGuid();
            var page = 1;
            var limit = 10;
            var search = "John";

            var users = Enumerable.Empty<User>();

            var expectedResponse = Enumerable.Empty<UserResponse>();

            Mock.Get(_userRepository)
                .Setup(repo => repo.GetAll(meId, page, limit, search))
                .ReturnsAsync(users);

            // Act
            var result = await _userService.GetAll(meId, page, limit, search);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task ValidateEmailAlreadyExists_ShouldReturnTrue_WhenEmailExists()
        {
            // Arrange
            var email = "john@example.com";

            Mock.Get(_userRepository)
                .Setup(repo => repo.ValidateEmailAlreadyExists(email))
                .ReturnsAsync(true);

            // Act
            var result = await _userService.ValidateEmailAlreadyExists(email);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateEmailAlreadyExists_ShouldReturnFalse_WhenEmailDoesNotExist()
        {
            // Arrange
            var email = "john@example.com";

            Mock.Get(_userRepository)
                .Setup(repo => repo.ValidateEmailAlreadyExists(email))
                .ReturnsAsync(false);

            // Act
            var result = await _userService.ValidateEmailAlreadyExists(email);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnUserResponse_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User("John Doe", "john@example.com", "john.doe", DateTime.Now, "12345", "password", "profile.jpg");
            var expectedResponse = new UserResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email
            };

            Mock.Get(_userRepository)
                .Setup(repo => repo.GetById(userId))
                .ReturnsAsync(user);

            Mock.Get(_mapper)
                .Setup(m => m.Map<UserResponse>(user))
                .Returns(expectedResponse);

            // Act
            var result = await _userService.GetById(userId);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

    }
}
