using System.ComponentModel.DataAnnotations;

namespace GeekWorld.Application.Contracts.Dtos.Requests.Friendships
{
    public class FriendshipRequest
    {
        [Required(ErrorMessage = "O campo identificador do amigo é obrigatório.")]
        public Guid FriendId { get; set; }

        public FriendshipRequest(Guid friendId)
        {
            FriendId = friendId;
        }
    }
}
