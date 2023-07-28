using GeekWorld.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GeekWorld.Application.Contracts.Dtos.Requests.Posts;
public class CreatePostRequest
{
    [Required(ErrorMessage = "O campo Conteudo é obrigatório.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "O campo Visibilidade é obrigatório.")]
    [EnumDataType(typeof(Visibility), ErrorMessage = "O valor fornecido para Visibilidade é inválido.")]
    public string Visibility { get; set; }

    public CreatePostRequest(string content, string visibility)
    {
        Content = content;
        Visibility = visibility;
    }
}
