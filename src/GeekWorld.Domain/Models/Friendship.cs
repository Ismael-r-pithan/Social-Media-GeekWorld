namespace GeekWorld.Domain.Models
{
    public class Friendship
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Status { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public Guid FriendId { get; private set; }
        public User Friend { get; private set; }

        public Friendship() { }

        public Friendship(string status, Guid userId, User user, Guid friendId, User friend)
        {
            Status = status;
            UserId = userId;
            User = user;
            FriendId = friendId;
            Friend = friend;
        }

        public void FriendshipInteraction(User me, User friend, string status)
        {
            UserId = me.Id;
            FriendId = friend.Id;
            User = me;
            Friend = friend;
            Status = status;
        }


        public void FriendshipResponse(string status)
        {
            Status = status;
        }
    }
}
