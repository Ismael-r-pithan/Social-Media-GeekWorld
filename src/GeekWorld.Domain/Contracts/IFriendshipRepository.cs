using GeekWorld.Domain.Models;

namespace GeekWorld.Domain.Contracts
{
    public interface IFriendshipRepository
    {
        Task<Friendship> FriendshipRequest(Friendship friendship);
        Task<Friendship?> FriendshipResponse(Friendship friendship);
        void RemoveFriendship(Guid friendship);
        Task<Friendship?> GetById(Guid friendshipId);
        Task<Boolean> ValidFriendRequest(Guid meId, Guid friendId);
        Task<IEnumerable<Friendship>> GetAllFriendshipRequests(Guid meId);
        public Task<string?> GetFriendshipStatus(Guid meId, Guid userId);
    }
}
