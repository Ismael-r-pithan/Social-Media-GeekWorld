using GeekWorld.Application.Contracts.Dtos.Requests.Friendships;
using GeekWorld.Application.Contracts.Dtos.Responses.Friendship;

namespace GeekWorld.Application.Contracts.Services
{
    public interface IFriendshipService
    {
        Task<FriendshipResponse> FriendshipRequest(Guid meId, FriendshipRequest request);
        Task<FriendshipResponse> FriendshipResponse(Guid meId, FriendshipInteractionRequest request);
        Task<IEnumerable<FriendshipResponse>> GetAllRequests(Guid meId);
        Task<FriendshipResponse> ValidFriendRequest(Guid userId);
        Task<string?> GetFriendshipStatus(Guid meId, Guid userId);

    }
}
