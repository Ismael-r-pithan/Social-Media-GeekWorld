using GeekWorld.Application.Contracts.Dtos.Responses.Users;

namespace GeekWorld.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<Boolean> ValidateEmailAlreadyExists(string email);

        Task<IEnumerable<UserResponse>> GetAll(Guid meId, int page, int limit, string search);

        public Task<UserResponse> GetById(Guid userId);
    }
}
