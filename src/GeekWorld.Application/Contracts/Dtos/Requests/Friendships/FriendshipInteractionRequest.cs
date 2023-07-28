using GeekWorld.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GeekWorld.Application.Contracts.Dtos.Requests.Friendships
{
    public class FriendshipInteractionRequest
    {
        [Required(ErrorMessage = "O campo identificador do amigo é obrigatório.")]
        public Guid FriendshipId { get; set; }

        [Required(ErrorMessage = "O campo com o valor da resposta da solicitação (status) é obrigatório.")]
        [EnumDataType(typeof(StatusFriendship), ErrorMessage = "O campo com o valor da resposta da solicitação é inválido.")]
        public string Status { get; set; }

        public FriendshipInteractionRequest(Guid friendshipId, string status)
        {
            FriendshipId = friendshipId;
            Status = status;
        }
    }
}
