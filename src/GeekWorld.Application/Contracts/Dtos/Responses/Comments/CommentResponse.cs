using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Validations;

namespace GeekWorld.Application.Contracts.Dtos.Responses.Comments
{
    public class CommentResponse : Notifiable
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        public UserResponse Author { get; set; }

        public CommentResponse() { }

        public CommentResponse(Guid id, string content, UserResponse author)
        {
            Id = id;
            Content = content;
            Author = author;
        }
    }
}