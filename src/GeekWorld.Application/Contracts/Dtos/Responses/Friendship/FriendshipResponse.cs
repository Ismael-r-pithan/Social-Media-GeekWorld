using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Validations;

namespace GeekWorld.Application.Contracts.Dtos.Responses.Friendship
{
    public class FriendshipResponse : Notifiable
    {
        public Guid id { get; set; }
        public string Status { get; set; }
        public UserResponse User { get; set; }

        public FriendshipResponse(Guid id, string status, UserResponse user)
        {
            this.id = id;
            Status = status;
            User = user;
        }

        public FriendshipResponse()
        {
        }
    }
}
