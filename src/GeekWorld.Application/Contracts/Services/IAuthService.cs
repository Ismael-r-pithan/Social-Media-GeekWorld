using GeekWorld.Application.Contracts.Dtos.Requests.Users;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;

namespace GeekWorld.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<UserResponse> SignUp(CreateUserRequest request);
        Task<UserResponse> Login(LoginUserRequest request);
    }
}
