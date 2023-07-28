using System.ComponentModel.DataAnnotations;

namespace GeekWorld.Application.Contracts.Dtos.Requests.Comments
{
    public class CreateCommentRequest
    {

        [Required(ErrorMessage = "O Conteudo do comentário é obrigatório.")]
        public string Content { get; set; }


        public CreateCommentRequest(string content)
        {
            Content = content;
        }

        public CreateCommentRequest()
        {
        }
    }
}
