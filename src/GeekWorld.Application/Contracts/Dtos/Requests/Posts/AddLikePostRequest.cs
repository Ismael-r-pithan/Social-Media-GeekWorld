using System.ComponentModel.DataAnnotations;

namespace GeekWorld.Application.Contracts.Dtos.Requests.Posts
{
    public class AddLikePostRequest
    {
        [Required(ErrorMessage = "O campo like/deslike é obrigatório.")]
        public bool Value { get; set; }
    }
}
