using GeekWorld.Domain.Contracts;
using GeekWorld.Domain.Enums;
using GeekWorld.Domain.Models;
using GeekWorld.Infrastructure.Database.Configs;
using Microsoft.EntityFrameworkCore;

namespace GeekWorld.Infrastructure.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly DataContext _context;

        public FriendshipRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Friendship?> GetById(Guid friendshipId)
        {
            Friendship? friendship = await _context.Friendships.FirstOrDefaultAsync(f => f.Id == friendshipId);
            if (friendship == null)
            {
                return null;
            }
            return friendship;
        }

        public async Task<Boolean> ValidFriendRequest(Guid meId, Guid friendId)
        {
            Friendship? friendship = await _context.Friendships.FirstOrDefaultAsync(f => (f.FriendId == friendId && f.UserId == meId) || (f.UserId == friendId && f.FriendId == meId));

            return friendship == null;
        }

        public async void RemoveFriendship(Guid friendshipId)
        {
            Friendship? friendship = await GetById(friendshipId);
            if (friendship != null)
            {
                _context.Friendships.Remove(friendship);
            }
        }

        public async Task<Friendship> FriendshipRequest(Friendship friendship)
        {
            await _context.Friendships.AddAsync(friendship);
            await _context.SaveChangesAsync();
            return friendship;
        }

        public async Task<Friendship?> FriendshipResponse(Friendship friendship)
        {
            Friendship? friendshipExists = await GetById(friendship.Id);
            if (friendshipExists == null)
            {
                return null;
            }

            friendship.FriendshipResponse(friendshipExists.Status);
            _context.Friendships.Update(friendship);

            await _context.Friendships.AddAsync(new Friendship(friendship.Status, friendshipExists.FriendId, friendshipExists.Friend, friendshipExists.UserId, friendshipExists.User));

            await _context.SaveChangesAsync();
            return friendship;
        }

        public async Task<IEnumerable<Friendship>> GetAllFriendshipRequests(Guid meId)
        {
            var requests = await _context.Friendships
                .Where(f => f.FriendId == meId && f.Status == StatusFriendship.PENDING.ToString())
                .ToListAsync();

            var userIds = requests.Select(f => f.UserId == meId ? f.FriendId : f.UserId).ToList();

            var friendships = await _context.Friendships
                .Include(f => f.User)
                .Include(f => f.Friend)
                .Where(f => userIds.Contains(f.UserId) || userIds.Contains(f.FriendId))
                .ToListAsync();

            return friendships;
        }


        public async Task<string?> GetFriendshipStatus(Guid meId, Guid userId)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f =>
                    (f.UserId == meId && f.FriendId == userId) ||
                    (f.UserId == userId && f.FriendId == meId));

            return friendship?.Status;
        }

    }
}
