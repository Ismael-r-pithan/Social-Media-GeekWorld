using GeekWorld.Application.Contracts.Dtos.Requests.Users;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;

namespace GeekWorld.Application.Contracts.Services
{
    public interface IMeService
    {
        Task<ProfileResponse> Me(Guid id);

        Task<UserResponse> Update(Guid id, UpdateProfileRequest request);

        public Task<IEnumerable<UserResponse>> GetAllFriends(Guid meId, int page, int limit, string search);
    }
}
